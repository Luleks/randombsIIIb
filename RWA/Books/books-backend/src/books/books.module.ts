import { Module, forwardRef } from '@nestjs/common';
import { BooksController } from './books.controller';
import { BooksService } from './books.service';
import { Book } from './book.entity';
import { TypeOrmModule } from '@nestjs/typeorm';
import { UsersModule } from 'src/users/users.module';
import { AuthorsModule } from 'src/authors/authors.module';
import { CategoryModule } from 'src/category/category.module';
import { BorrowsModule } from 'src/borrows/borrows.module';
import { ReviewsModule } from 'src/reviews/reviews.module';

@Module({
  imports: [
    TypeOrmModule.forFeature([Book]),
    UsersModule,
    AuthorsModule,
    CategoryModule,
    BorrowsModule,
    forwardRef(() => ReviewsModule),
  ],
  exports: [TypeOrmModule],
  controllers: [BooksController],
  providers: [BooksService],
})
export class BooksModule {}
