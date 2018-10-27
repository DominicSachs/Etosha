import { ActivatedRoute, Router } from '@angular/router';
import { of } from 'rxjs';
import { User } from './user.model';
import { UserService } from './user.service';
import { UserDeleteComponent } from './userdelete.component';

describe('UserDeleteComponent', () => {
    let activatedRoute: ActivatedRoute;
    let router: Router;
    let component: UserDeleteComponent;
    let userService: UserService;

    beforeEach(() => {
        activatedRoute = <any>{
            params: of({ id: 1 })
        };

        router = <any>{
            navigateByUrl: _ => { }
        };

        userService = <any>{
            getUser: (id: number) => of({}),
            deleteUser: (user: User) => of({})
        };

        component = new UserDeleteComponent(activatedRoute, userService, router);
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
        const user = { id: 1, firstName: 'Sam', lastName: 'Sample', email: 'sam@sample.com', userName: 'sam@sample.com', roleId: 1 };
        spyOn(userService, 'getUser').and.returnValue(of(user));
        component.ngOnInit();

        expect(component.user).toBe(user);
        expect(userService.getUser).toHaveBeenCalledWith(1);
    });

    it('should do nothing on submit if !valid', () => {
        component.user = { id: 1, firstName: 'Sam', lastName: 'Sample', email: 'sam@sample.com', userName: 'sam@sample.com', roleId: 1 };
        spyOn(userService, 'deleteUser').and.returnValue(of({}));
        spyOn(router, 'navigateByUrl');
        component.delete();

        expect(userService.deleteUser).toHaveBeenCalledWith(component.user);
        expect(router.navigateByUrl).toHaveBeenCalledWith('/users');
    });
});
