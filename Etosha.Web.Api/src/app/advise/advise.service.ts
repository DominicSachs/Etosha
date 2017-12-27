import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';

@Injectable()
export class AdviseService {
    constructor(private httpClient: HttpClient) { }

    getAdvice(id: string): any {
        return this.httpClient.get(`${environment.apiEndpoint}/advises/${id}`);
    }
}