import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { environment } from '../../../environments/environment';
import { UserRole } from '../models/role.model';
import { RoleService } from './role.service';

describe('RoleService', () => {
  let httpClient: HttpClient;
  let service: RoleService;

  beforeEach(() => {
    httpClient = <any>{
      get: _ => of({})
    };

    service = new RoleService(httpClient);
  });

  it('should get roles', done => {
    const roles: UserRole[] = [
      { id: 1, name: 'Administrators' },
      { id: 2, name: 'Users' }
    ];

    spyOn(httpClient, 'get').and.returnValue(of(roles));

    service.getRoles().subscribe(result => {
      expect(httpClient.get).toHaveBeenCalledWith(environment.apiEndpoint + '/roles');
      expect(result.length).toBe(2);
      done();
    });
  });
});
