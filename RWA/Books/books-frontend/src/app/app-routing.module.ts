import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomePageComponent } from './home-page/home-page/home-page.component';
import { FaqComponent } from './misc/faq/faq.component';
import { PrivacyPolicyComponent } from './misc/privacy-policy/privacy-policy.component';
import { LoginComponent } from './auth/login/login.component';
import { RegisterComponent } from './auth/register/register.component';
import { UserAccountComponent } from './user-account/user-account/user-account.component';
import { BookPageComponent } from './books/book-page/book-page.component';
import { PageNotFoundComponent } from './misc/page-not-found/page-not-found.component';

const routes: Routes = [
  { path: '', component: HomePageComponent },
  { path: 'faq', component: FaqComponent },
  { path: 'privacy', component: PrivacyPolicyComponent },
  { path: 'login', component: LoginComponent },
  { path: 'signup', component: RegisterComponent },
  { path: 'account', component: UserAccountComponent },
  { path: 'books/:id', component: BookPageComponent },
  { path: '**', component: PageNotFoundComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
