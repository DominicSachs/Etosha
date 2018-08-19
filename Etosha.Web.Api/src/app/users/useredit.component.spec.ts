import { FormBuilder } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { of } from 'rxjs';
import { User } from './user.model';
import { UserService } from './user.service';
import { UserEditComponent } from './useredit.component';

describe('UsereditComponent', () => {
    let activatedRoute: ActivatedRoute;
    let router: Router;
    let component: UserEditComponent;
    let userService: UserService;
    let formBuilder: FormBuilder;

    beforeEach(() => {
        activatedRoute = <any>{
            params: of({ id: 1 })
        };

        router = <any>{
            navigateByUrl: _ => { }
        };

        userService = <any>{
            getUser: (id: number) => of({}),
            saveUser: (user: User) => of({})
        };

        formBuilder = new FormBuilder();
        component = new UserEditComponent(activatedRoute, userService, formBuilder, router);
    });

    it('should init a new user with id 0', () => {
        activatedRoute.params = of({});
        component.ngOnInit();
        expect(component.userForm.value.id).toBe(0);
    });

    it('should unsubscribe', () => {
        const testSubscription = of({}).subscribe();
        spyOn(activatedRoute.params, 'subscribe').and.returnValue(testSubscription);
        spyOn(testSubscription, 'unsubscribe');
        component.ngOnInit();
        component.ngOnDestroy();
        expect(testSubscription.unsubscribe).toHaveBeenCalled();
    });

    it('should init an existing user', () => {
        const user = { id: 1, firstName: 'Sam', lastName: 'Sample', email: 'sam@sample.com', userName: 'sam@sample.com' };
        spyOn(userService, 'getUser').and.returnValue(of(user));
        component.ngOnInit();
        expect(component.userForm.value.id).toBe(user.id);
        expect(component.userForm.value.firstName).toBe(user.firstName);
        expect(component.userForm.value.lastName).toBe(user.lastName);
        expect(component.userForm.value.email).toBe(user.email);

        expect(userService.getUser).toHaveBeenCalledWith(1);
    });

    it('should do nothing on submit if !valid', () => {
        spyOn(userService, 'saveUser');
        component.onSubmit({ value: new User(), valid: false });
        expect(userService.saveUser).not.toHaveBeenCalled();
    });

    it('should submit, call saveUser and navigate by url', () => {
        const user = { id: 1, firstName: 'Sam', lastName: 'Sample', email: 'sam@sample.com', userName: 'sam@sample.com' };
        spyOn(userService, 'saveUser').and.returnValue(of(user));
        spyOn(router, 'navigateByUrl');
        component.onSubmit({ value: user, valid: true });
        expect(userService.saveUser).toHaveBeenCalledWith(user);
        expect(router.navigateByUrl).toHaveBeenCalledWith('/users');
    });
});
