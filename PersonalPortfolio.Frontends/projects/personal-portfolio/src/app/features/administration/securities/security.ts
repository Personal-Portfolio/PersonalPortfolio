import { EntityState } from '@ngrx/entity';

export interface Security {
  id: string;
  code: string;
  description: string;
}

export interface SecurityState extends EntityState<Security> {}
