import { Action, createReducer, on } from '@ngrx/store';
import { login, logout } from './user.actions';
import { User } from '../../interfaces';

export interface UserState {
  user: User | null;
}

export const initialState: UserState = {
  user: null,
};

const _userReducer = createReducer(
  initialState,
  on(login, (state, { user }) => ({
    ...state,
    user: {
      ...user,
      access_token: user.access_token ?? state.user?.access_token,
    },
  })),
  on(logout, (state) => ({ ...state, user: null }))
);

export function userReducer(
  state: UserState | undefined,
  action: Action
): UserState {
  return _userReducer(state, action);
}
