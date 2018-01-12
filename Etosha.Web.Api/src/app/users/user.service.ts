import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { User } from './user.model';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';

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
    if (user.id === 0) {
      return this.httpClient.post<User>(`${environment.apiEndpoint}/users`, user);
    }

    return this.httpClient.put<User>(`${environment.apiEndpoint}/users/${user.id}`, user);
  }
}
