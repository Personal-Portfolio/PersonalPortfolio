import { createEntityAdapter, EntityAdapter } from '@ngrx/entity';

import { Currency, CurrencyState } from './currency';
import { actionCurrenciesUpsertOne, actionCurrenciesUpsertMany, actionCurrenciesDeleteOne } from './currencies.actions';
import { Action, createReducer, on } from '@ngrx/store';

export function sort(a: Currency, b: Currency): number {
    return a.id.localeCompare(b.id);
}

export const currenciesAdapter: EntityAdapter<Currency> = createEntityAdapter<Currency>({
    sortComparer: sort
});

export const initialState: CurrencyState = currenciesAdapter.getInitialState({
    ids: [],
    entities: {}
});

const reducer = createReducer(
    initialState,
    on(actionCurrenciesUpsertOne, (state, { currency }) =>
        currenciesAdapter.upsertOne(currency, state)
    ),
    on(actionCurrenciesUpsertMany, (state, { currencies }) =>
        currenciesAdapter.upsertMany(currencies, state)
    ),
    on(actionCurrenciesDeleteOne, (state, { id }) => currenciesAdapter.removeOne(id, state))
);

export function currenciesReducer(state: CurrencyState | undefined, action: Action) {
    return reducer(state, action);
}
