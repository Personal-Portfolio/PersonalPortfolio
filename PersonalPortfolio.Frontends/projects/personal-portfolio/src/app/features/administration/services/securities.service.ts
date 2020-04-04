import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { Security } from '../state/securities/security';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators'

@Injectable()
export class SecuritiesService {
    constructor(private http: HttpClient) { }

    getAll(): Observable<Security[]> {
        return this.http.get("http://localhost:4010/api/securities").pipe(
            map((items: {code: string, description: string, type: string, currency: string}[]) => items.map(i => {
                return { id: i.code, description: i.description, type: i.type, currency: i.currency}
            }))
        );
    }

    getByCode(symbol: string): Observable<Security> {
        return this.http.get(`http://localhost:4010/api/securities/${symbol}`).pipe(
            map((item: {code: string, description: string, type: string, currency: string}) => {
                return { id: item.code, description: item.description, type: item.type, currency: item.currency }
            })
        );
    }
}
