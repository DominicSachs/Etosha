import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, of } from 'rxjs';
import { LoginModel } from '../models/login.model';
import { TokenModel } from '../models/token.model';
import { AuthService } from './auth.service';
import { WindowRef } from './window/window.ref';

describe('AuthService', () => {
  let testObject: AuthService;
  let httpClient: HttpClient;
  let windowRef: WindowRef;

  beforeEach(() => {
    httpClient = <any>{
      post: _ => of({})
    };

    windowRef = {
      nativeWindow: {
        localStorage: {
          getItem: _ => {},
          setItem: () => {},
          removeItem: _ => {}
        }
      }
    } as any;

    testObject = new AuthService(httpClient, windowRef);
  });

  describe('isAuthenticated', () => {
    it('should return false', () => {
      jest.spyOn(windowRef.nativeWindow.localStorage, 'getItem').mockReturnValue(null);

      const result = testObject.isAuthenticated();
      expect(result).toBeFalsy();
    });

    it('should return false', () => {
      jest.spyOn(windowRef.nativeWindow.localStorage, 'getItem').mockReturnValue('token');

      const result = testObject.isAuthenticated();
      expect(result).toBeTruthy();
    });
  });

  describe('getToken', () => {
    it('should return string from localStorage', () => {
      jest.spyOn(windowRef.nativeWindow.localStorage, 'getItem').mockReturnValue('token');

      const result = testObject.getToken();
      expect(result).toBe('token');
    });
  });

  describe('logout', () => {
    it('should remove token from localStorage', () => {
      jest.spyOn(windowRef.nativeWindow.localStorage, 'removeItem');

      testObject.logout();
      expect(windowRef.nativeWindow.localStorage.removeItem).toHaveBeenCalledWith('auth_token');
    });
  });

  describe('login', () => {
    it('should return true and add token to localStorage', done => {
      jest.spyOn(windowRef.nativeWindow.localStorage, 'setItem');
      jest.spyOn(httpClient, 'post').mockReturnValue(of(<TokenModel>{ token: 'token' }));
      const model: LoginModel = { email: 'sam@sample.com', password: 'a' };

      testObject.login(model).subscribe(result => {
        expect(windowRef.nativeWindow.localStorage.setItem).toHaveBeenCalledWith('auth_token', 'token');
        expect(result).toBeTruthy();
        done();
      });
    });

    it('should return false and nothing add to localStorage', done => {
      jest.spyOn(windowRef.nativeWindow.localStorage, 'setItem');
      jest.spyOn(httpClient, 'post').mockReturnValue(of(<TokenModel>{ token: null }));
      const model: LoginModel = { email: 'sam@sample.com', password: 'a' };

      testObject.login(model).subscribe(result => {
        expect(windowRef.nativeWindow.localStorage.setItem).not.toHaveBeenCalled();
        expect(result).toBeFalsy();
        done();
      });
    });
  });

  describe('login', () => {
    it('should return observable with true', done => {
      const subject = new BehaviorSubject<boolean>(false);
      testObject.isLoginSubject = subject;
      subject.next(true);

      testObject.isLoggedIn().subscribe(result => {
        expect(result).toBeTruthy();
        done();
      });
    });

    it('should return observable with false', done => {
      const subject = new BehaviorSubject<boolean>(true);
      testObject.isLoginSubject = subject;
      subject.next(false);

      testObject.isLoggedIn().subscribe(result => {
        expect(result).toBeFalsy();
        done();
      });
    });
  });
});
