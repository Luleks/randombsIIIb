import { Book } from 'src/books/book.entity';
import { User } from 'src/users/user.entity';
import { Column, Entity, ManyToOne, PrimaryGeneratedColumn } from 'typeorm';

@Entity()
export class Borrow {
  @PrimaryGeneratedColumn()
  id: number;

  @Column()
  date: Date;

  @ManyToOne(() => Book, (book) => book.borrows)
  book: Book;

  @ManyToOne(() => User, (user) => user.borrows)
  user: User;
}
