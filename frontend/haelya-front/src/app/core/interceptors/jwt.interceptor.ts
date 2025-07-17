import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { AuthService } from '../services/auth.service';

// Liste des routes publiques où il ne faut pas renvoyer le token
const PUBLIC_ROUTES = [
    '/auth/login',
    '/auth/register'
]

export const jwtInterceptor: HttpInterceptorFn = (req, next) => {
  const _authService: AuthService = inject(AuthService);

  const token = _authService.getToken();

  //Si la requête cible une des routes publique, on n'ajoute pas le token
  if (PUBLIC_ROUTES.some(route => req.url.includes(route))) {
    return next(req);
  }

  // vérifier si le token existe
  if (token) {
    // ajouter Authorization dans les headers
    const requestClone = req.clone({
      headers: req.headers.append("Authorization", "Bearer " + token),
    });

    return next(requestClone);
  }

  return next(req);
};