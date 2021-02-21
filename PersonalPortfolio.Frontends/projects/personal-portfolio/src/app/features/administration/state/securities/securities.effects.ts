import { Injectable } from '@angular/core';
import { Store } from '@ngrx/store';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { map, switchMap } from 'rxjs/operators';

import { State } from '../administration.state';
import { actionSecuritiesRequestAll, actionSecuritiesUpsertMany } from './securities.actions';
import { SecuritiesService } from '../../services/securities.service';

@Injectable()
export class SecuritiesEffects {
  constructor(
    private actions$: Actions,
    private store: Store<State>,
    private securitiesService: SecuritiesService
  ) {}

  loadCurrencies = createEffect(
    () => this.actions$
            .pipe(
                ofType(actionSecuritiesRequestAll),
                switchMap(actions => this.securitiesService.getAll()
                    .pipe(
                        map(items => actionSecuritiesUpsertMany( { securities: items }))
                    )
                )
            ),
    { dispatch: true }
  );
}
