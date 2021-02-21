import { Store, select } from '@ngrx/store';
import { Component, OnInit, ChangeDetectionStrategy } from '@angular/core';
import { Observable } from 'rxjs';
import {
  routeAnimations,
  selectIsAuthenticated
} from '../../../core/core.module';

import { State } from '../examples.state';

@Component({
  selector: 'personal-portfolio-examples',
  templateUrl: './examples.component.html',
  styleUrls: ['./examples.component.scss'],
  animations: [routeAnimations],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class ExamplesComponent implements OnInit {
  isAuthenticated$: Observable<boolean>;

  examples = [
    { link: 'todos', label: 'personal-portfolio.examples.menu.todos' },
    { link: 'stock-market', label: 'personal-portfolio.examples.menu.stocks' },
    { link: 'theming', label: 'personal-portfolio.examples.menu.theming' },
    { link: 'crud', label: 'personal-portfolio.examples.menu.crud' },
    { link: 'form', label: 'personal-portfolio.examples.menu.form' },
    { link: 'notifications', label: 'personal-portfolio.examples.menu.notifications' },
    { link: 'elements', label: 'personal-portfolio.examples.menu.elements' },
    { link: 'authenticated', label: 'personal-portfolio.examples.menu.auth', auth: true }
  ];

  constructor(private store: Store<State>) {}

  ngOnInit(): void {
    this.isAuthenticated$ = this.store.pipe(select(selectIsAuthenticated));
  }
}
