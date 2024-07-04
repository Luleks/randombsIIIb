import {
  Body,
  Controller,
  Delete,
  Get,
  Param,
  ParseIntPipe,
  Post,
  UseGuards,
} from '@nestjs/common';
import { JwtAuthGuard } from 'src/auth/jwt-auth.guard';
import { CreateReviewDto } from './dto/create-review.dto';
import { ReviewsService } from './reviews.service';
import { Roles, RolesGuard } from 'src/auth/roles-auth.guard';

@Controller('reviews')
export class ReviewsController {
  constructor(private reviewService: ReviewsService) {}

  @UseGuards(JwtAuthGuard, RolesGuard)
  @Roles('admin', 'user')
  @Post('add')
  async addBook(@Body() createReviewDto: CreateReviewDto) {
    return this.reviewService.addReview(createReviewDto);
  }

  @UseGuards(JwtAuthGuard, RolesGuard)
  @Roles('admin')
  @Get()
  async getCommnets() {
    return this.reviewService.findAll();
  }

  @UseGuards(JwtAuthGuard, RolesGuard)
  @Roles('admin')
  @Delete(':reviewId')
  async deleteReview(@Param('reviewId', ParseIntPipe) reviewId: number) {
    return this.reviewService.deleteReview(reviewId);
  }
}
