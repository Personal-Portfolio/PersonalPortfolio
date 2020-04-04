import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { SecurityType } from '../state/security-types/security-type';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators'

@Injectable()
export class SecurityTypesService {
    constructor(private http: HttpClient) { }

    getAll(): Observable<SecurityType[]> {
        return this.http.get("http://localhost:4010/api/security-types").pipe(
            map((items: string[]) => items.map(i => {
                return { id: i, type: i}
            }))
        );
    }
}
