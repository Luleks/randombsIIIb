import { Component } from '@angular/core';

@Component({
  selector: 'app-home-page',
  templateUrl: './home-page.component.html',
  styleUrl: './home-page.component.scss',
})
export class HomePageComponent {
  exploreClicked() {
    document
      .querySelector('.filter-form')
      ?.scrollIntoView({ behavior: 'smooth', block: 'start' });
  }
}
