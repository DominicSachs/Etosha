import { HttpRequest } from '@angular/common/http';
import { Router } from '@angular/router';
import { Observable, of, throwError } from 'rxjs';
import { AuthService } from '../services/auth.service';
import { TokenInterceptor } from './token.interceptor';

describe('TokenInterceptor', () => {
  let testObject: TokenInterceptor;
  let authService: AuthService;
  let router: Router;

  beforeEach(() => {
    router = <any>{
      navigate: jasmine.createSpy('navigate')
    };

    authService = <any>{
      isAuthenticated: _ => true,
      getToken: _ => 'token'
    };

    testObject = new TokenInterceptor(authService, router);
  });

  it('should inject authorization token into requests', done => {
    const next = <any>{ handle: _ => { } };
    spyOn(next, 'handle').and.callFake(request => {
      expect(request.headers.get('Authorization')).toEqual('Bearer token');
      done();
      return of({});
    });

    testObject.intercept(new HttpRequest('get', '/', {}), next).subscribe(_ => { });
  });

  it('should navigate to login on 401', done => {
    const next = <any>{ handle: _ => throwError({ status: 401 }) };

    testObject.intercept(new HttpRequest('get', '/', {}), next).subscribe(_ => {
      expect(router.navigate).toHaveBeenCalledWith(['login']);
      done();
    });
  });

  it('should navigate to login on 403', done => {
    const next = <any>{ handle: _ => throwError({ status: 403 }) };

    testObject.intercept(new HttpRequest('get', '/', {}), next).subscribe(_ => {
      expect(router.navigate).toHaveBeenCalledWith(['login']);
      done();
    });
  });

  it('should throw an error if not 401 or 403', done => {
    const next = <any>{ handle: _ => throwError({ status: 400 }) };

    testObject.intercept(new HttpRequest('get', '/', {}), next).subscribe(_ => { }, error => {
      expect(error.status).toEqual(400);

      done();
    });
  });
});
