import { Injectable } from '@angular/core';
import { Store } from '@ngrx/store';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { map, switchMap, catchError } from 'rxjs/operators';

import { State } from '../administration.state';
import { actionCurrenciesRequestAll, actionCurrenciesFailRequest, actionCurrenciesUpsertMany } from './currencies.actions';
import { CurrenciesService } from '../../services/currencies.service';
import { of } from 'rxjs';

@Injectable()
export class CurrenciesEffects {
  constructor(
    private actions$: Actions,
    private store: Store<State>,
    private currenciesService: CurrenciesService
  ) {}

  loadCurrencies = createEffect(
    () => this.actions$
            .pipe(
                ofType(actionCurrenciesRequestAll),
                switchMap(actions => this.currenciesService.getAll()
                    .pipe(
                        map(items => actionCurrenciesUpsertMany({ currencies: items }),
                        catchError(error =>
                            of(actionCurrenciesFailRequest({ error: error }))
                          )
                        )
                    )
                )
            ),
    { dispatch: true }
  );
}
