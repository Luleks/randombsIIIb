import { Injectable } from '@angular/core';
import { CreateUserDto } from './interfaces';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from '../interfaces';

@Injectable({
  providedIn: 'root',
})
export class LoginService {
  constructor(private httpClient: HttpClient) {}

  loginUser(credentials: {
    email: string;
    password: string;
  }): Observable<User> {
    return this.httpClient.post(
      'http://localhost:3000/auth/login',
      credentials
    ) as Observable<User>;
  }

  registerUser(newUserInfo: CreateUserDto) {
    return this.httpClient.post(
      'http://localhost:3000/auth/register',
      newUserInfo
    );
  }
}
