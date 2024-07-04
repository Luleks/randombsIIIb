import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Book, ExtendedBook } from '../interfaces';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class BooksService {
  constructor(private httpClient: HttpClient) {}

  uploadBook(
    userId: number,
    bookToUpload: Book,
    access_token: string
  ): Observable<Book> {
    const headers = new HttpHeaders({
      Authorization: `Bearer ${access_token}`,
    });

    return this.httpClient.post(
      `http://localhost:3000/books/add/${userId}`,
      bookToUpload,
      { headers }
    ) as Observable<Book>;
  }

  getUploadedBooks(
    userId: number,
    access_token: string
  ): Observable<Partial<Book>[]> {
    const headers = new HttpHeaders({
      Authorization: `Bearer ${access_token}`,
    });

    return this.httpClient.get(
      `http://localhost:3000/books/uploader/${userId}`,
      {
        headers,
      }
    ) as Observable<Partial<Book>[]>;
  }

  getDownloadedBooks(
    userId: number,
    access_token: string
  ): Observable<Partial<Book>[]> {
    const headers = new HttpHeaders({
      Authorization: `Bearer ${access_token}`,
    });

    return this.httpClient.get(
      `http://localhost:3000/books/downloads/${userId}`,
      {
        headers,
      }
    ) as Observable<Partial<Book>[]>;
  }

  getBooks(
    page: number,
    limit: number,
    filters: {
      title?: string;
      numPages?: number;
      author?: string;
      category?: string;
    } = {}
  ): Observable<{ length: number; books: Book[] }> {
    let params = new HttpParams()
      .set('page', page.toString())
      .set('limit', limit.toString());

    if (filters.title) {
      params = params.set('title', filters.title);
    }
    if (filters.numPages) {
      params = params.set('numPages', filters.numPages.toString());
    }
    if (filters.author) {
      params = params.set('author', filters.author);
    }
    if (filters.category) {
      params = params.set('category', filters.category);
    }

    return this.httpClient.get<{ length: number; books: Book[] }>(
      'http://localhost:3000/books',
      {
        params: params,
      }
    );
  }

  getAuthors(): Observable<{ id: number; name: string }[]> {
    return this.httpClient.get<{ id: number; name: string }[]>(
      `http://localhost:3000/authors`
    );
  }

  getCategories(): Observable<{ id: number; name: string }[]> {
    return this.httpClient.get<{ id: number; name: string }[]>(
      `http://localhost:3000/category`
    );
  }

  getBookById(bookId: number, access_token: string): Observable<ExtendedBook> {
    const headers = new HttpHeaders({
      Authorization: `Bearer ${access_token}`,
    });
    return this.httpClient.get<ExtendedBook>(
      `http://localhost:3000/books/${bookId}`,
      {
        headers,
      }
    );
  }

  addDownload(bookId: number, userId: number, access_token: string) {
    const headers = new HttpHeaders({
      Authorization: `Bearer ${access_token}`,
    });
    return this.httpClient.post<ExtendedBook>(
      `http://localhost:3000/books/newDownload/${bookId}/${userId}`,
      {
        headers,
      }
    );
  }

  addReview(
    params: { bookId: number; userId: number; comment: string; rating: number },
    access_token: string
  ) {
    const headers = new HttpHeaders({
      Authorization: `Bearer ${access_token}`,
      'Content-Type': 'application/json',
    });

    const body = {
      userId: params.userId,
      bookId: params.bookId,
      rating: params.rating,
      comment: params.comment,
    };

    console.log(body);

    return this.httpClient.post(`http://localhost:3000/reviews/add`, body, {
      headers,
    });
  }

  getAdminBooks(access_token: string) {
    const headers = new HttpHeaders({
      Authorization: `Bearer ${access_token}`,
      'Content-Type': 'application/json',
    });

    return this.httpClient.get<Partial<Book>[]>(
      'http://localhost:3000/books/admin-books',
      {
        headers,
      }
    );
  }

  deleteBook(bookId: number, access_token: string) {
    const headers = new HttpHeaders({
      Authorization: `Bearer ${access_token}`,
      'Content-Type': 'application/json',
    });

    return this.httpClient.delete(`http://localhost:3000/books/${bookId}`, {
      headers,
    });
  }
}
