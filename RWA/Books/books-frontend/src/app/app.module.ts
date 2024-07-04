import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { HomePageModule } from './home-page/home-page.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FrameModule } from './frame/frame.module';
import { MiscModule } from './misc/misc.module';
import { AuthModule } from './auth/auth.module';
import { StoreModule } from '@ngrx/store';
import { userReducer } from './store/user/user.reducer';
import { UserAccountModule } from './user-account/user-account.module';
import { userAccountReducer } from './store/user/user-books.reducer';
import { EffectsModule } from '@ngrx/effects';
import { UserAccountEffects } from './store/user/user.effects';
import { BooksModule } from './books/books.module';

@NgModule({
  declarations: [AppComponent],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HomePageModule,
    FrameModule,
    MiscModule,
    AuthModule,
    BooksModule,
    UserAccountModule,
    StoreModule.forRoot({ user: userReducer, userAccount: userAccountReducer }),
    EffectsModule.forRoot([UserAccountEffects]),
  ],
  providers: [provideAnimationsAsync()],
  bootstrap: [AppComponent],
})
export class AppModule {}
