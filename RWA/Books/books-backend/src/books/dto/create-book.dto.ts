import { IsNotEmpty, IsNumber, IsString, IsUrl } from 'class-validator';

export class CreateBookDto {
  @IsString()
  @IsNotEmpty()
  title: string;

  @IsString()
  @IsNotEmpty()
  isbn: string;

  @IsNumber()
  numPages: number;

  @IsUrl()
  paperbackLink: string;

  @IsNumber()
  year: number;

  @IsNotEmpty()
  pdfFile: string;

  @IsNotEmpty()
  cover: string;

  @IsNotEmpty()
  authors: string[];

  @IsNotEmpty()
  categories: string[];
}
