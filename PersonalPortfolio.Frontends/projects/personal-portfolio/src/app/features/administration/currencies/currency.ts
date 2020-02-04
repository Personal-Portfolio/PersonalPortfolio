import { EntityState } from '@ngrx/entity';

export interface Currency {
  id: string;
  description: string;
}

export interface CurrencyState extends EntityState<Currency> {}