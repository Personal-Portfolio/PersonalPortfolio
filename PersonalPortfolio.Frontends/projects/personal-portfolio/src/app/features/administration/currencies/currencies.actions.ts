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

export const actionCurrenciesLoad = createAction(
    '[Currencies] Load All'
);
