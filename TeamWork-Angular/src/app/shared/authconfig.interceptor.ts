import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpInterceptor
} from '@angular/common/http';

@Injectable()
export class AuthconfigInterceptor implements HttpInterceptor {

  constructor() { }

  intercept(request: HttpRequest<any>, next: HttpHandler) {

    const authToken = localStorage.getItem('access_token');
    if (authToken != null) {
      request = request.clone({
        setHeaders: {
          Authorization: 'Bearer ' + authToken
        }
      });
    }
    return next.handle(request);
  }
}
