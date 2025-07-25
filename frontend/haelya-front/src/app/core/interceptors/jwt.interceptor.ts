import { HttpErrorResponse, HttpHandlerFn, HttpInterceptorFn, HttpRequest } from '@angular/common/http';
import { inject } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { catchError, switchMap, throwError } from 'rxjs';

// Liste des routes publiques où il ne faut pas renvoyer le token
const PUBLIC_ROUTES = [
    '/auth/login',
    '/auth/register',
    '/api/Auth/Refresh'
]

export const jwtInterceptor: HttpInterceptorFn = (req: HttpRequest<any>, next: HttpHandlerFn) => {
  const authService: AuthService = inject(AuthService);
  const token = authService.getToken();
   const isPublic = PUBLIC_ROUTES.some(route => req.url.includes(route));

  // Si la route est publique, on laisse passer
  if (isPublic) {
    return next(req);
  }

  // Cloner la requête avec le token si disponible
  const authReq = token
    ? req.clone({
        headers: req.headers.set('Authorization', `Bearer ${token}`),
        withCredentials: true
      })
    : req;

  return next(authReq).pipe(
    catchError((error: HttpErrorResponse) => {
      if (error.status === 401 && !req.url.includes('/api/Auth/Refresh')) {
         console.warn('[Interceptor] Token expiré. Tentative de refresh...');
        // Le token a probablement expiré → tentative de refresh
        return authService.refreshToken().pipe(
          switchMap(response => {
            const newToken = response.accessToken;
            authService.saveAuth(newToken)

            // Rejouer la requête d'origine avec le nouveau token
            const retryReq = req.clone({
              headers: req.headers.set('Authorization', `Bearer ${newToken}`),
              withCredentials: true
            });

            return next(retryReq);
          }),
          catchError(refreshError => {
            console.error('[Interceptor] Refresh échoué, redirection vers /login', refreshError); 
            // Si le refresh échoue, logout
            authService.logout();
            return throwError(() => refreshError);
          })
        );
      }

      return throwError(() => error);
    })
  );
};