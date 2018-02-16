import { HttpClient } from '@angular/common/http';
import { AuthService } from './auth.service';
import { Observable } from 'rxjs/Observable';
import { LoginModel } from '../models/login.model';
import { TokenModel } from '../models/token.model';

describe('AuthService', () => {
  let testObject: AuthService;
  let httpClient: HttpClient;
  
  beforeEach(() => {
    httpClient = <any>{
        post: _ => Observable.of({})
    };
    
    testObject = new AuthService(httpClient);
  });

  describe('isAuthenticated', () => {
    it('should return false', () => {
        spyOn(window.localStorage, 'getItem').and.returnValue(null);

        let result = testObject.isAuthenticated();
        expect(result).toBeFalsy();
    });

    it('should return false', () => {
        spyOn(window.localStorage, 'getItem').and.returnValue('token');

        let result = testObject.isAuthenticated();
        expect(result).toBeTruthy();
    });
  });

  describe('getToken', () => {
    it('should return string from localStorage', () => {
        spyOn(window.localStorage, 'getItem').and.returnValue('token');

        let result = testObject.getToken();
        expect(result).toBe('token');
    });
  });

  describe('logout', () => {
    it('should remove token from localStorage', () => {
        spyOn(window.localStorage, 'removeItem');

        let result = testObject.logout();
        expect(window.localStorage.removeItem).toHaveBeenCalledWith('auth_token');
    });
  });

  describe('login', () => {
    it('should return true and add token to localStorage', done => {
        spyOn(window.localStorage, 'setItem');
        spyOn(httpClient, 'post').and.returnValue(Observable.of(<TokenModel>{ token: 'token' }))
        var model: LoginModel = { email: 'sam@sample.com', password: 'a' };

        let result = testObject.login(model).subscribe(result => {
            expect(window.localStorage.setItem).toHaveBeenCalledWith('auth_token', 'token');
            expect(result).toBeTruthy();
            done();
        });
    });

    it('should return false and nothing add to localStorage', done => {
        spyOn(window.localStorage, 'setItem');
        spyOn(httpClient, 'post').and.returnValue(Observable.of(<TokenModel>{ token: null }))
        var model: LoginModel = { email: 'sam@sample.com', password: 'a' };

        let result = testObject.login(model).subscribe(result => {
            expect(window.localStorage.setItem).not.toHaveBeenCalled();
            expect(result).toBeFalsy();
            done();
        });
    });
  });
});