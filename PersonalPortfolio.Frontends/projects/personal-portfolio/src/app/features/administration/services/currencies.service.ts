import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { Currency } from '../currencies/currency';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators'

@Injectable()
export class CurrenciesService {
    private static DB: Object = {
        _ids: ['USD', 'EUR', 'RUB'],
        USD: 'United States dollar',
        EUR: 'EURO',
        RUB: 'Russian Rubble'
    };

    constructor(private http: HttpClient) { }


    getAll(): Observable<Currency[]> {
        return this.http.get("http://localhost:4010/api/currencies").pipe(
            map((items: {code: string, description: string}[]) => items.map(i => {
                return { id: i.code, description: i.description}
            }))
        )
    }

    getByCode(symbol: string): Observable<Currency> {
        const result = this.buildResult(symbol);
        return of(result);
    }

    private buildResult(symbol: string): Currency {
        return CurrenciesService.DB[symbol];
    }

    private buildArray(): Currency[] {
        let ids = CurrenciesService.DB['_ids'] as Array<string>;
        
        return ids.map(i => {
            return { id: i, description: CurrenciesService.DB[i]} as Currency;
        });
    }
}
