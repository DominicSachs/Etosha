import { Observable, of } from 'rxjs';
import { User } from './user.model';
import { UserService } from './user.service';
import { UsersComponent } from './users.component';

describe('UsersComponent', () => {
  let component: UsersComponent;
  let userService: UserService;

  userService = <any>{
    getUsers: () => of([])
  };

  beforeEach(() => {
    component = new UsersComponent(userService);
  });

  it('should load users on init', () => {
    const users: User[] = [
      { id: 1, firstName: 'A', lastName: 'A', userName: 'a@a.com', email: 'a@a.com' },
      { id: 2, firstName: 'B', lastName: 'B', userName: 'b@b.com', email: 'b@b.com' }
    ];

    spyOn(userService, 'getUsers').and.returnValue(of(users));
    component.ngOnInit();

    expect(component.dataSource.data.length).toBe(2);
    expect(userService.getUsers).toHaveBeenCalled();
  });
});
