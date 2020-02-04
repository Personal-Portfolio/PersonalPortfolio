import { Component, ChangeDetectionStrategy } from '@angular/core';
import {
    routeAnimations
  } from '../../../core/core.module';
  
@Component({
    selector: 'personal-portfolio-administration',
    templateUrl: './administration.component.html',
    styleUrls: ['./administration.component.scss'],
    animations: [routeAnimations],
    changeDetection: ChangeDetectionStrategy.OnPush
})
export class AdministrationComponent {
    tabs = [
        { link: 'currencies', label: 'personal-portfolio.administration.menu.currencies' },
        { link: 'securities', label: 'personal-portfolio.administration.menu.securities' },
        { link: 'data-providers', label: 'personal-portfolio.administration.menu.data-providers' }
      ];
}
