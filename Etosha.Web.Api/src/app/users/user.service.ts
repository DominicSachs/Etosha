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
}
