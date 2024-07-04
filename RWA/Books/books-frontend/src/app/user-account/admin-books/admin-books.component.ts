import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Book } from '../../interfaces';
import { BooksService } from '../../books/books.service';
import { Store } from '@ngrx/store';
import { loadUserBooks } from '../../store/user/user.actions';
import { selectUser } from '../../store/user/user.selectors';
import { Router } from '@angular/router';

@Component({
  selector: 'app-admin-books',
  templateUrl: './admin-books.component.html',
  styleUrls: ['./admin-books.component.scss'],
})
export class AdminBooksComponent implements OnInit {
  books: Partial<Book>[] | undefined;
  access_token!: string;

  constructor(
    private router: Router,
    private snackBar: MatSnackBar,
    private booksService: BooksService,
    private store: Store
  ) {}

  ngOnInit(): void {
    this.store.select(selectUser).subscribe((user) => {
      if (user == null) this.router.navigate(['']);
      else {
        this.access_token = user.access_token;

        this.booksService.getAdminBooks(this.access_token).subscribe({
          next: (data) => (this.books = data),
          error: (_) =>
            this.snackBar.open('Failed to load books', 'Close', {
              duration: 3000,
            }),
        });
      }
    });
  }

  removeBook(bookId: number): void {
    this.booksService.deleteBook(bookId, this.access_token).subscribe({
      next: () =>
        (this.books = this.books?.filter((book) => book.id !== bookId)),
      error: () =>
        alert(
          'There was a problem trying to delete book, please try again latter'
        ),
    });
  }
}
