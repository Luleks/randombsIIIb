import {
  ForbiddenException,
  Injectable,
  NotFoundException,
} from '@nestjs/common';
import { InjectRepository } from '@nestjs/typeorm';
import { Book } from 'src/books/book.entity';
import { User } from 'src/users/user.entity';
import { Repository } from 'typeorm';
import { Review } from './review.entity';
import { CreateBookDto } from 'src/books/dto/create-book.dto';
import { CreateReviewDto } from './dto/create-review.dto';
import { NotFoundError } from 'rxjs';

@Injectable()
export class ReviewsService {
  constructor(
    @InjectRepository(Book)
    private readonly bookRepository: Repository<Book>,
    @InjectRepository(User)
    private readonly userRepository: Repository<User>,
    @InjectRepository(Review)
    private readonly reviewRepository: Repository<Review>,
  ) {}

  async findAll() {
    return this.reviewRepository.find({ select: ['id', 'comment'] });
  }

  async addReview(params: CreateReviewDto): Promise<void> {
    const { userId, bookId, comment, rating } = params;

    const user = await this.userRepository.findOne({ where: { id: userId } });
    if (!user) {
      throw new NotFoundException('User not found');
    }

    const book = await this.bookRepository.findOne({ where: { id: bookId } });
    if (!book) {
      throw new NotFoundException('Book not found');
    }

    const existingReview = await this.reviewRepository.findOne({
      where: { user: { id: userId }, book: { id: bookId } },
    });

    if (existingReview) {
      throw new ForbiddenException('You can only leave one review per book.');
    }

    const newReview = new Review();
    newReview.comment = comment;
    newReview.rating = rating;
    newReview.user = user;
    newReview.book = book;

    await this.reviewRepository.save(newReview);
  }

  async deleteReview(reviewId: number): Promise<void> {
    await this.reviewRepository.delete(reviewId);
  }
}
