import { Injector } from '@angular/core';
import { Router } from '@angular/router';
import { HttpRequest } from '@angular/common/http';
import { TokenInterceptor } from './token.interceptor';
import { AuthService } from '../services/auth.service';
import { Observable } from 'rxjs';

describe('TokenInterceptor', () => {
  let testObject: TokenInterceptor;
  let injector: Injector;
  let authService: AuthService;
  let router: Router;

  beforeEach(() => {
    injector = <any>{
      get: (type: any) => {
          if(type === 'AuthService') {
              return authService;
          }

          return router;
      }
    };
    
    router = <any>{
      navigate: jasmine.createSpy('navigate')
    };
    
    authService = <any>{
        isAuthenticated: _ => true,
        getToken: _ => 'token'
    };

    testObject = new TokenInterceptor(injector);
  });

  it('should inject authorization token into requests', done => {
    const next = <any>{ handle: _ => { } };
    spyOn(next, 'handle').and.callFake(request => {
      expect(request.headers.get('Authorization')).toEqual('Bearer token');
      done();
      return Observable.of({});
    });

    testObject.intercept(new HttpRequest('get', '/', {}), next).subscribe(_ => { });
  });

  it('should navigate to login on 401', done => {
    const next = <any>{ handle: _ => { } };
    spyOn(next, 'handle').and.callFake(request => {
      done();
      return Observable.throw({ status: 401 });
    });

    testObject.intercept(new HttpRequest('get', '/', {}), next).subscribe(_ => { });

    expect(router.navigate).toHaveBeenCalledWith(['login']);
  });

  it('should navigate to login on 403', done => {
    const next = <any>{ handle: _ => { } };
    spyOn(next, 'handle').and.callFake(request => {
      done();
      return Observable.throw({ status: 403 });
    });

    testObject.intercept(new HttpRequest('get', '/', {}), next).subscribe(_ => { });

    expect(router.navigate).toHaveBeenCalledWith(['login']);
  });

  it('should throw an error if not 401 or 403', done => {
    const next = <any>{ handle: _ => { } };
    spyOn(next, 'handle').and.callFake(request => {
      done();
      return Observable.throw({ status: 400 });
    });

    testObject.intercept(new HttpRequest('get', '/', {}), next).subscribe(_ => { });

    expect(next.handle).toThrow({ status: 400 });
  });
});
