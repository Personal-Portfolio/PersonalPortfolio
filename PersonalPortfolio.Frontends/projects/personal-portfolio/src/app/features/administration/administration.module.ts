import { NgModule } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { StoreModule } from '@ngrx/store';
import { EffectsModule } from '@ngrx/effects';
import { TranslateModule, TranslateLoader } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';

import { SharedModule } from '../../shared/shared.module';
import { environment } from '../../../environments/environment';
import { AdministrationComponent } from './components/administration/administration.component';
import { AdministrationRoutingModule } from './administration-routing.module';
import { CurrenciesComponent } from './components/administration/currencies/currencies.component';
import { SecuritiesComponent } from './components/administration/securities/securities.component';
import { FEATURE_NAME, reducers } from './state/administration.state';
import { AdministrationEffects } from './state/administration.effects';
import { CurrenciesEffects } from './state/currencies/currencies.effects';
import { CurrenciesService } from './services/currencies.service';
import { SecuritiesService } from './services/securities.service';
import { SecuritiesEffects } from './state/securities/securities.effects';
import { SecurityTypesEffects } from './state/security-types/security-types.effects';
import { SecurityTypesService } from './services/securities-types.service';
import { InfoComponent } from './components/administration/info/info.component';

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
        AdministrationEffects,
        CurrenciesEffects,
        SecurityTypesEffects,
        SecuritiesEffects
      ])
    ],
    declarations: [
        AdministrationComponent,
        CurrenciesComponent,
        InfoComponent,
        SecuritiesComponent
    ],
    providers: [CurrenciesService, SecuritiesService, SecurityTypesService]
})
export class AdministrationModule {
    constructor() {}
}
