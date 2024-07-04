import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Store } from '@ngrx/store';
import { selectUser } from '../../store/user/user.selectors';
import { BooksService } from '../../books/books.service';
import { addUploadedBook } from '../../store/user/user.actions';

@Component({
  selector: 'app-book-upload',
  templateUrl: './book-upload.component.html',
  styleUrls: ['./book-upload.component.scss'],
})
export class BookUploadComponent implements OnInit {
  bookForm: FormGroup;
  id!: number;
  access_token!: string;

  constructor(
    private fb: FormBuilder,
    private store: Store,
    private booksService: BooksService
  ) {
    this.bookForm = this.fb.group({
      title: ['', Validators.required],
      isbn: ['', Validators.required],
      numPages: ['', Validators.required],
      paperbackLink: [''],
      year: ['', Validators.required],
      pdfFile: ['', Validators.required],
      cover: ['', Validators.required],
      authors: this.fb.array([this.createAuthor()]),
      categories: this.fb.array([this.createCategory()]),
    });
  }

  ngOnInit(): void {
    this.store.select(selectUser).subscribe((user) => {
      if (user) {
        this.id = user.id;
        this.access_token = user.access_token;
      }
    });
  }
  createAuthor(): FormGroup {
    return this.fb.group({
      name: ['', Validators.required],
    });
  }

  createCategory(): FormGroup {
    return this.fb.group({
      name: ['', Validators.required],
    });
  }

  get authors(): FormArray {
    return this.bookForm.get('authors') as FormArray;
  }

  addAuthor(): void {
    this.authors.push(this.createAuthor());
  }

  removeAuthor(index: number): void {
    this.authors.removeAt(index);
  }

  get categories(): FormArray {
    return this.bookForm.get('categories') as FormArray;
  }

  addCategory(): void {
    this.categories.push(this.createCategory());
  }

  removeCategory(index: number): void {
    this.categories.removeAt(index);
  }

  async onSubmit(): Promise<void> {
    if (this.bookForm.valid && !isNaN(this.bookForm.get('year')?.value)) {
      this.booksService
        .uploadBook(
          this.id,
          {
            ...this.bookForm.value,
            categories: this.bookForm
              .get('categories')
              ?.value.map((x: { name: string }) => x['name']),
            authors: this.bookForm
              .get('authors')
              ?.value.map((x: { name: string }) => x['name']),
          },
          this.access_token
        )
        .subscribe({
          next: (book) => {
            alert('Book successfully uploaded');
            this.bookForm.reset();
            this.store.dispatch(
              addUploadedBook({
                book: { id: book.id, title: book.title, cover: book.cover },
              })
            );
          },
          error: (error) => {
            console.log(error);
            alert(
              'There was an error trying to upload book. Please try again latter'
            );
          },
        });
    }
  }
}
