import { Module } from '@nestjs/common';
import { Author } from './author.entity';
import { TypeOrmModule } from '@nestjs/typeorm';
import { Authorship } from './authorship.entity';
import { AuthorsController } from './authors.controller';
import { AuthorsService } from './authors.service';

@Module({
  imports: [
    TypeOrmModule.forFeature([Author]),
    TypeOrmModule.forFeature([Authorship]),
  ],
  exports: [TypeOrmModule],
  controllers: [AuthorsController],
  providers: [AuthorsService],
})
export class AuthorsModule {}
