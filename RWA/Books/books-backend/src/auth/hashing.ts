import { InternalServerErrorException } from '@nestjs/common';
import * as bcrypt from 'bcrypt';

const saltRounds = 10;

export async function hashPassword(password: string): Promise<string> {
  try {
    const hash = await bcrypt.hash(password, saltRounds);
    return hash;
  } catch (err) {
    throw new InternalServerErrorException(
      'There was an error trying to hash password',
    );
  }
}

export async function comparePassword(
  plainPassword: string,
  hashedPassword: string,
): Promise<boolean> {
  try {
    const match = await bcrypt.compare(plainPassword, hashedPassword);
    return match;
  } catch (err) {
    throw new InternalServerErrorException(
      'There was an error trying to authenticate user',
    );
  }
}
