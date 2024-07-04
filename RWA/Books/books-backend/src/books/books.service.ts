import { Injectable, NotFoundException } from '@nestjs/common';
import { CreateBookDto } from './dto/create-book.dto';
import { InjectRepository } from '@nestjs/typeorm';
import { User } from 'src/users/user.entity';
import { Repository } from 'typeorm';
import { Book } from './book.entity';
import { Authorship } from 'src/authors/authorship.entity';
import { Category } from 'src/category/category.entity';
import { Author } from 'src/authors/author.entity';
import { HomePageBookDto } from './dto/home-page-book.dto';
import { Borrow } from 'src/borrows/borrow.entity';
import { Review } from 'src/reviews/review.entity';

@Injectable()
export class BooksService {
  constructor(
    @InjectRepository(Book)
    private readonly bookRepository: Repository<Book>,
    @InjectRepository(User)
    private readonly userRepository: Repository<User>,
    @InjectRepository(Authorship)
    private readonly authorshipRepository: Repository<Authorship>,
    @InjectRepository(Category)
    private readonly categoryRepository: Repository<Category>,
    @InjectRepository(Author)
    private readonly authorRepository: Repository<Author>,
    @InjectRepository(Borrow)
    private readonly borrowRepository: Repository<Borrow>,
    @InjectRepository(Review)
    private readonly reviewRepository: Repository<Review>,
  ) {}

  async addBook(
    uid: number,
    createBookDto: CreateBookDto,
  ): Promise<Partial<Book>> {
    const uploader = await this.userRepository.findOneOrFail({
      where: { id: uid },
    });

    const newBook = new Book();
    newBook.title = createBookDto.title;
    newBook.isbn = createBookDto.isbn;
    newBook.numPages = createBookDto.numPages;
    newBook.paperbackLink = createBookDto.paperbackLink;
    newBook.year = createBookDto.year;
    newBook.uploader = uploader;
    newBook.pdfFile = createBookDto.pdfFile;
    newBook.cover = createBookDto.cover;

    const savedBook = await this.bookRepository.save(newBook);
    savedBook.categories = [];

    await Promise.all(
      createBookDto.authors.map(async (authorName) => {
        let author = await this.authorRepository.findOne({
          where: { name: authorName },
        });
        if (!author) {
          author = this.authorRepository.create({ name: authorName });
          author = await this.authorRepository.save(author);
        }
        const authorship = new Authorship();
        authorship.book = savedBook;
        authorship.author = author;
        await this.authorshipRepository.save(authorship);
      }),
    );
    await Promise.all(
      createBookDto.categories.map(async (categoryName) => {
        let category = await this.categoryRepository.findOne({
          where: { name: categoryName },
        });
        if (!category) {
          category = this.categoryRepository.create({ name: categoryName });
          category = await this.categoryRepository.save(category);
        }
        savedBook.categories.push(category);
      }),
    );

    await this.bookRepository.save(savedBook);

    return { ...savedBook, pdfFile: null };
  }

  async getUploadersBooks(uid: number): Promise<Partial<Book>[]> {
    return this.bookRepository.find({
      where: {
        uploader: { id: uid },
      },
      select: ['id', 'title', 'cover'],
    });
  }

  async getDownloadedBooks(userId: number): Promise<Partial<Book>[]> {
    const borrows = await this.borrowRepository.find({
      where: {
        user: { id: userId },
      },
      relations: ['book'],
    });

    return borrows.map((borrow) => ({
      id: borrow.book.id,
      title: borrow.book.title,
      cover: borrow.book.cover,
    }));
  }

  async addDownload(bookId: number, userId: number): Promise<void> {
    const book = await this.bookRepository.findOneBy({ id: bookId });
    if (!book) {
      throw new NotFoundException(`Book with id ${bookId} not found`);
    }

    const user = await this.userRepository.findOneBy({ id: userId });
    if (!user) {
      throw new NotFoundException(`User with id ${userId} not found`);
    }

    const borrow = new Borrow();
    borrow.date = new Date();
    borrow.book = book;
    borrow.user = user;

    await this.borrowRepository.save(borrow);
  }

  async findBook(id: number): Promise<any> {
    const book = await this.bookRepository.findOne({
      where: { id: id },
      relations: [
        'authors',
        'authors.author',
        'categories',
        'borrows',
        'reviews',
        'reviews.user',
      ],
    });

    if (!book) {
      throw new NotFoundException('Book not found');
    }

    const authors = book.authors.map((authorship) => authorship.author);

    const numberOfDownloads = book.borrows.length;

    const reviews = book.reviews.map((review) => ({
      username: review.user.username,
      rating: review.rating,
      comment: review.comment,
      avatar: review.user.avatar,
    }));

    const { borrows, ...bookWithoutBorrows } = book;

    const bookWithDetails = {
      ...bookWithoutBorrows,
      authors: authors.map((x) => x.name),
      categories: book.categories.map((x) => x.name),
      numberOfDownloads,
      comments: reviews,
    };

    return bookWithDetails;
  }

  async findAll() {
    return await this.bookRepository.find({ select: ['id', 'cover', 'title'] });
  }

  async deleteBook(bookId: number): Promise<void> {
    const book = await this.bookRepository.findOne({
      where: { id: bookId },
      relations: ['reviews', 'authors', 'borrows', 'categories'],
    });

    if (!book) {
      throw new NotFoundException('Book not found');
    }

    await this.reviewRepository.delete({ book });

    await this.borrowRepository.delete({ book });

    const authorships = await this.authorshipRepository.find({
      where: { book },
      relations: ['author'],
    });
    await this.authorshipRepository.delete({ book });

    for (const authorship of authorships) {
      const otherBooksCount = await this.authorshipRepository.count({
        where: { author: authorship.author },
      });
      if (otherBooksCount === 0) {
        await this.authorRepository.delete(authorship.author.id);
      }
    }

    for (const category of book.categories) {
      const otherBooksCount = await this.bookRepository
        .createQueryBuilder('book')
        .leftJoin('book.categories', 'category')
        .where('category.id = :categoryId', { categoryId: category.id })
        .getCount();

      if (otherBooksCount === 1) {
        await this.categoryRepository.delete(category.id);
      }
    }

    await this.bookRepository.delete(bookId);
  }

  async findAllPaginated(
    page: number = 1,
    limit: number = 10,
    filters?: {
      title?: string;
      numPages?: number;
      author?: number;
      category?: string;
    },
  ): Promise<{ length: number; books: HomePageBookDto[] }> {
    const skip = (page - 1) * limit;
    let queryBuilder = this.bookRepository
      .createQueryBuilder('book')
      .select(['book.id', 'book.title', 'book.cover'])
      .skip(skip)
      .take(limit);

    if (filters) {
      if (filters.title) {
        queryBuilder = queryBuilder.andWhere('book.title = :title', {
          title: filters.title,
        });
      }
      if (filters.numPages) {
        queryBuilder = queryBuilder
          .andWhere('book.numPages >= :minNumPages', {
            minNumPages: filters.numPages - 50,
          })
          .andWhere('book.numPages <= :maxNumPages', {
            maxNumPages: filters.numPages + 50,
          });
      }
      if (filters.author) {
        queryBuilder = queryBuilder
          .leftJoin('book.authors', 'authorship')
          .leftJoin('authorship.author', 'author')
          .andWhere('author.id = :authorId', { authorId: filters.author });
      }
      if (filters.category) {
        queryBuilder = queryBuilder
          .leftJoin('book.categories', 'categories')
          .andWhere('categories.id = :categoryId', {
            categoryId: filters.category,
          });
      }
    }
    const [books, total] = await queryBuilder.getManyAndCount();
    return {
      length: total,
      books: books.map((book) => ({
        id: book.id,
        title: book.title,
        cover: book.cover,
      })),
    };
  }
}
