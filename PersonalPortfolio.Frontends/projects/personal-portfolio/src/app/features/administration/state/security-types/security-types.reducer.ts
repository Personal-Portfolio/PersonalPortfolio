import { createEntityAdapter, EntityAdapter, EntityState } from '@ngrx/entity';

import { SecurityType } from './security-type';
import { actionSecurityTypesUpsertMany } from './security-types.actions';
import { Action, createReducer, on } from '@ngrx/store';

export function sort(a: SecurityType, b: SecurityType): number {
    return a.id.localeCompare(b.id);
}

export const securitiesTypesAdapter: EntityAdapter<SecurityType> = createEntityAdapter<SecurityType>({
    sortComparer: sort
});

export const initialState: EntityState<SecurityType> = securitiesTypesAdapter.getInitialState({
    ids: [],
    entities: {}
});

const reducer = createReducer(
    initialState,
    on(actionSecurityTypesUpsertMany, (state, { securityTypes }) =>
        securitiesTypesAdapter.upsertMany(securityTypes, state)
    )
);
export function securityTypesReducer(state: EntityState<SecurityType> | undefined, action: Action) {
    return reducer(state, action);
}
