import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { BooksService } from '../books.service';
import { Store } from '@ngrx/store';
import { selectUser } from '../../store/user/user.selectors';
import { ExtendedBook } from '../../interfaces';

@Component({
  selector: 'app-book-page',
  templateUrl: './book-page.component.html',
  styleUrls: ['./book-page.component.scss'],
})
export class BookPageComponent implements OnInit {
  book!: ExtendedBook;
  access_token!: string;
  userId!: number;
  avatar!: string;
  username!: string;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private bookService: BooksService,
    private store: Store
  ) {}

  ngOnInit(): void {
    this.store.select(selectUser).subscribe((user) => {
      if (user == null) {
        console.log('here');
        this.router.navigate(['login']);
      } else {
        this.access_token = user.access_token;
        this.userId = user.id;
        this.avatar = user.avatar;
        this.username = user.username;

        const bookId = this.route.snapshot.paramMap.get('id');
        if (bookId) {
          this.bookService.getBookById(+bookId, this.access_token).subscribe({
            next: (data) => {
              this.book = data;
            },
            error: (_) => this.router.navigate(['page-not-found']),
          });
        }
      }
    });
  }

  newDownload() {
    this.bookService
      .addDownload(this.book.id, this.userId, this.access_token)
      .subscribe();
  }
}
