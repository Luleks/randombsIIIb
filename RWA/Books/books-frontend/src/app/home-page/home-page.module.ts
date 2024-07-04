import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BrowserModule } from '@angular/platform-browser';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatPaginatorModule } from '@angular/material/paginator';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HomePageComponent } from './home-page/home-page.component';
import { MatInputModule } from '@angular/material/input';
import { MatGridListModule } from '@angular/material/grid-list';
import { BookListComponent } from './book-list/book-list.component';
import { FilterComponent } from './filter/filter.component';
import { MatSelectModule } from '@angular/material/select';

@NgModule({
  declarations: [HomePageComponent, BookListComponent, FilterComponent],
  imports: [
    CommonModule,
    MatInputModule,
    MatButtonModule,
    MatCardModule,
    MatPaginatorModule,
    BrowserAnimationsModule,
    FormsModule,
    BrowserModule,
    MatSelectModule,
    ReactiveFormsModule,
  ],
  exports: [HomePageComponent],
})
export class HomePageModule {}
