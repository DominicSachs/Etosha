import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/observable/of'
import { environment } from '../../environments/environment';
import { UserService } from './user.service';
import { User } from './user.model';

describe('UserService', () => {
  let httpClient: HttpClient;
  let service: UserService;

  httpClient = <any>{
    get: _ => Observable.of({})   
  };

  beforeEach(() => {
    service = new UserService(httpClient);
  });

  it('should get users', done => {
    const users: User[] = [
      { id: 1, firstName: 'A', lastName: 'A', userName: 'a@a.com', email: 'a@a.com' },
      { id: 2, firstName: 'B', lastName: 'B', userName: 'b@b.com', email: 'b@b.com' }
    ];

    spyOn(httpClient, 'get').and.returnValue(Observable.of(users));

    service.getUsers().subscribe(result => {
      expect(httpClient.get).toHaveBeenCalledWith(environment.apiEndpoint + '/users');
      expect(result.length).toBe(2);
      done();
    });
  });

  it('should get an user', done => {
    const user: User = { id: 1, firstName: 'A', lastName: 'A', userName: 'a@a.com', email: 'a@a.com' };

    spyOn(httpClient, 'get').and.returnValue(Observable.of(user));

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
});
