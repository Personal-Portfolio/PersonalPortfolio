import { v4 as uuid } from 'uuid';
import { Router } from '@angular/router';
import { FormBuilder, NgForm } from '@angular/forms';
import { Component, ChangeDetectionStrategy } from '@angular/core';
import { Store, select } from '@ngrx/store';
import { Observable } from 'rxjs';

import { ROUTE_ANIMATIONS_ELEMENTS } from '../../../../core/core.module';

import { State } from '../../administration.state';
import { Security } from '../security';
import { selectAllSecurities, selectSelectedSecurities } from '../../securities/securities.selectors';
import { actionSecuritiesDeleteOne, actionSecuritiesUpsertOne } from '../securities.actions';
@Component({
    selector: 'personal-portfolio-securities',
    templateUrl: './securities.component.html',
    styleUrls: ['./securities.component.scss'],
    changeDetection: ChangeDetectionStrategy.OnPush
})
export class SecuritiesComponent {

    routeAnimationsElements = ROUTE_ANIMATIONS_ELEMENTS;

    securityFormGroup = this.fb.group(SecuritiesComponent.createSecurity());
    securities$: Observable<Security[]> = this.store.pipe(select(selectAllSecurities));
    selectedSecurity$: Observable<Security> = this.store.pipe(select(selectSelectedSecurities));

    isEditing: boolean;

    static createSecurity(): Security {
        return { id: uuid(), code: '', description: '' };
    }

    constructor(
        public store: Store<State>,
        public fb: FormBuilder,
        private router: Router
    ) { }

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
