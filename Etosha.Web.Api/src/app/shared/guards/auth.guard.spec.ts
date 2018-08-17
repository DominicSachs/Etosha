import { ActivatedRouteSnapshot, Router, RouterStateSnapshot } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { AuthGuard } from './auth.guard';

describe('AuthGuard', () => {
  let testObject: AuthGuard;
  let router: Router;
  let authService: AuthService;
  let state: RouterStateSnapshot;

  beforeEach(() => {
    router = <any>{
        navigate: jasmine.createSpy('navigate')
    };

    authService = <any>{
        isAuthenticated: _ => true
    };

    testObject = new AuthGuard(authService, router);
  });

  it('should activate and return true', () => {
    const result = testObject.canActivate(null, state);
    expect(result).toBeTruthy();
  });

  it('should navigate to login and return false', () => {
    state = <any>{ url: 'test' };
    spyOn(authService, 'isAuthenticated').and.returnValue(false);

    const result = testObject.canActivate(null, state);
    expect(result).toBeFalsy();
    expect(router.navigate).toHaveBeenCalledWith(['login'], { queryParams: { returnUrl: 'test' }});
  });
});
