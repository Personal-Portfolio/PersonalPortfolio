import { createAction, props } from '@ngrx/store';
import { Security } from './security';

export const actionSecuritiesUpsertOne = createAction(
    '[Securities] Upsert One', props<{ security: Security }>()
);

export const actionSecuritiesDeleteOne = createAction(
    '[Securities] Delete One', props<{ id: string }>()
);
