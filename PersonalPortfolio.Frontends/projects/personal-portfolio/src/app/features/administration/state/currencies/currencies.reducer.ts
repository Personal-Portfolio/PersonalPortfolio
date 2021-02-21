import { createEntityAdapter, EntityAdapter, EntityState } from '@ngrx/entity';

import { Currency } from './currency';
import { actionCurrenciesUpsertOne, actionCurrenciesUpsertMany, actionCurrenciesDeleteOne } from './currencies.actions';
import { Action, createReducer, on } from '@ngrx/store';

export function sort(a: Currency, b: Currency): number {
    return a.id.localeCompare(b.id);
}

export const currenciesAdapter: EntityAdapter<Currency> = createEntityAdapter<Currency>({
    sortComparer: sort
});

export const initialState: EntityState<Currency> = currenciesAdapter.getInitialState({
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

export function currenciesReducer(state: EntityState<Currency> | undefined, action: Action) {
    return reducer(state, action);
}
