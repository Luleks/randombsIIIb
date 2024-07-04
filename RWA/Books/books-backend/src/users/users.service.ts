import {
  ForbiddenException,
  Injectable,
  InternalServerErrorException,
} from '@nestjs/common';
import { InjectRepository } from '@nestjs/typeorm';
import { User } from './user.entity';
import { Repository } from 'typeorm';
import { CreateUserDto } from './dto/create-user.dto';
import { hashPassword } from 'src/auth/hashing';
import { UpdateUserDto } from './dto/update-user.dto';
import { Category } from 'src/category/category.entity';

@Injectable()
export class UsersService {
  constructor(
    @InjectRepository(User) private userRepository: Repository<User>,
    @InjectRepository(Category)
    private categoryRepository: Repository<Category>,
  ) {}

  async registerUser(createUserDto: CreateUserDto): Promise<User> {
    const hashedPassword = await hashPassword(createUserDto.password);

    try {
      const newUser = { ...createUserDto, password: hashedPassword };

      const user = this.userRepository.create(newUser);
      return await this.userRepository.save(user);
    } catch (error) {
      if (error.code === '23505')
        throw new ForbiddenException('Username or email alredy taken');
      else
        throw new InternalServerErrorException(
          'There was an error trying to register new user',
        );
    }
  }

  private async updateUsersCategories(
    user: User,
    categories: string[],
  ): Promise<void> {
    const categoryEntities: Category[] = [];

    for (const categoryName of categories) {
      let category = await this.categoryRepository.findOne({
        where: { name: categoryName },
      });

      if (!category) {
        category = this.categoryRepository.create({ name: categoryName });
        await this.categoryRepository.save(category);
      }

      categoryEntities.push(category);
    }

    user.categories = categoryEntities;
    await this.userRepository.save(user);
  }

  async updateUser(updateUserDto: UpdateUserDto): Promise<Partial<User>> {
    try {
      const userToUpdate = await this.userRepository.findOneBy({
        id: updateUserDto.id,
      });
      await this.updateUsersCategories(userToUpdate, updateUserDto.categories);

      if (userToUpdate) {
        userToUpdate.firstName = updateUserDto.firstName;
        userToUpdate.lastName = updateUserDto.lastName;

        if (updateUserDto.password)
          userToUpdate.password = await hashPassword(updateUserDto.password);

        await this.userRepository.save(userToUpdate);
        const { password, ...rest } = await this.findOne(updateUserDto.email);
        return rest;
      }
    } catch (error) {
      throw new InternalServerErrorException(
        'There was an error trying to update user information',
      );
    }
  }

  async findOne(email: string): Promise<User | undefined> {
    return this.userRepository.findOne({
      where: { email: email },
      relations: { categories: true },
    });
  }
}
