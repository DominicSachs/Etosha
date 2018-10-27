import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { UserRole } from '../models/role.model';

@Injectable()
export class RoleService {
  constructor(private httpClient: HttpClient) { }

  getRoles(): Observable<UserRole[]> {
    return this.httpClient.get<UserRole[]>(`${environment.apiEndpoint}/roles`);
  }
}
