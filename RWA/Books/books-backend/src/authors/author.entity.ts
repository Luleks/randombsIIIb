import { Authorship } from 'src/authors/authorship.entity';
import { Column, Entity, OneToMany, PrimaryGeneratedColumn } from 'typeorm';

@Entity()
export class Author {
  @PrimaryGeneratedColumn()
  id: number;

  @Column()
  name: string;

  @OneToMany(() => Authorship, (authorship) => authorship.author)
  authoredBooks: Authorship[];
}
