import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { Store, select } from '@ngrx/store';
import {
  catchError,
  filter,
  map,
  mergeMap,
  withLatestFrom,
} from 'rxjs/operators';
import { of, forkJoin } from 'rxjs';
import { UserState } from './user.reducer';
import { selectUserId, selectAccessToken } from './user.selectors';
import { BooksService } from '../../books/books.service';
import {
  loadUserBooks,
  loadUserBooksFailure,
  loadUserBooksSuccess,
} from './user.actions';

@Injectable()
export class UserAccountEffects {
  constructor(
    private actions$: Actions,
    private store: Store<UserState>,
    private booksService: BooksService
  ) {}

  loadUserBooks$ = createEffect(() =>
    this.actions$.pipe(
      ofType(loadUserBooks),
      withLatestFrom(
        this.store.pipe(select(selectUserId)),
        this.store.pipe(select(selectAccessToken))
      ),
      filter(([_, userId]) => !!userId),
      mergeMap(([_, userId, accessToken]) =>
        forkJoin({
          uploadedBooks: this.booksService.getUploadedBooks(
            userId!,
            accessToken!
          ),
          downloadedBooks: this.booksService.getDownloadedBooks(
            userId!,
            accessToken!
          ),
        }).pipe(
          map(({ uploadedBooks, downloadedBooks }) =>
            loadUserBooksSuccess({
              uploadedBooks,
              downloadedBooks,
            })
          ),
          catchError((error) => of(loadUserBooksFailure({ error })))
        )
      )
    )
  );
}
