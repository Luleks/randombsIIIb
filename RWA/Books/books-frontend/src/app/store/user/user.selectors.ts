import { createFeatureSelector, createSelector } from '@ngrx/store';
import { UserState } from './user.reducer';
import {
  UserAccountState,
  downloadedBooksAdapter,
  uploadedBooksAdapter,
} from './user-books.reducer';

export const selectUserState = createFeatureSelector<UserState>('user');

export const selectUser = createSelector(
  selectUserState,
  (state: UserState) => state.user
);

export const selectUserId = createSelector(selectUser, (user) =>
  user ? user.id : null
);

export const selectAccessToken = createSelector(selectUser, (user) =>
  user ? user.access_token : null
);

export const selectUserAccountState =
  createFeatureSelector<UserAccountState>('userAccount');

const { selectAll: selectAllUploadedBooks } =
  uploadedBooksAdapter.getSelectors();
const { selectAll: selectAllDownloadedBooks } =
  downloadedBooksAdapter.getSelectors();

export const selectUploadedBooks = createSelector(
  selectUserAccountState,
  (state: UserAccountState) => selectAllUploadedBooks(state.uploadedBooks)
);

export const selectDownloadedBooks = createSelector(
  selectUserAccountState,
  (state: UserAccountState) => selectAllDownloadedBooks(state.downloadedBooks)
);
