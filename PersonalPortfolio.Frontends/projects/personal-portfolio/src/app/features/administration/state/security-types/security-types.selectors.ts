import { createSelector } from '@ngrx/store';

import { selectAdministration, AdministrationState } from '../administration.state';

import { securitiesTypesAdapter } from './security-types.reducer';

const { selectEntities, selectAll } = securitiesTypesAdapter.getSelectors();

export const selectSecurityTypes = createSelector(
    selectAdministration,
    (state: AdministrationState) => state.securityTypes
);
export const selectAllSecurityTypes = createSelector(selectSecurityTypes, selectAll);
export const selectSecurityTypeEntities = createSelector(selectSecurityTypes, selectEntities);
export const selectSecurityType = createSelector(
    selectSecurityTypeEntities, 
    securityTypes => (id: string) => securityTypes[id]
  );
