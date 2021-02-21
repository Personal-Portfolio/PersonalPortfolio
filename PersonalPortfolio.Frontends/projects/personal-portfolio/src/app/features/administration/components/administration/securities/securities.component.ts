import { Router } from '@angular/router';
import { FormBuilder, NgForm } from '@angular/forms';
import { Component, ChangeDetectionStrategy, OnInit } from '@angular/core';
import { Store, select } from '@ngrx/store';
import { Observable } from 'rxjs';

import { ROUTE_ANIMATIONS_ELEMENTS } from '../../../../../core/core.module';

import { State } from '../../../state/administration.state';
import { Security } from '../../../state/securities/security';
import { SecurityType } from '../../../state/security-types/security-type';

import { actionSecuritiesRequestAll, actionSecuritiesDeleteOne, actionSecuritiesUpsertOne } from '../../../state/securities/securities.actions';
import { actionSecurityTypesRequestAll } from '../../../state/security-types/security-types.actions';

import { selectAllSecurities, selectSelectedSecurities } from '../../../state/securities/securities.selectors';
import { selectAllSecurityTypes, selectSecurityType } from '../../../state/security-types/security-types.selectors';
import { Currency } from '../../../state/currencies/currency';
import { selectAllCurrencies, selectCurrecy } from '../../../state/currencies/currencies.selectors';
import { actionCurrenciesRequestAll } from '../../../state/currencies/currencies.actions';

@Component({
    selector: 'personal-portfolio-securities',
    templateUrl: './securities.component.html',
    styleUrls: ['./securities.component.scss'],
    changeDetection: ChangeDetectionStrategy.OnPush
})
export class SecuritiesComponent implements OnInit {
    routeAnimationsElements = ROUTE_ANIMATIONS_ELEMENTS;
    securityFormGroup = this.fb.group(SecuritiesComponent.createSecurity());
    securities$: Observable<Security[]> = this.store.pipe(select(selectAllSecurities));
    currencies$: Observable<Currency[]> = this.store.pipe(select(selectAllCurrencies));
    securityTypes$: Observable<SecurityType[]> = this.store.pipe(select(selectAllSecurityTypes));
    selectedSecurity$: Observable<Security> = this.store.pipe(select(selectSelectedSecurities));
    securityTypeSelection$: Observable<(id: string) => SecurityType> = this.store.pipe(select(selectSecurityType));
    currencySelection$: Observable<(id: string) => Currency> = this.store.pipe(select(selectCurrecy));

    isEditing: boolean;

    static createSecurity(): Security {
        return { id: '', description: '', type: 'Equity', currency: 'USD' };
    }


    constructor(
        private store: Store<State>,
        public fb: FormBuilder,
        private router: Router
    ) {
        this.isEditing = false;
    }

    ngOnInit(): void {
        this.store.dispatch(actionSecuritiesRequestAll());
        this.store.dispatch(actionSecurityTypesRequestAll());
        this.store.dispatch(actionCurrenciesRequestAll());
    }

    select(security: Security) {
        this.isEditing = false;
        this.router.navigate(['admin/securities', security.id]);
    }

    deselect() {
        this.isEditing = false;
        this.router.navigate(['admin/securities']);
    }

    edit(security: Security) {
        this.isEditing = true;
        this.securityFormGroup.setValue(security);
    }

    addNew(securityForm: NgForm) {
        securityForm.resetForm();
        this.securityFormGroup.reset();
        this.securityFormGroup.setValue(SecuritiesComponent.createSecurity());
        this.isEditing = true;
    }

    cancelEditing() {
        this.isEditing = false;
    }

    delete(security: Security) {
        this.store.dispatch(actionSecuritiesDeleteOne({ id: security.id }));
        this.isEditing = false;
        this.router.navigate(['admin/securities']);
    }

    save() {
        if (!this.securityFormGroup.valid) {
            return;
        }

        const security = this.securityFormGroup.value;
        this.store.dispatch(actionSecuritiesUpsertOne({ security }));
        this.isEditing = false;
        this.router.navigate(['admin/securities', security.id]);
    }
}
