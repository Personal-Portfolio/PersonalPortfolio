import { Injectable } from '@angular/core';
import { Store } from '@ngrx/store';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { map, switchMap, catchError } from 'rxjs/operators';

import { State } from '../administration.state';
import { actionSecurityTypesRequestAll, actionSecurityTypesFailRequest, actionSecurityTypesUpsertMany } from './security-types.actions';
import { SecurityTypesService } from '../../services/securities-types.service';
import { of } from 'rxjs';

@Injectable()
export class SecurityTypesEffects {
  constructor(
    private actions$: Actions,
    private store: Store<State>,
    private securityTypesService: SecurityTypesService
  ) {}

  loadCurrencies = createEffect(
    () => this.actions$
            .pipe(
                ofType(actionSecurityTypesRequestAll),
                switchMap(actions => this.securityTypesService.getAll()
                    .pipe(
                        map(items => actionSecurityTypesUpsertMany({ securityTypes: items }),
                        catchError(error =>
                            of(actionSecurityTypesFailRequest({ error: error }))
                          )
                        )
                    )
                )
            ),
    { dispatch: true }
  );
}
