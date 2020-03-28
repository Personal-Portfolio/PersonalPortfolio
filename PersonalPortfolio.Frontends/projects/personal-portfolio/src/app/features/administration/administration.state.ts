import { ActionReducerMap, createFeatureSelector } from '@ngrx/store';

import { AppState } from '../../core/core.module';
import { CurrencyState } from './currencies/currency';
import { currenciesReducer } from './currencies/currencies.reducer';
import { securitiesReducer } from './securities/securities.reducer';
import { SecurityState } from './securities/security';

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
  currencies: CurrencyState;
  securities: SecurityState;
//   dataProviders: DataProvidersState;
}

export interface State extends AppState {
  administration: AdministrationState;
}
