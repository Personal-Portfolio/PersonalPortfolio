import { createAction, props } from '@ngrx/store';
import { Currency } from './currency';

export const actionCurrenciesUpsertOne = createAction(
    '[Currencies] Upsert One', props<{ currency: Currency }>()
);

export const actionCurrenciesUpsertMany = createAction(
    '[Currencies] Upsert Many', props<{ currencies: Currency[] }>()
);

export const actionCurrenciesDeleteOne = createAction(
    '[Currencies] Delete One', props<{ id: string }>()
);

export const actionCurrenciesRequestAll = createAction(
    '[Currencies] Load All'
);

export const actionCurrenciesFailRequest = createAction(
    '[Currencies] Do on load failed', props<{ error: Error }>()
);

export const actionCurrenciesRequestCancelRequest = createAction(
    '[Currencies] Cancel load'
);
