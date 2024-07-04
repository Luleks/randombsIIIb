import { Component, OnInit } from '@angular/core';
import {
  AbstractControl,
  FormBuilder,
  FormGroup,
  Validators,
} from '@angular/forms';
import { Store } from '@ngrx/store';
import { selectUser } from '../../store/user/user.selectors';
import { HttpClient } from '@angular/common/http';
import { UserService } from '../user.service';
import { login } from '../../store/user/user.actions';
import { Router } from '@angular/router';

@Component({
  selector: 'app-user-info',
  templateUrl: './user-info.component.html',
  styleUrls: ['./user-info.component.scss'],
})
export class UserInfoComponent implements OnInit {
  userInfoForm: FormGroup;
  categories: string[] = [];
  avatar: string = '';
  id: number = 0;
  access_token: string = '';

  constructor(
    private fb: FormBuilder,
    private store: Store,
    private userService: UserService,
    private router: Router
  ) {
    this.userInfoForm = this.fb.group(
      {
        email: [{ value: '', disabled: true }, Validators.required],
        username: [{ value: '', disabled: true }, Validators.required],
        firstName: ['', Validators.required],
        lastName: ['', Validators.required],
        password: ['', Validators.required],
        repeatPassword: ['', Validators.required],
        newCategory: [''],
      },
      { validators: this.passwordMatchValidator }
    );
  }

  passwordMatchValidator(
    control: AbstractControl
  ): { [key: string]: boolean } | null {
    const password = control.get('password');
    const repeatPassword = control.get('repeatPassword');
    if (password && repeatPassword && password.value !== repeatPassword.value) {
      return { passwordMismatch: true };
    }
    return null;
  }

  ngOnInit(): void {
    this.store.select(selectUser).subscribe((user) => {
      if (user) {
        this.userInfoForm.patchValue(user);
        this.categories = user.categories.map((x) => x.name) ?? [];
        this.avatar = user.avatar;
        this.id = user.id;
        this.access_token = user.access_token;
      }
    });
  }

  saveChanges() {
    console.log(this.userInfoForm.get('password')?.value);
    if (
      this.userInfoForm.get('password')?.value != '' &&
      this.userInfoForm.valid
    ) {
      this.userService
        .updateUser(
          {
            ...this.userInfoForm.value,
            categories: this.categories,
            email: this.userInfoForm.get('email')?.value,
            id: this.id,
          },
          this.access_token
        )
        .subscribe({
          next: (user) => {
            this.store.dispatch(login({ user }));
            alert('Information successfully changed');
          },
          error: () => {
            alert('There was an error trying to update user information');
          },
        });
    } else if (
      this.userInfoForm.get('firstName')?.valid &&
      this.userInfoForm.get('lastName')?.valid
    ) {
      this.userService
        .updateUser(
          {
            ...this.userInfoForm.value,
            categories: this.categories,
            email: this.userInfoForm.get('email')?.value,
            id: this.id,
            password: null,
          },
          this.access_token
        )
        .subscribe({
          next: (user) => {
            this.store.dispatch(login({ user }));
            alert('Information successfully changed');
          },
          error: (error) => {
            console.log(error);
            alert(
              'There was an error trying to update user information. Please try again latter.'
            );
          },
        });
    }
  }

  addCategory(): void {
    const category = this.userInfoForm.get('newCategory')?.value;
    if (category) {
      this.categories.push(category);
      this.userInfoForm.get('newCategory')?.reset();
    }
  }

  removeCategory(index: number): void {
    this.categories.splice(index, 1);
  }
}
