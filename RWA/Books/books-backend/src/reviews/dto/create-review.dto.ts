import { IsString } from '@nestjs/class-validator';
import { IsNotEmpty, Max, Min } from 'class-validator';

export class CreateReviewDto {
  userId: number;
  bookId: number;

  @IsString()
  @IsNotEmpty()
  comment: string;

  @Min(1)
  @Max(10)
  rating: number;
}
