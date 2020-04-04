import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Currency } from '../state/currencies/currency';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators'

@Injectable()
export class CurrenciesService {

    constructor(private http: HttpClient) { }

    getAll(): Observable<Currency[]> {
        return this.http.get("http://localhost:4010/api/currencies").pipe(
            map((items: { code: string, description: string }[]) => items.map(i => {
                return { id: i.code, description: i.description }
            }))
        );
    }

    getByCode(symbol: string): Observable<Currency> {
        return this.http.get(`http://localhost:4010/api/currencies/${symbol}`).pipe(
            map((item: { code: string, description: string }) => {
                return { id: item.code, description: item.description }
            })
        );
    }
}
