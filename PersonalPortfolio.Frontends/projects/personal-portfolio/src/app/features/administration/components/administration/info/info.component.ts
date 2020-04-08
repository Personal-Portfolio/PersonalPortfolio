import { Component, OnInit, ChangeDetectionStrategy } from '@angular/core';
import { ROUTE_ANIMATIONS_ELEMENTS } from '../../../../../core/core.module';
import { Store } from '@ngrx/store';

import { State } from '../../../state/administration.state';
import { actionSecurityTypesRequestAll } from '../../../state/security-types/security-types.actions';
import { SecurityTypesDataSource } from './security-types.datasource';

@Component({
    selector: 'personal-portfolio-info',
    templateUrl: './info.component.html',
    styleUrls: ['./info.component.scss'],
    changeDetection: ChangeDetectionStrategy.OnPush
})
export class InfoComponent implements OnInit {

    displayedColumns = ['type', 'category', 'description'];
    securityTypesDataSource: SecurityTypesDataSource;
    routeAnimationsElements = ROUTE_ANIMATIONS_ELEMENTS;
    constructor(
        private store: Store<State>,
    ) {
        this.securityTypesDataSource = new SecurityTypesDataSource(store);
    }

    ngOnInit(): void {
        this.store.dispatch(actionSecurityTypesRequestAll());
    }

}
