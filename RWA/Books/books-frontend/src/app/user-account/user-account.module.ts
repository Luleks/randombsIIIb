import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserAccountComponent } from './user-account/user-account.component';

import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatIconModule } from '@angular/material/icon';
import { MatTabsModule } from '@angular/material/tabs';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { provideHttpClient } from '@angular/common/http';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { UserInfoComponent } from './user-info/user-info.component';
import { BookUploadComponent } from './book-upload/book-upload.component';
import { MatOptionModule } from '@angular/material/core';
import { UploadedBooksComponent } from './uploaded-books/uploaded-books.component';
import { StoreModule } from '@ngrx/store';
import { EffectsModule } from '@ngrx/effects';
import { userAccountReducer } from '../store/user/user-books.reducer';
import { UserAccountEffects } from '../store/user/user.effects';
import { DownloadedBooksComponent } from './downloaded-books/downloaded-books.component';
import { AdminBooksComponent } from './admin-books/admin-books.component';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { AdminReviewComponent } from './admin-review/admin-review.component';

@NgModule({
  declarations: [
    UserAccountComponent,
    UserInfoComponent,
    BookUploadComponent,
    UploadedBooksComponent,
    DownloadedBooksComponent,
    AdminBooksComponent,
    AdminReviewComponent,
  ],
  imports: [
    CommonModule,
    BrowserModule,
    BrowserAnimationsModule,
    FormsModule,
    ReactiveFormsModule,
    MatButtonModule,
    MatCardModule,
    MatInputModule,
    MatFormFieldModule,
    MatGridListModule,
    MatIconModule,
    MatTabsModule,
    MatProgressSpinnerModule,
    MatSnackBarModule,
    MatOptionModule,
    StoreModule.forFeature('userAccount', userAccountReducer),
    EffectsModule.forFeature([UserAccountEffects]),
  ],
  exports: [UserAccountComponent],
  providers: [provideHttpClient()],
})
export class UserAccountModule {}
