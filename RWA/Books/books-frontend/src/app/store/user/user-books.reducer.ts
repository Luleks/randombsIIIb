import { EntityState, EntityAdapter, createEntityAdapter } from '@ngrx/entity';
import { createReducer, on } from '@ngrx/store';
import { Book, BookView } from '../../interfaces';
import {
  loadUserBooksSuccess,
  loadUserBooksFailure,
  addUploadedBook,
  addDownloadedBook,
} from './user.actions';

export interface UserAccountState {
  uploadedBooks: EntityState<BookView>;
  downloadedBooks: EntityState<BookView>;
  error: any;
}

export const uploadedBooksAdapter: EntityAdapter<BookView> = createEntityAdapter<BookView>();
export const downloadedBooksAdapter: EntityAdapter<BookView> = createEntityAdapter<BookView>();

export const initialState: UserAccountState = {
  uploadedBooks: uploadedBooksAdapter.getInitialState(),
  downloadedBooks: downloadedBooksAdapter.getInitialState(),
  error: null,
};

export const userAccountReducer = createReducer(
  initialState,
  on(loadUserBooksSuccess, (state, { uploadedBooks, downloadedBooks }) => ({
    ...state,
    uploadedBooks: uploadedBooksAdapter.setAll(uploadedBooks, state.uploadedBooks),
    downloadedBooks: downloadedBooksAdapter.setAll(downloadedBooks, state.downloadedBooks),
    error: null,
  })),
  on(loadUserBooksFailure, (state, { error }) => ({
    ...state,
    error,
  })),
  on(addUploadedBook, (state, { book }) => ({
    ...state,
    uploadedBooks: uploadedBooksAdapter.addOne(book, state.uploadedBooks),
  })),
  on(addDownloadedBook, (state, { book }) => ({
    ...state,
    downloadedBooks: downloadedBooksAdapter.addOne(book, state.downloadedBooks),
  }))
);
