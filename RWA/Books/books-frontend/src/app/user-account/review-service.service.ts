import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class ReviewServiceService {
  constructor(private httpClient: HttpClient) {}

  getAdminReviews(access_token: string) {
    const headers = new HttpHeaders({
      Authorization: `Bearer ${access_token}`,
      'Content-Type': 'application/json',
    });

    return this.httpClient.get<{ id: number; comment: string }[]>(
      'http://localhost:3000/reviews',
      {
        headers,
      }
    );
  }

  deleteReview(reviewId: number, access_token: string) {
    const headers = new HttpHeaders({
      Authorization: `Bearer ${access_token}`,
      'Content-Type': 'application/json',
    });

    return this.httpClient.delete(`http://localhost:3000/reviews/${reviewId}`, {
      headers,
    });
  }
}
