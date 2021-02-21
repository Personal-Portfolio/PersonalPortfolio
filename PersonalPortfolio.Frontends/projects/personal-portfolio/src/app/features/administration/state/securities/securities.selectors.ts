import { createSelector } from '@ngrx/store';

import { selectRouterState } from '../../../../core/core.module';
import { selectAdministration, AdministrationState } from '../administration.state';

import { securitiesAdapter } from './securities.reducer';

const { selectEntities, selectAll } = securitiesAdapter.getSelectors();

export const selectSecurities = createSelector(
    selectAdministration,
    (state: AdministrationState) => state.securities
);
export const selectAllSecurities = createSelector(selectSecurities, selectAll);
export const selectSecuritiesEntities = createSelector(selectSecurities, selectEntities);
export const selectSelectedSecurities = createSelector(
    selectSecuritiesEntities,
    selectRouterState,
    (entities, params) => params && entities[params.state.params.id]
);
