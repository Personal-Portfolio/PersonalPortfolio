import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { AdministrationComponent } from './components/administration/administration.component';
import { CurrenciesComponent } from './components/administration/currencies/currencies.component';
import { SecuritiesComponent } from './components/administration/securities/securities.component';
import { InfoComponent } from './components/administration/info/info.component';

const routes: Routes = [
    {
        path: '',
        component: AdministrationComponent,
        children: [
            {
                path: '',
                redirectTo: 'currencies/',
                pathMatch: 'full'
            },
            {
                path: 'currencies',
                redirectTo: 'currencies/',
                pathMatch: 'full'
            },
            {
                path: 'currencies/:id',
                component: CurrenciesComponent,
                data: { title: 'personal-portfolio.administration.menu.currencies' }
            },
            {
                path: 'securities',
                redirectTo: 'securities/',
                pathMatch: 'full'
            },
            {
                path: 'securities/:id',
                component: SecuritiesComponent,
                data: { title: 'personal-portfolio.administration.menu.securities' }
            },
            {
                path: 'system-info',
                component: InfoComponent,
                data: { title: 'personal-portfolio.administration.menu.system-info' }
            }
        ]
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class AdministrationRoutingModule {}
