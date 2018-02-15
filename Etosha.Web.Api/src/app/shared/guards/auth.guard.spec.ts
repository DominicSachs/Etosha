import { Router, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { AuthGuard } from './auth.guard';
import { AuthService } from '../services/auth.service';

describe('AuthGuard', () => {
  let testObject: AuthGuard;
  let router: Router;
  let authService: AuthService;
  let next: ActivatedRouteSnapshot;
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
    let result = testObject.canActivate(next, state);
    expect(result).toBeTruthy();
  });

  it('should navigate to login and return false', () => {
    state = <any>{ url: 'test' };
    spyOn(authService, 'isAuthenticated').and.returnValue(false);

    let result = testObject.canActivate(next, state);
    expect(result).toBeFalsy();
    expect(router.navigate).toHaveBeenCalledWith(['login'], { queryParams: { returnUrl: 'test' }});
  });
});