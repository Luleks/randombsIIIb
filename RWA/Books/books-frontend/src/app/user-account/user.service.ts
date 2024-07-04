import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { User } from '../interfaces';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  constructor(private httpClient: HttpClient) {}

  updateUser(
    updatedUserInfo: Partial<User>,
    access_token: string
  ): Observable<User> {
    const headers = new HttpHeaders({
      Authorization: `Bearer ${access_token}`,
    });

    return this.httpClient.post(
      'http://localhost:3000/users/updateProfile',
      updatedUserInfo,
      { headers }
    ) as Observable<User>;
  }

  
}
