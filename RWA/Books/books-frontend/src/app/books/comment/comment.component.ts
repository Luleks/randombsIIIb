import { Component, Input } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Comment } from '../../interfaces';
import { BooksService } from '../books.service';

@Component({
  selector: 'app-comment',
  templateUrl: './comment.component.html',
  styleUrls: ['./comment.component.scss'],
})
export class CommentComponent {
  @Input() comments: Comment[] = [];
  commentForm: FormGroup;

  @Input() userId!: number;
  @Input() username!: string;
  @Input() userAvatar!: string;
  @Input() access_token!: string;
  @Input() bookId!: number;

  constructor(private fb: FormBuilder, private booksService: BooksService) {
    this.commentForm = this.fb.group({
      rating: [5, [Validators.required, Validators.min(1), Validators.max(10)]],
      comment: ['', [Validators.required, Validators.minLength(10)]],
    });
  }

  addComment() {
    if (this.commentForm.valid) {
      const values = { ...this.commentForm.value };
      this.booksService
        .addReview(
          {
            ...this.commentForm.value,
            userId: this.userId,
            bookId: this.bookId,
          },
          this.access_token
        )
        .subscribe({
          next: () => {
            this.comments.push({
              username: this.username,
              ...values,
              avatar: this.userAvatar,
            });
          },
          error: (err) => alert(err.error.message),
        });

      this.commentForm.reset({ rating: 5 });
    }
  }
}
