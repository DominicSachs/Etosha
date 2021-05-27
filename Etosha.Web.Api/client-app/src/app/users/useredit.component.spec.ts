import { FormBuilder } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { of } from 'rxjs';
import { RoleService } from '../shared/services/role.service';
import { User } from './user.model';
import { UserService } from './user.service';
import { UserEditComponent } from './useredit.component';

describe('UsereditComponent', () => {
    let activatedRoute: ActivatedRoute;
    let router: Router;
    let component: UserEditComponent;
    let userService: UserService;
    let formBuilder: FormBuilder;
    let roleService: RoleService;

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

        roleService = <any>{
            getRoles: (id: number) => of([])
        };

        formBuilder = new FormBuilder();
        component = new UserEditComponent(activatedRoute, userService, formBuilder, router, roleService);
    });

    it('should init a new user with id 0', () => {
        activatedRoute.params = of({});
        component.ngOnInit();
        expect(component.userForm.value.id).toBe(0);
    });

    it('should unsubscribe', () => {
        const testSubscription = of({}).subscribe();
        jest.spyOn(activatedRoute.params, 'subscribe').mockReturnValue(testSubscription);
        jest.spyOn(testSubscription, 'unsubscribe');
        component.ngOnInit();
        component.ngOnDestroy();
        expect(testSubscription.unsubscribe).toHaveBeenCalled();
    });

    it('should init an existing user', () => {
        const user: User = { id: 1, firstName: 'Sam', lastName: 'Sample', email: 'sam@sample.com', userName: 'sam@sample.com', roleId: 1 };
        jest.spyOn(userService, 'getUser').mockReturnValue(of(user));
        component.ngOnInit();
        expect(component.userForm.value.id).toBe(user.id);
        expect(component.userForm.value.firstName).toBe(user.firstName);
        expect(component.userForm.value.lastName).toBe(user.lastName);
        expect(component.userForm.value.email).toBe(user.email);

        expect(userService.getUser).toHaveBeenCalledWith(1);
    });

    it('should do nothing on submit if !valid', () => {
        jest.spyOn(userService, 'saveUser');
        component.onSubmit({ value: new User(), valid: false });
        expect(userService.saveUser).not.toHaveBeenCalled();
    });

    it('should submit, call saveUser and navigate by url', () => {
        const user: User = { id: 1, firstName: 'Sam', lastName: 'Sample', email: 'sam@sample.com', userName: 'sam@sample.com', roleId: 1 };
        jest.spyOn(userService, 'saveUser').mockReturnValue(of(user));
        jest.spyOn(router, 'navigateByUrl');
        component.onSubmit({ value: user, valid: true });
        expect(userService.saveUser).toHaveBeenCalledWith(user);
        expect(router.navigateByUrl).toHaveBeenCalledWith('/users');
    });
});
