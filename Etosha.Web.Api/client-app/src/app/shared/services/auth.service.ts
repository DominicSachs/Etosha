
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject ,  Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from '../../../environments/environment';
import { LoginModel } from '../models/login.model';
import { TokenModel } from '../models/token.model';
import { BaseService } from './base.service';

@Injectable()
export class AuthService extends BaseService {
    isLoginSubject = new BehaviorSubject<boolean>(this.isAuthenticated());

    constructor(private httpClient: HttpClient) {
        super();
    }

    isLoggedIn(): Observable<boolean> {
        return this.isLoginSubject.asObservable();
    }

    isAuthenticated(): boolean {
        return !!localStorage.getItem('auth_token');
        // Check whether the token is expired and return
        // return !this.jwtHelper.isTokenExpired(token);
    }

    getToken(): string {
        return localStorage.getItem('auth_token');
    }

    login(model: LoginModel): Observable<boolean> {
        return this.httpClient.post<TokenModel>(`${environment.apiEndpoint}/auth/login`, model).pipe(
            map((result: TokenModel) => {
                if (result.token) {
                    localStorage.setItem('auth_token', result.token);
                    this.isLoginSubject.next(true);
                    return true;
                } else {
                    this.isLoginSubject.next(false);
                    return false;
                }
            }));
    }

    logout() {
        localStorage.removeItem('auth_token');
        this.isLoginSubject.next(false);
    }
}
