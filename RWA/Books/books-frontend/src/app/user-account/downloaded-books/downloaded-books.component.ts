import { Component } from '@angular/core';
import { selectDownloadedBooks } from '../../store/user/user.selectors';
import { Store } from '@ngrx/store';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { Book } from '../../interfaces';

@Component({
  selector: 'app-downloaded-books',
  templateUrl: './downloaded-books.component.html',
  styleUrl: './downloaded-books.component.scss',
})
export class DownloadedBooksComponent {
  downloadedBooks$!: Observable<Partial<Book>[]>;

  constructor(public router: Router, private store: Store) {}

  navigate(to: string, id: number) {
    this.router.navigate([to, id]);
  }

  ngOnInit(): void {
    this.downloadedBooks$ = this.store.select(selectDownloadedBooks);
  }
}
