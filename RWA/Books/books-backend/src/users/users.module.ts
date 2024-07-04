import { Module } from '@nestjs/common';
import { UsersService } from './users.service';
import { TypeOrmModule } from '@nestjs/typeorm';
import { User } from './user.entity';
import { UsersController } from './users.controller';
import { CategoryModule } from 'src/category/category.module';
import { CategoryService } from 'src/category/category.service';

@Module({
  imports: [CategoryModule, TypeOrmModule.forFeature([User])],
  providers: [UsersService, CategoryService],
  exports: [UsersService, TypeOrmModule],
  controllers: [UsersController],
})
export class UsersModule {}
