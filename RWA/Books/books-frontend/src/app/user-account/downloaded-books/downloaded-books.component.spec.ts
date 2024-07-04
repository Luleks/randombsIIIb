import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DownloadedBooksComponent } from './downloaded-books.component';

describe('DownloadedBooksComponent', () => {
  let component: DownloadedBooksComponent;
  let fixture: ComponentFixture<DownloadedBooksComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [DownloadedBooksComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DownloadedBooksComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
