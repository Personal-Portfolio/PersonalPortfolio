import { Router } from '@angular/router';
import { FormBuilder, NgForm } from '@angular/forms';
import { Component, ChangeDetectionStrategy } from '@angular/core';
import { Store, select } from '@ngrx/store';
import { Observable } from 'rxjs';

import { ROUTE_ANIMATIONS_ELEMENTS } from '../../../../core/core.module';

import { State } from '../../administration.state';
import { Currency } from '../currency';
import { actionCurrenciesUpsertOne, actionCurrenciesDeleteOne, actionCurrenciesLoad } from '../currencies.actions';
import { selectSelectedCurrencies, selectAllCurrencies } from '../currencies.selectors';

@Component({
    selector: 'personal-portfolio-currencies',
    templateUrl: './currencies.component.html',
    styleUrls: ['./currencies.component.scss'],
    changeDetection: ChangeDetectionStrategy.OnPush
})
export class CurrenciesComponent {
    routeAnimationsElements = ROUTE_ANIMATIONS_ELEMENTS;

    currencyFormGroup = this.fb.group(CurrenciesComponent.createCurrency());
    currencies$: Observable<Currency[]> = this.store.pipe(select(selectAllCurrencies));
    selectedCurrency$: Observable<Currency> = this.store.pipe(select(selectSelectedCurrencies));

    isEditing: boolean;

    static createCurrency(): Currency {
        return { id: 'USD', description: '' };
    }

    constructor(
        public store: Store<State>,
        public fb: FormBuilder,
        private router: Router
    ) {
        this.store.dispatch(actionCurrenciesLoad());
        this.isEditing = false;
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
