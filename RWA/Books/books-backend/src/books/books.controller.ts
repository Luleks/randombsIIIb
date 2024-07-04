import {
  Body,
  Controller,
  Delete,
  Get,
  Param,
  ParseIntPipe,
  Post,
  Query,
  UseGuards,
} from '@nestjs/common';
import { JwtAuthGuard } from 'src/auth/jwt-auth.guard';
import { CreateBookDto } from './dto/create-book.dto';
import { BooksService } from './books.service';
import { HomePageBookDto } from './dto/home-page-book.dto';
import { Roles, RolesGuard } from 'src/auth/roles-auth.guard';

@Controller('books')
export class BooksController {
  constructor(private booksService: BooksService) {}

  @UseGuards(JwtAuthGuard, RolesGuard)
  @Roles('admin', 'user')
  @Post('add/:uid')
  async addBook(
    @Param('uid') uid: string,
    @Body() createBookDto: CreateBookDto,
  ) {
    return this.booksService.addBook(+uid, createBookDto);
  }

  @UseGuards(JwtAuthGuard, RolesGuard)
  @Roles('admin', 'user')
  @Get('/uploader/:uid')
  async getUsersUploads(@Param('uid') uid: string) {
    return this.booksService.getUploadersBooks(+uid);
  }

  @UseGuards(JwtAuthGuard, RolesGuard)
  @Roles('admin', 'user')
  @Get('/downloads/:uid')
  async getUsersDownloads(@Param('uid') uid: string) {
    return this.booksService.getDownloadedBooks(+uid);
  }

  @UseGuards(JwtAuthGuard, RolesGuard)
  @Roles('admin')
  @Get('admin-books')
  async getAll() {
    return this.booksService.findAll();
  }

  @Get()
  async findAll(
    @Query('page') page: string,
    @Query('limit') limit: string,
    @Query('title') title?: string,
    @Query('numPages') numPages?: string,
    @Query('author') author?: string,
    @Query('category') category?: string,
  ): Promise<{ length: number; books: HomePageBookDto[] }> {
    return this.booksService.findAllPaginated(+page, +limit, {
      title,
      numPages: +numPages,
      author: +author,
      category,
    });
  }

  @UseGuards(JwtAuthGuard, RolesGuard)
  @Roles('admin', 'user')
  @Get('/:bookId')
  async findBook(@Param('bookId', ParseIntPipe) bookId: number) {
    return this.booksService.findBook(bookId);
  }

  @Post('/newDownload/:bookId/:userId')
  async addDownload(
    @Param('bookId', ParseIntPipe) bookId: number,
    @Param('userId', ParseIntPipe) userId: number,
  ) {
    return this.booksService.addDownload(bookId, userId);
  }

  @UseGuards(JwtAuthGuard, RolesGuard)
  @Roles('admin')
  @Delete(':userId')
  async deleteBook(@Param('userId', ParseIntPipe) bookId: number) {
    return this.booksService.deleteBook(bookId);
  }
}
