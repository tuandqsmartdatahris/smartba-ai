import {
  HttpInterceptorFn
} from '@angular/common/http';

import { inject } from '@angular/core';

import { AuthService }
  from '../services/auth.service';

export const authInterceptor: HttpInterceptorFn =
(req, next) => {
   
  console.log('AUTH INTERCEPTOR RUNNING');
    
  const authService = inject(AuthService);

  const token = authService.getAccessToken();

  if (token) {

    req = req.clone({

      setHeaders: {
        Authorization:
          `Bearer ${token}`
      }

    });

  }

  if (req.url.includes('/v1/businesses')) {
          return next(req); // Cho phép request đi qua luôn mà không gắn thêm token nội bộ
      }

  return next(req);
};


/*

HttpClient
      ↓
AuthInterceptor
      ↓
AuthService
      ↓
localStorage
      ↓
accessToken

*/