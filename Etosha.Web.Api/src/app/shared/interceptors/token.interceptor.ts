
import {throwError as observableThrowError, of as observableOf,  Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent } from '@angular/common/http';
import { AuthService } from '../services/auth.service';

@Injectable()
export class TokenInterceptor implements HttpInterceptor {
  constructor(private authService: AuthService, private router: Router) { }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    if (this.authService.isAuthenticated()) {
        request = request.clone({ setHeaders: { Authorization: `Bearer ${this.authService.getToken()}` } });
    }

    return next.handle(request).pipe(
            catchError(res => {
                if (res.status === 401 || res.status === 403) {
                    this.router.navigate(['login']);
                    return observableOf(<any>{ });
                } else {
                    return observableThrowError(res);
                }
            }));
  }
}
