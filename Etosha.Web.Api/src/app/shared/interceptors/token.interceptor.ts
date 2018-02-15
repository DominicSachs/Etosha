import { Injectable, Injector } from '@angular/core';
import { Router } from '@angular/router';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpErrorResponse } from '@angular/common/http';
import { AuthService } from '../services/auth.service';
import { Observable } from 'rxjs/Observable';

@Injectable()
export class TokenInterceptor implements HttpInterceptor {
  constructor(private injector: Injector) { }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const authService = this.injector.get(AuthService);
    const router = this.injector.get(Router);

    if (authService.isAuthenticated()) {
        request = request.clone({ setHeaders: { Authorization: `Bearer ${authService.getToken()}` } });
    }

    return next.handle(request)
            .catch(res => {
                if (res.status === 401 || res.status === 403) {
                  router.navigate(['login']);
                  return Observable.empty();
                } else {
                    return Observable.throw(res);
                }
            });
  }
}
