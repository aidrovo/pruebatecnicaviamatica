import { inject } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { CanActivateFn } from '@angular/router';

export const authGuard: CanActivateFn = () => {
  const auth = inject(AuthService);
  const router = inject(Router);

  const token = localStorage.getItem('token');

  if (token) return true;

  if (auth.isLoggedIn()) {
    return true;
  }

  return router.createUrlTree(['/']);
};
