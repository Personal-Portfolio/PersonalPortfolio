import { ActionReducerMap, createFeatureSelector } from '@ngrx/store';

import { AppState } from '../../core/core.module';
import { Currency } from './currencies/currency';
import { Security } from './securities/security';
import { currenciesReducer } from './currencies/currencies.reducer';
import { securitiesReducer } from './securities/securities.reducer';
import { EntityState } from '@ngrx/entity';

export const FEATURE_NAME = 'administration';
export const selectAdministration = createFeatureSelector<State, AdministrationState>(
  FEATURE_NAME
);
export const reducers: ActionReducerMap<AdministrationState> = {
    currencies: currenciesReducer,
    securities: securitiesReducer
    // dataProviders: dataProvidersReducer
};

export interface AdministrationState {
  currencies: EntityState<Currency>;
  securities: EntityState<Security>;
//   dataProviders: DataProvidersState;
}

export interface State extends AppState {
  administration: AdministrationState;
}
