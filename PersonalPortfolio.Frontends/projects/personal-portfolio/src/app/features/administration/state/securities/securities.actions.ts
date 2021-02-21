import { createAction, props } from '@ngrx/store';
import { Security } from './security';

export const actionSecuritiesUpsertOne = createAction(
    '[Securities] Upsert One', props<{ security: Security }>()
);

export const actionSecuritiesUpsertMany = createAction(
    '[Securities] Upsert Many', props<{ securities: Security[] }>()
);

export const actionSecuritiesDeleteOne = createAction(
    '[Securities] Delete One', props<{ id: string }>()
);

export const actionSecuritiesRequestAll= createAction(
    '[Securities] Load All'
);

export const actionSecuritiesFailRequest = createAction(
    '[Securities] Do on load failed', props<{ error: Error }>()
);

export const actionSecuritiesRequestCancelRequest = createAction(
    '[Securities] Cancel load'
);
