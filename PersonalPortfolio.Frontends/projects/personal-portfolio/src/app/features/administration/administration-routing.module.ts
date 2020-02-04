import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { AdministrationComponent } from './administration/administration.component';
import { CurrenciesComponent } from './currencies/component/currencies.component';

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
            }
        ]
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class AdministrationRoutingModule {}
