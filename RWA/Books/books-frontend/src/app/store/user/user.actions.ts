import { createAction, props } from '@ngrx/store';
import { Book, User } from '../../interfaces';

export const login = createAction('[User] Login', props<{ user: User }>());

export const logout = createAction('[User] Logout');

export const loadUserBooks = createAction('[User Account] Load User Books');
export const loadUserBooksSuccess = createAction(
  '[User Account] Load User Books Success',
  props<{ uploadedBooks: Partial<Book>[]; downloadedBooks: Partial<Book>[] }>()
);
export const loadUserBooksFailure = createAction(
  '[User Account] Load User Books Failure',
  props<{ error: any }>()
);

export const addUploadedBook = createAction(
  '[User Account] Add Uploaded Book',
  props<{ book: Partial<Book> }>()
);

export const addDownloadedBook = createAction(
  '[User Account] Add Downloaded Book',
  props<{ book: Partial<Book> }>()
);
