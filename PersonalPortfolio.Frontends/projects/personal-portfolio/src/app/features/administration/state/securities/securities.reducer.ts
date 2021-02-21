import { createEntityAdapter, EntityAdapter, EntityState } from '@ngrx/entity';

import { Security } from './security';
import { actionSecuritiesUpsertOne, actionSecuritiesUpsertMany, actionSecuritiesDeleteOne } from './securities.actions';
import { Action, createReducer, on } from '@ngrx/store';

export function sort(a: Security, b: Security): number {
    return a.id.localeCompare(b.id);
}

export const securitiesAdapter: EntityAdapter<Security> = createEntityAdapter<Security>({
    sortComparer: sort
});

export const initialState: EntityState<Security> = securitiesAdapter.getInitialState({
    ids: [],
    entities: {}
});

const reducer = createReducer(
    initialState,
    on(actionSecuritiesUpsertOne, (state, { security }) =>
        securitiesAdapter.upsertOne(security, state)
    ),
    on(actionSecuritiesUpsertMany, (state, { securities }) =>
        securitiesAdapter.upsertMany(securities, state)
    ),
    on(actionSecuritiesDeleteOne, (state, { id }) => securitiesAdapter.removeOne(id, state))
);

export function securitiesReducer(state: EntityState<Security> | undefined, action: Action) {
    return reducer(state, action);
}
