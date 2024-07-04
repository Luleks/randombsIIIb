import { Component, OnInit } from '@angular/core';
import { Book } from '../../interfaces';
import { BooksService } from '../../books/books.service';
import { Router } from '@angular/router';
import { Store } from '@ngrx/store';
import { selectUser } from '../../store/user/user.selectors';

@Component({
  selector: 'app-book-list',
  templateUrl: './book-list.component.html',
  styleUrls: ['./book-list.component.scss'],
})
export class BookListComponent implements OnInit {
  books: Partial<Book>[] = [];
  page = 1;
  limit = 12;
  length: number = 12;

  constructor(
    private bookService: BooksService,
    public router: Router,
    public store: Store
  ) {}

  ngOnInit(): void {
    this.getBooks();
  }

  getBooks(
    filters: {
      title?: string;
      numPages?: number;
      author?: string;
      category?: string;
    } = {}
  ): void {
    this.bookService
      .getBooks(this.page, this.limit, filters)
      .subscribe((data: { length: number; books: Partial<Book>[] }) => {
        this.books = data.books;
        this.length = data.length;
      });
  }

  onPageChange(newPage: number): void {
    this.page = newPage;
    this.getBooks();
  }

  onFilterChange(filters: {
    title?: string;
    numPages?: number;
    author?: string;
    category?: string;
  }): void {
    this.getBooks(filters);
  }
}
