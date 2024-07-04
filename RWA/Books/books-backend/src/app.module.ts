import { Module } from '@nestjs/common';
import { AppController } from './app.controller';
import { AppService } from './app.service';
import { TypeOrmModule } from '@nestjs/typeorm';
import { dataSourceOptions } from 'db/data-source';
import { AuthModule } from './auth/auth.module';
import { UsersModule } from './users/users.module';
import { CategoryService } from './category/category.service';
import { CategoryModule } from './category/category.module';
import { BooksModule } from './books/books.module';
import { AuthorsModule } from './authors/authors.module';
import { BorrowsModule } from './borrows/borrows.module';
import { ReviewsModule } from './reviews/reviews.module';

@Module({
  imports: [
    UsersModule,
    AuthModule,
    TypeOrmModule.forRoot(dataSourceOptions),
    CategoryModule,
    BooksModule,
    AuthorsModule,
    BorrowsModule,
    ReviewsModule,
  ],
  controllers: [AppController],
  providers: [AppService, CategoryService],
})
export class AppModule {}
