import { Injectable, CanActivate, ExecutionContext } from '@nestjs/common';
import { SetMetadata } from '@nestjs/common';
import { Reflector } from '@nestjs/core';

@Injectable()
export class RolesGuard implements CanActivate {
  constructor(private reflector: Reflector) {}

  canActivate(context: ExecutionContext): boolean {
    const roles = this.reflector.get<string[]>('roles', context.getHandler());
    if (!roles) {
      return true;
    }
    const request = context.switchToHttp().getRequest();
    const user = request.user;

    if (!user || !user.roles) {
      return false;
    }
    const userRoles = Array.isArray(user.roles) ? user.roles : [user.roles];
    return roles.some((role) => userRoles.includes(role));
  }
}

export const Roles = (...roles: string[]) => SetMetadata('roles', roles);
