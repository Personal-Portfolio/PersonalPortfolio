import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { Currency } from '../currencies/currency';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class CurrenciesService {
    private static Repo: Currency[] = [
        {
            id: 'USD', description: 'United States dollar'
        },
        {
            id: 'EUR', description: 'EURO'
        },
        {
            id: 'RUB', description: 'Russian Rubble'
        }];

    constructor(private http: HttpClient) { }

    getAll(): Observable<Currency[]> {
        const result = this.buildArray();
        return of(result);
    }

    getByCode(symbol: string): Observable<Currency> {
        const result = this.buildResult(symbol);
        return of(result);
    }

    private buildResult(symbol: string): Currency {
        return CurrenciesService.Repo.find(c => c.id === symbol);
    }

    private buildArray(): Currency[] {
        return CurrenciesService.Repo; 
    }
}
