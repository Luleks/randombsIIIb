import { Authorship } from 'src/authors/authorship.entity';
import { Borrow } from 'src/borrows/borrow.entity';
import { Category } from 'src/category/category.entity';
import { Review } from 'src/reviews/review.entity';
import { User } from 'src/users/user.entity';
import {
  Column,
  Entity,
  JoinTable,
  ManyToMany,
  ManyToOne,
  OneToMany,
  PrimaryGeneratedColumn,
} from 'typeorm';

@Entity()
export class Book {
  @PrimaryGeneratedColumn()
  id: number;

  @Column()
  title: string;

  @Column({ unique: true })
  isbn: string;

  @Column()
  numPages: number;

  @Column()
  paperbackLink: string;

  @Column()
  year: number;

  @Column()
  pdfFile: string;

  @Column()
  cover: string;

  @OneToMany(() => Authorship, (authorship) => authorship.book)
  authors: Authorship[];

  @ManyToMany(() => Category)
  @JoinTable()
  categories: Category[];

  @OneToMany(() => Review, (review) => review.book)
  reviews: Review[];

  @OneToMany(() => Borrow, (borrow) => borrow.book)
  borrows: Borrow[];

  @ManyToOne(() => User, (user) => user.uploadedBooks)
  uploader: User;
}
