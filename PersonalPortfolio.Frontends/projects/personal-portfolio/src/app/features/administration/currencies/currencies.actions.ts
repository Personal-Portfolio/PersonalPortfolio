import { createAction, props } from '@ngrx/store';
import { Currency } from './currency';

export const actionCurrenciesUpsertOne = createAction(
  '[Currencies] Upsert One',
  props<{ currency: Currency }>()
);

export const actionCurrenciesDeleteOne = createAction(
  '[Currencies] Delete One',
  props<{ id: string }>()
);
