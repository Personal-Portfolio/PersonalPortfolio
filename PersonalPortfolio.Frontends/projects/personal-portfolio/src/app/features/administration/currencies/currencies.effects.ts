import { Injectable } from '@angular/core';
import { Store } from '@ngrx/store';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { map, switchMap } from 'rxjs/operators';

import { State } from '../administration.state';
import { actionCurrenciesLoad, actionCurrenciesUpsertMany } from './currencies.actions';
import { CurrenciesService } from '../services/currencies.service';

export const BOOKS_KEY = 'EXAMPLES.BOOKS';

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
                ofType(actionCurrenciesLoad),
                switchMap(actions => this.currenciesService.getAll()
                    .pipe(
                        map(items => actionCurrenciesUpsertMany( { currencies: items }))
                    )
                )
            ),
    { dispatch: true }
  );
}
