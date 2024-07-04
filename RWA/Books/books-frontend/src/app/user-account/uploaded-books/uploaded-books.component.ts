import { Component, Input } from '@angular/core';
import { Router } from '@angular/router';
import { Store } from '@ngrx/store';
import { selectUploadedBooks } from '../../store/user/user.selectors';
import { Book } from '../../interfaces';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-uploaded-books',
  templateUrl: './uploaded-books.component.html',
  styleUrl: './uploaded-books.component.scss',
})
export class UploadedBooksComponent {
  uploadedBooks$!: Observable<Partial<Book>[]>;

  constructor(public router: Router, private store: Store) {}

  navigate(to: string, id: number) {
    this.router.navigate([to, id]);
  }

  ngOnInit(): void {
    this.uploadedBooks$ = this.store.select(selectUploadedBooks);
  }
}
