import { createAction, props } from '@ngrx/store';
import { SecurityType } from './security-type';

export const actionSecurityTypesRequestAll = createAction(
    '[Security Types] Load All'
);

export const actionSecurityTypesUpsertMany = createAction(
    '[Security Types] Upsert Many', props<{ securityTypes: SecurityType[] }>()
);

export const actionSecurityTypesFailRequest = createAction(
    '[Security Types] Do on load failed', props<{ error: Error }>()
);

export const actionSecurityTypesRequestCancelRequest = createAction(
    '[Security Types] Cancel load'
);