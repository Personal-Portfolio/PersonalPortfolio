import { NgModule } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { StoreModule } from '@ngrx/store';
import { EffectsModule } from '@ngrx/effects';
import { TranslateModule, TranslateLoader } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';

import { SharedModule } from '../../shared/shared.module';
import { environment } from '../../../environments/environment';
import { FEATURE_NAME, reducers } from './administration.state';
import { AdministrationComponent } from './administration/administration.component';
import { AdministrationRoutingModule } from './administration-routing.module';
import { CurrenciesComponent } from './currencies/component/currencies.component';
import { AdministrationEffects } from './administration.effects';

export function HttpLoaderFactory(http: HttpClient) {
    return new TranslateHttpLoader(
      http,
      `${environment.i18nPrefix}/assets/i18n/administration/`,
      '.json'
    );
  }

@NgModule({
    imports: [
      SharedModule,
      AdministrationRoutingModule,
      StoreModule.forFeature(FEATURE_NAME, reducers),
      TranslateModule.forChild({
        loader: {
          provide: TranslateLoader,
          useFactory: HttpLoaderFactory,
          deps: [HttpClient]
        },
        isolate: true
      }),
      EffectsModule.forFeature([
        AdministrationEffects
      ])
    ],
    declarations: [
        AdministrationComponent,
        CurrenciesComponent
    ]
})
export class AdministrationModule {
    constructor() {}
}
