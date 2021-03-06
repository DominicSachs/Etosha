import { FormBuilder } from '@angular/forms';
import { ActivatedRoute, Router} from '@angular/router';
import { of, throwError } from 'rxjs';
import { LoginModel } from '../../shared/models/login.model';
import { AuthService } from '../../shared/services/auth.service';
import { LoginComponent } from './login.component';

describe('LoginComponent', () => {
    let testObject: LoginComponent;
    let activatedRoute: ActivatedRoute;
    let router: Router;
    let authService: AuthService;

    beforeEach(() => {
        activatedRoute = <any>{
            snapshot: {
                queryParams: {
                    returnUrl: 'foo'
                }
            }
        };

        router = <any>{
            navigateByUrl: _ => { }
        };

        authService = <any>{
            login: _ => { }
        };

        testObject = new LoginComponent(new FormBuilder(), authService, activatedRoute, router);
    });

    it('should construct a form', () => {
        expect(testObject.loginForm.controls['email']).toBeDefined();
        expect(testObject.loginForm.controls['password']).toBeDefined();
    });

    it('should navigate to return url', () => {
        jest.spyOn(router, 'navigateByUrl');
        jest.spyOn(authService, 'login').mockReturnValue(of(true));

        testObject.ngOnInit();
        testObject.onSubmit({ value: new LoginModel(), valid: true });
        expect(router.navigateByUrl).toHaveBeenCalledWith('foo');
    });

    it('should navigate to default page without return url', () => {
        jest.spyOn(router, 'navigateByUrl');
        jest.spyOn(authService, 'login').mockReturnValue(of(true));
        activatedRoute.snapshot.queryParams.returnUrl = null;
        testObject.ngOnInit();
        testObject.onSubmit({ value: new LoginModel(), valid: true });
        expect(router.navigateByUrl).toHaveBeenCalledWith('/');
    });

    it('should do nothing on submit if !valid', () => {
        jest.spyOn(router, 'navigateByUrl');
        jest.spyOn(authService, 'login').mockReturnValue(throwError(false));

        testObject.onSubmit({ value: new LoginModel(), valid: true });
        expect(router.navigateByUrl).not.toHaveBeenCalled();
        expect(testObject.hasError).toBeTruthy();
    });
});
