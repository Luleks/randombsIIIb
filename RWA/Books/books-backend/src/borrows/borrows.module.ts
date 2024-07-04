import { Module } from '@nestjs/common';
import { TypeOrmModule } from '@nestjs/typeorm';
import { Borrow } from './borrow.entity';

@Module({
  imports: [TypeOrmModule.forFeature([Borrow])],
  exports: [TypeOrmModule],
})
export class BorrowsModule {}
