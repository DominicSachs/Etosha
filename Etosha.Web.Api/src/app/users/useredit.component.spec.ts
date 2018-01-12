import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder } from '@angular/forms';
import { UserEditComponent } from './useredit.component';
import { UserService } from './user.service';
import { Observable } from 'rxjs/Observable';
import { User } from './user.model';

describe('UsereditComponent', () => {
    let activatedRoute: ActivatedRoute;
    let router: Router;
    let component: UserEditComponent;
    let userService: UserService;
    let formBuilder: FormBuilder;

    beforeEach(() => {
        activatedRoute = <any>{
            params: Observable.of({ id: 1 })
        };

        router = <any>{
            navigateByUrl: _ => { }
        };

        userService = <any>{
            getUser: (id: number) => Observable.of({}),
            saveUser: (user: User) => Observable.of({})
        };

        formBuilder = new FormBuilder();
        component = new UserEditComponent(activatedRoute, userService, formBuilder, router);
    });

    it('should init a new user with id 0', () => {
        activatedRoute.params = Observable.of({});
        component.ngOnInit();
        expect(component.userForm.value.id).toBe(0);
    });

    it('should init an existing user', () => {
        var user = { id: 1, firstName: 'Sam', lastName: 'Sample', email: 'sam@sample.com', userName: 'sam@sample.com' };
        spyOn(userService, 'getUser').and.returnValue(Observable.of(user));
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
    
    it('should do nothing on submit if !valid', () => {
        var user = { id: 1, firstName: 'Sam', lastName: 'Sample', email: 'sam@sample.com', userName: 'sam@sample.com' };
        spyOn(userService, 'saveUser').and.returnValue(Observable.of(user));
        spyOn(router, 'navigateByUrl');
        component.onSubmit({ value: user, valid: true });
        expect(userService.saveUser).toHaveBeenCalledWith(user);
        expect(router.navigateByUrl).toHaveBeenCalledWith('/users');
    });
});
