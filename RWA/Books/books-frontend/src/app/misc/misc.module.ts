import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FaqComponent } from './faq/faq.component';
import { MatButtonModule } from '@angular/material/button';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatIconModule } from '@angular/material/icon';
import { MatToolbarModule } from '@angular/material/toolbar';
import { PrivacyPolicyComponent } from './privacy-policy/privacy-policy.component';
import { MatCardModule } from '@angular/material/card';
import { PageNotFoundComponent } from './page-not-found/page-not-found.component';

@NgModule({
  declarations: [FaqComponent, PrivacyPolicyComponent, PageNotFoundComponent],
  imports: [
    CommonModule,
    MatToolbarModule,
    MatButtonModule,
    MatIconModule,
    MatExpansionModule,
    MatCardModule,
  ],
  exports: [FaqComponent, PrivacyPolicyComponent],
})
export class MiscModule {}
