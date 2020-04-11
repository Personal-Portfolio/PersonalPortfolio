import { Router } from '@angular/router';
import { FormBuilder, NgForm } from '@angular/forms';
import { Component, ChangeDetectionStrategy, OnInit, OnDestroy } from '@angular/core';
import { Store, select } from '@ngrx/store';
import { Observable, BehaviorSubject, Subscription } from 'rxjs';

import { ROUTE_ANIMATIONS_ELEMENTS } from '../../../../../core/core.module';

import { State } from '../../../state/administration.state';
import { Currency } from '../../../state/currencies/currency';
import { actionCurrenciesRequestAll, actionCurrenciesDeleteOne, actionCurrenciesUpsertOne } from '../../../state/currencies/currencies.actions';
import { selectAllCurrencies, selectSelectedCurrencies } from '../../../state/currencies/currencies.selectors';
import { CurrenciesDataSource } from './currencies.datasource';
import { trigger, state, style, transition, animate } from '@angular/animations';
import { withLatestFrom } from 'rxjs/operators';


@Component({
    selector: 'personal-portfolio-currencies',
    templateUrl: './currencies.component.html',
    styleUrls: ['./currencies.component.scss'],
    animations: [
        trigger('detailExpand', [
          state('collapsed', style({height: '0px', minHeight: '0'})),
          state('expanded', style({height: '*'})),
          transition('expanded <=> collapsed', animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')),
        ]),
      ],
    changeDetection: ChangeDetectionStrategy.OnPush
})
export class CurrenciesComponent implements OnInit, OnDestroy{
    routeAnimationsElements = ROUTE_ANIMATIONS_ELEMENTS;

    currencyFormGroup = this.fb.group(CurrenciesComponent.createCurrency());
    currencies$: Observable<Currency[]> = this.store.pipe(select(selectAllCurrencies));
    selectedCurrency$: Observable<Currency> = this.store.pipe(select(selectSelectedCurrencies));

    currenciesDataSource: CurrenciesDataSource;

    private _rowClick = new BehaviorSubject<Currency>(null);
    private rowClick$: Observable<Currency> = this._rowClick.asObservable();
    private rowClickSubscription: Subscription; // TODO: get rid of this

    static createCurrency(): Currency {
        return { id: '', description: '' };
    }

    displayedColumns = ['id', 'description'];
    displayedColumnsNames = {
        id: 'personal-portfolio.administration.currency.code-placeholder',
        description: 'personal-portfolio.administration.currency.description-placeholder'
    };

    constructor(
        public store: Store<State>,
        public fb: FormBuilder,
        private router: Router
    ) {
        this.currenciesDataSource = new CurrenciesDataSource(store);
    }
    ngOnDestroy(): void {
        this.rowClickSubscription.unsubscribe();
    }
    ngOnInit(): void {
        this.store.dispatch(actionCurrenciesRequestAll());
        this.rowClickSubscription = this.rowClick$.pipe(withLatestFrom(this.selectedCurrency$))
            .subscribe(([click, selection]) => {
                if(click && selection && selection.id === click.id) {
                    this.deselect();
                } else if (click){
                    this.select(click);
                }
            });
    }

    onRowClick(currency: Currency) {
        this._rowClick.next(currency);
    }

    select(currency: Currency) {
        this.currencyFormGroup.setValue(currency);
        this.router.navigate(['admin/currencies', currency.id]);
    }

    deselect() {
        this.router.navigate(['admin/currencies']);
    }

    addNew(currencyForm: NgForm) {
        currencyForm.resetForm();
        this.currencyFormGroup.reset();
        this.currencyFormGroup.setValue(CurrenciesComponent.createCurrency());
    }

    delete(currency: Currency) {
        this.store.dispatch(actionCurrenciesDeleteOne({ id: currency.id }));
        this.router.navigate(['admin/currencies']);
    }

    save() {
        if (!this.currencyFormGroup.valid) {
            return;
        }

        const currency = this.currencyFormGroup.value;
        this.store.dispatch(actionCurrenciesUpsertOne({ currency }));
        this.router.navigate(['admin/currencies', currency.id]);
    }
}
