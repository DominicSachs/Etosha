import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { LoginModel } from '../models/login.model';
import { TokenModel } from '../models/token.model';
import { BaseService } from './base.service';
import 'rxjs/add/operator/map';
import { Observable } from 'rxjs/Observable';
import { BehaviorSubject } from 'rxjs/BehaviorSubject';

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
        return this.httpClient.post<TokenModel>(`${environment.apiEndpoint}/auth/login`, model)
            .map((result: TokenModel) => {
                if (result.token) {
                    localStorage.setItem('auth_token', result.token);
                    this.isLoginSubject.next(true);
                    return true;
                } else {
                    this.isLoginSubject.next(false);
                    return false;
                }
            });
    }

    logout() {
        localStorage.removeItem('auth_token');
        this.isLoginSubject.next(false);
    }
}
