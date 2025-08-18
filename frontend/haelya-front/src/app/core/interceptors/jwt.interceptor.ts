import { HttpErrorResponse, HttpHandlerFn, HttpInterceptorFn, HttpRequest } from '@angular/common/http';
import { inject } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { catchError, EMPTY, switchMap, throwError } from 'rxjs';
import { Router } from '@angular/router';

// Liste des routes publiques où il ne faut pas renvoyer le token
const PUBLIC_ROUTES = [
    '/auth/login',
    '/auth/register',
    '/api/Auth/Refresh'
]

let isRefreshing = false;

export const jwtInterceptor: HttpInterceptorFn = (req: HttpRequest<any>, next: HttpHandlerFn) => {
  const authService: AuthService = inject(AuthService);
  const router : Router = inject(Router);
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
        
        // Si un refresh est déjà en cours, on évite la boucle
        if (isRefreshing) {
          console.warn('[Interceptor] Refresh déjà en cours, abandon de la requête');
          return EMPTY; // Annule silencieusement cette requête
        }

        console.warn('[Interceptor] Token expiré. Tentative de refresh...');
        isRefreshing = true;

        // Le token a probablement expiré → tentative de refresh
        return authService.refreshToken().pipe(
          switchMap(response => {
            const newToken = response.accessToken;
            authService.saveAuth(newToken);
            isRefreshing = false; // Reset du flag

            // Rejouer la requête d'origine avec le nouveau token
            const retryReq = req.clone({
              headers: req.headers.set('Authorization', `Bearer ${newToken}`),
              withCredentials: true
            });

            return next(retryReq);
          }),
          catchError(refreshError => {
            console.error('[Interceptor] Refresh échoué, redirection vers /login', refreshError);
            isRefreshing = false; // Reset du flag
            
            // Logout sans déclencher de requêtes HTTP supplémentaires
            authService.clearTokens(); // Méthode qui nettoie juste les tokens localement
            
            // Navigation directe vers login
            router.navigate(['/login']).then(() => {
              console.log('[Interceptor] Redirection vers /login effectuée');
            });
            
            return EMPTY; // Retourne EMPTY au lieu de throwError pour éviter les erreurs en cascade
          })
        );
      }

      return throwError(() => error);
    })
  );
};