import { Author } from 'src/authors/author.entity';
import { Book } from 'src/books/book.entity';
import { Entity, ManyToOne, OneToMany, PrimaryGeneratedColumn } from 'typeorm';

@Entity()
export class Authorship {
  @PrimaryGeneratedColumn()
  id: number;

  @ManyToOne(() => Author, (author) => author.authoredBooks)
  author: Author;

  @ManyToOne(() => Book, (book) => book.authors)
  book: Book;
}
