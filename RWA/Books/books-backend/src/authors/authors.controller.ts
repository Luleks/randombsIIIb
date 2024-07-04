import { Controller, Get } from '@nestjs/common';
import { Author } from './author.entity';
import { AuthorsService } from './authors.service';

@Controller('authors')
export class AuthorsController {
  constructor(private authorService: AuthorsService) {}

  @Get()
  async findAll(): Promise<Partial<Author>[]> {
    return this.authorService.findAll();
  }
}
