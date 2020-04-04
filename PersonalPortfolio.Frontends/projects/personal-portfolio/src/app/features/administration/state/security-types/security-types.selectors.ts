import { createSelector } from '@ngrx/store';

import { selectRouterState } from '../../../../core/core.module';
import { selectAdministration, AdministrationState } from '../administration.state';

import { securitiesTypesAdapter } from './security-types.reducer';

const { selectEntities, selectAll } = securitiesTypesAdapter.getSelectors();

export const selectSecurityTypes = createSelector(
    selectAdministration,
    (state: AdministrationState) => state.securityTypes
);
export const selectAllSecurityTypes = createSelector(selectSecurityTypes, selectAll);
export const selectSecurityTypeEntities = createSelector(selectSecurityTypes, selectEntities);
export const selectSelectedSecurities = createSelector(
    selectSecurityTypeEntities,
    selectRouterState,
    (entities, params) => params && entities[params.state.params.id]
);
