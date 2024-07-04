import { Component, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import { selectUser } from '../../store/user/user.selectors';
import { Router } from '@angular/router';
import { loadUserBooks } from '../../store/user/user.actions';

@Component({
  selector: 'app-user-account',
  templateUrl: './user-account.component.html',
  styleUrl: './user-account.component.scss',
})
export class UserAccountComponent implements OnInit {
  userType!: string;

  constructor(private store: Store, private router: Router) {}

  ngOnInit(): void {
    this.store.select(selectUser).subscribe((user) => {
      if (user == null) this.router.navigate(['']);
      else {
        this.userType = user.userType;
        if (user.userType == 'user') this.store.dispatch(loadUserBooks());
      }
    });
  }
}
