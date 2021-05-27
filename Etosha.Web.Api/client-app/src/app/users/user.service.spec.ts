import { HttpClient } from '@angular/common/http';
import { of } from 'rxjs';
import { environment } from '../../environments/environment';
import { User } from './user.model';
import { UserService } from './user.service';

describe('UserService', () => {
  let httpClient: HttpClient;
  let service: UserService;

  beforeEach(() => {
    httpClient = <any>{
      get: _ => of({}),
      post: _ => of({}),
      put: _ => of({}),
      delete: _ => of({})
    };

    service = new UserService(httpClient);
  });

  it('should get users', done => {
    const users: User[] = [
      { id: 1, firstName: 'A', lastName: 'A', userName: 'a@a.com', email: 'a@a.com', roleId: 1 },
      { id: 2, firstName: 'B', lastName: 'B', userName: 'b@b.com', email: 'b@b.com', roleId: 1 }
    ];

    jest.spyOn(httpClient, 'get').mockReturnValue(of(users));

    service.getUsers().subscribe(result => {
      expect(httpClient.get).toHaveBeenCalledWith(environment.apiEndpoint + '/users');
      expect(result.length).toBe(2);
      done();
    });
  });

  it('should get an user', done => {
    const user: User = { id: 1, firstName: 'A', lastName: 'A', userName: 'a@a.com', email: 'a@a.com', roleId: 1 };

    jest.spyOn(httpClient, 'get').mockReturnValue(of(user));

    service.getUser(1).subscribe(result => {
      expect(httpClient.get).toHaveBeenCalledWith(environment.apiEndpoint + '/users/1');
      expect(result.id).toBe(1);
      expect(result.firstName).toBe('A');
      expect(result.lastName).toBe('A');
      expect(result.userName).toBe('a@a.com');
      expect(result.email).toBe('a@a.com');
      done();
    });
  });

  it('should post a new user', done => {
    const user: User = { id: 0, firstName: 'A', lastName: 'A', userName: 'a@a.com', email: 'a@a.com', roleId: 1 };

    jest.spyOn(httpClient, 'post').mockReturnValue(of(user));

    service.saveUser(user).subscribe(result => {
      expect(httpClient.post).toHaveBeenCalledWith(environment.apiEndpoint + '/users', user);
      done();
    });
  });

  it('should put an existing user', done => {
    const user: User = { id: 1, firstName: 'A', lastName: 'A', userName: 'a@a.com', email: 'a@a.com', roleId: 1 };
    jest.spyOn(httpClient, 'put').mockReturnValue(of(user));

    service.saveUser(user).subscribe(result => {
      expect(httpClient.put).toHaveBeenCalledWith(environment.apiEndpoint + '/users/1', user);
      done();
    });
  });

  it('should delete an existing user', done => {
    const user: User = { id: 1, firstName: 'A', lastName: 'A', userName: 'a@a.com', email: 'a@a.com', roleId: 1 };
    jest.spyOn(httpClient, 'delete').mockReturnValue(of(user));

    service.deleteUser(user).subscribe(result => {
      expect(httpClient.delete).toHaveBeenCalledWith(environment.apiEndpoint + '/users/1');
      done();
    });
  });
});
