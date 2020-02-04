import { ActionReducerMap, createFeatureSelector } from '@ngrx/store';

import { AppState } from '../../core/core.module';
import { CurrencyState } from './currencies/currency';
import { currenciesReducer } from './currencies/currencies.reducer';

export const FEATURE_NAME = 'administration';
export const selectAdministration = createFeatureSelector<State, AdministrationState>(
  FEATURE_NAME
);
export const reducers: ActionReducerMap<AdministrationState> = {
    currencies: currenciesReducer
    // securities: securitiesReducer,
    // dataProviders: dataProvidersReducer
};

export interface AdministrationState {
  currencies: CurrencyState;
//   securities: SecuritiesState;
//   dataProviders: DataProvidersState;
}

export interface State extends AppState {
  administration: AdministrationState;
}
