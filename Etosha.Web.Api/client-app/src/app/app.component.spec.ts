import { Router } from '@angular/router';
import { AuthService } from './shared/services/auth.service';
import { AppComponent } from './app.component';
import { of } from 'rxjs';
import { take } from 'rxjs/operators';

describe('AppComponent', () => {
  let testObject: AppComponent;
  let router: Router;
  let authService: AuthService;

  beforeEach(() => {
    router = <any>{
      navigateByUrl: _ => { }
    };

    authService = <any>{
      isLoggedIn: _ => { },
      logout: _ => { }
    };

    testObject = new AppComponent(authService, router);
  });

  it('gets login status on init', done => {
    jest.spyOn(authService, 'isLoggedIn').mockReturnValue(of(true));

    testObject.ngOnInit();

    testObject.isLoggedIn$.pipe(take(1)).subscribe(result => {
      expect(result).toBeTruthy();
      done();
    });
  });

  it('should call  authService.logout and navigateByUrl on logout', () => {
    jest.spyOn(router, 'navigateByUrl');
    jest.spyOn(authService, 'logout');

    testObject.logout();

    expect(router.navigateByUrl).toHaveBeenCalledWith('/');
    expect(authService.logout).toHaveBeenCalled();
  });
});
