import { DataSource, CollectionViewer } from '@angular/cdk/collections';
import { SecurityType } from '../../../state/security-types/security-type';
import { BehaviorSubject, Observable } from 'rxjs';
import { Store, select } from '@ngrx/store';
import { State } from '../../../state/administration.state';
import { selectAllSecurityTypes } from '../../../state/security-types/security-types.selectors';

export class SecurityTypesDataSource implements DataSource<SecurityType> {

    private dataSubject = new BehaviorSubject<SecurityType[]>([]);
    private loadingSubject = new BehaviorSubject<boolean>(false);

    public loading$ = this.loadingSubject.asObservable();

    constructor(private store: Store<State>) { }

    connect(collectionViewer: CollectionViewer): Observable<SecurityType[]> {
        return this.store.pipe(select(selectAllSecurityTypes));
    }

    disconnect(collectionViewer: CollectionViewer): void {
        this.dataSubject.complete();
        this.loadingSubject.complete();
    } 
}