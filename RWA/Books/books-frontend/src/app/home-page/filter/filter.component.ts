import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { BooksService } from '../../books/books.service';

@Component({
  selector: 'app-filter',
  templateUrl: './filter.component.html',
  styleUrls: ['./filter.component.scss'],
})
export class FilterComponent implements OnInit {
  @Output() filterChange = new EventEmitter<any>();

  filterForm: FormGroup;
  authors: { id: number; name: string }[] = [];
  categories: { id: number; name: string }[] = [];

  constructor(private fb: FormBuilder, private booksService: BooksService) {
    this.filterForm = this.fb.group({
      title: [''],
      numPages: [''],
      author: [''],
      category: [''],
    });
  }

  ngOnInit(): void {
    this.loadAuthors();
    this.loadCategories();
  }

  loadAuthors() {
    this.booksService
      .getAuthors()
      .subscribe((authors) => (this.authors = authors));
  }

  loadCategories() {
    this.booksService
      .getCategories()
      .subscribe((cats) => (this.categories = cats));
  }

  applyFilter() {
    this.filterChange.emit(this.filterForm.value);
  }
}
