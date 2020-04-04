import { Router } from '@angular/router';
import { FormBuilder, NgForm } from '@angular/forms';
import { Component, ChangeDetectionStrategy, OnInit } from '@angular/core';
import { Store, select } from '@ngrx/store';
import { Observable } from 'rxjs';

import { ROUTE_ANIMATIONS_ELEMENTS } from '../../../../core/core.module';

import { State } from '../../state/administration.state';
import { Currency } from '../../state/currencies/currency';
import { actionCurrenciesRequestAll, actionCurrenciesDeleteOne, actionCurrenciesUpsertOne } from '../../state/currencies/currencies.actions';
import { selectAllCurrencies, selectSelectedCurrencies } from '../../state/currencies/currencies.selectors';


@Component({
    selector: 'personal-portfolio-currencies',
    templateUrl: './currencies.component.html',
    styleUrls: ['./currencies.component.scss'],
    changeDetection: ChangeDetectionStrategy.OnPush
})
export class CurrenciesComponent implements OnInit{
    routeAnimationsElements = ROUTE_ANIMATIONS_ELEMENTS;

    currencyFormGroup = this.fb.group(CurrenciesComponent.createCurrency());
    currencies$: Observable<Currency[]> = this.store.pipe(select(selectAllCurrencies));
    selectedCurrency$: Observable<Currency> = this.store.pipe(select(selectSelectedCurrencies));

    isEditing: boolean;

    static createCurrency(): Currency {
        return { id: '', description: '' };
    }

    constructor(
        public store: Store<State>,
        public fb: FormBuilder,
        private router: Router
    ) {
        this.isEditing = false;
    }
    ngOnInit(): void {
        this.store.dispatch(actionCurrenciesRequestAll());
    }

    select(currency: Currency) {
        this.isEditing = false;
        this.router.navigate(['admin/currencies', currency.id]);
    }

    deselect() {
        this.isEditing = false;
        this.router.navigate(['admin/currencies']);
    }

    edit(currency: Currency) {
        this.isEditing = true;
        this.currencyFormGroup.setValue(currency);
    }

    addNew(currencyForm: NgForm) {
        currencyForm.resetForm();
        this.currencyFormGroup.reset();
        this.currencyFormGroup.setValue(CurrenciesComponent.createCurrency());
        this.isEditing = true;
    }

    cancelEditing() {
        this.isEditing = false;
    }

    delete(currency: Currency) {
        this.store.dispatch(actionCurrenciesDeleteOne({ id: currency.id }));
        this.isEditing = false;
        this.router.navigate(['admin/currencies']);
    }

    save() {
        if (!this.currencyFormGroup.valid) {
            return;
        }

        const currency = this.currencyFormGroup.value;
        this.store.dispatch(actionCurrenciesUpsertOne({ currency }));
        this.isEditing = false;
        this.router.navigate(['admin/currencies', currency.id]);
    }
}
