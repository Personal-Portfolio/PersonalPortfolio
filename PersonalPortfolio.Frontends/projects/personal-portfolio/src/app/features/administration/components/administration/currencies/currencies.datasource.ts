import { DataSource, CollectionViewer } from '@angular/cdk/collections';
import { BehaviorSubject, Observable } from 'rxjs';
import { Store, select } from '@ngrx/store';
import { State } from '../../../state/administration.state';
import { Currency } from '../../../state/currencies/currency';
import { selectAllCurrencies } from '../../../state/currencies/currencies.selectors';

export class CurrenciesDataSource implements DataSource<Currency> {

    private dataSubject = new BehaviorSubject<Currency[]>([]);
    private loadingSubject = new BehaviorSubject<boolean>(false);

    public loading$ = this.loadingSubject.asObservable();

    constructor(private store: Store<State>) { }

    connect(collectionViewer: CollectionViewer): Observable<Currency[]> {
        return this.store.pipe(select(selectAllCurrencies));
    }

    disconnect(collectionViewer: CollectionViewer): void {
        this.dataSubject.complete();
        this.loadingSubject.complete();
    } 
}