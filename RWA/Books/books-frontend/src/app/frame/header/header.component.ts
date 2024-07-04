import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { Store } from '@ngrx/store';
import { selectUser } from '../../store/user/user.selectors';
import { logout } from '../../store/user/user.actions';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss',
})
export class HeaderComponent {
  user$ = this.store.select(selectUser);

  constructor(public router: Router, private store: Store) {}

  navigate(to: string) {
    this.router.navigate([to]);
  }

  logout() {
    this.store.dispatch(logout());
    this.router.navigate(['']);
  }
}
