import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { User } from './user.model';

@Injectable()
export class UserService {
  constructor(private httpClient: HttpClient) { }

  getUsers(): Observable<User[]> {
    return this.httpClient.get<User[]>(`${environment.apiEndpoint}/users`);
  }

  getUser(id: number): Observable<User> {
    return this.httpClient.get<User>(`${environment.apiEndpoint}/users/${id}`);
  }

  saveUser(user: User): Observable<User> {
    if (user.id) {
      return this.httpClient.put<User>(`${environment.apiEndpoint}/users/${user.id}`, user);
    }

    return this.httpClient.post<User>(`${environment.apiEndpoint}/users`, user);
  }

  deleteUser(user: User): Observable<any> {
    return this.httpClient.delete(`${environment.apiEndpoint}/users/${user.id}`);
  }
}
