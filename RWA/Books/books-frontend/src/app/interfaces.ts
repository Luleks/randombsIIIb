export interface User {
  id: number;
  email: string;
  username: string;
  firstName: string;
  lastName: string;
  avatar: string;
  userType: string;
  access_token: string;
  categories: { name: string; id: number }[];
}

export interface Book {
  id: number;
  title: string;
  isbn: string;
  numPages: number;
  paperbackLink: string;
  year: number;
  pdfFile: string;
  cover: string;
  authors: string[];
  categories: string[];
}

export interface ExtendedBook extends Book {
  numberOfDownloads: number;
  comments: Comment[];
}

export interface Comment {
  username: string;
  rating: number;
  comment: string;
  avatar: string;
}

export interface BookView extends Partial<Book> {}
