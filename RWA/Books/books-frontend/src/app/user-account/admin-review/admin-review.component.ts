import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ReviewServiceService } from '../review-service.service';
import { Store } from '@ngrx/store';
import { Router } from '@angular/router';
import { selectUser } from '../../store/user/user.selectors';

@Component({
  selector: 'app-admin-review',
  templateUrl: './admin-review.component.html',
  styleUrls: ['./admin-review.component.scss'],
})
export class AdminReviewComponent implements OnInit {
  reviews!: { id: number; comment: string }[];
  access_token!: string;

  constructor(
    private reviewService: ReviewServiceService,
    private router: Router,
    private snackBar: MatSnackBar,
    private store: Store
  ) {}

  ngOnInit(): void {
    this.store.select(selectUser).subscribe((user) => {
      if (user == null) this.router.navigate(['']);
      else {
        this.access_token = user.access_token;

        this.reviewService.getAdminReviews(this.access_token).subscribe({
          next: (data) => (this.reviews = data),
          error: (_) =>
            this.snackBar.open('Failed to load books', 'Close', {
              duration: 3000,
            }),
        });
      }
    });
  }

  removeReview(reviewId: number): void {
    this.reviewService.deleteReview(reviewId, this.access_token).subscribe({
      next: () =>
        (this.reviews = this.reviews?.filter(
          (review) => review.id !== reviewId
        )),
      error: () =>
        alert(
          'There was a problem trying to delete book, please try again latter'
        ),
    });
  }
}
