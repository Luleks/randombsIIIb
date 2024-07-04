import { Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  Validators,
  AbstractControl,
} from '@angular/forms';
import { LoginService } from '../login.service';
import { Router } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-register-form',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss'],
})
export class RegisterComponent implements OnInit {
  registerForm!: FormGroup;
  avatars: string[];

  constructor(
    private fb: FormBuilder,
    private authService: LoginService,
    private router: Router
  ) {
    this.avatars = Array.from({ length: 9 }).map(
      (_, ind) =>
        `https://raw.githubusercontent.com/mantinedev/mantine/master/.demo/avatars/avatar-${
          ind + 1
        }.png`
    );
  }

  ngOnInit(): void {
    this.registerForm = this.fb.group(
      {
        email: ['', [Validators.required, Validators.email]],
        username: ['', Validators.required],
        password: ['', [Validators.required, Validators.minLength(6)]],
        repeatPassword: ['', Validators.required],
        firstName: ['', Validators.required],
        lastName: ['', Validators.required],
        avatar: [
          'https://raw.githubusercontent.com/mantinedev/mantine/master/.demo/avatars/avatar-1.png',
          Validators.required,
        ],
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

  selectAvatar(avatar: string): void {
    this.registerForm.get('avatar')!.setValue(avatar);
  }

  onSubmit(): void {
    if (this.registerForm.valid) {
      this.authService.registerUser(this.registerForm.value).subscribe({
        next: (_) => {
          alert('Registration successful, please log in to continue');
          this.router.navigate(['login']);
        },
        error: (error: HttpErrorResponse) => {
          alert(error.error.message);
        },
      });
    }
  }
}
