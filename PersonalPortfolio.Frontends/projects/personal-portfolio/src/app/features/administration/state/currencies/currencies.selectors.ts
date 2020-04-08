import { createSelector } from '@ngrx/store';

import { selectRouterState } from '../../../../core/core.module';
import { selectAdministration, AdministrationState } from '../administration.state';

import { currenciesAdapter } from './currencies.reducer';

const { selectEntities, selectAll } = currenciesAdapter.getSelectors();

export const selectCurrencies = createSelector(
    selectAdministration,
    (state: AdministrationState) => state.currencies
);
export const selectAllCurrencies = createSelector(selectCurrencies, selectAll);
export const selectCurrenciesEntities = createSelector(selectCurrencies, selectEntities);
export const selectSelectedCurrencies = createSelector(
    selectCurrenciesEntities,
    selectRouterState,
    (entities, params) => params && entities[params.state.params.id]
);

export const selectCurrecy = createSelector(
    selectCurrenciesEntities, 
    currencies => (id: string) => currencies[id]
  );
