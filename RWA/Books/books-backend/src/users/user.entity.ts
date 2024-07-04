import { Book } from 'src/books/book.entity';
import { Borrow } from 'src/borrows/borrow.entity';
import { Category } from 'src/category/category.entity';
import { Review } from 'src/reviews/review.entity';
import {
  Column,
  Entity,
  JoinTable,
  ManyToMany,
  OneToMany,
  PrimaryGeneratedColumn,
} from 'typeorm';

@Entity()
export class User {
  @PrimaryGeneratedColumn()
  id: number;

  @Column({ unique: true })
  email: string;

  @Column({ unique: true })
  username: string;

  @Column()
  password: string;

  @Column()
  firstName: string;

  @Column()
  lastName: string;

  @Column()
  avatar: string;

  @Column()
  userType: string;

  @ManyToMany(() => Category)
  @JoinTable()
  categories: Category[];

  @OneToMany(() => Review, (review) => review.user)
  reviews: Review[];

  @OneToMany(() => Borrow, (borrow) => borrow.user)
  borrows: Borrow[];

  @OneToMany(() => Book, (book) => book.uploader)
  uploadedBooks: Book[];
}
