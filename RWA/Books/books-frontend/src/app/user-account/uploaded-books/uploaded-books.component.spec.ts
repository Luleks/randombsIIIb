import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UploadedBooksComponent } from './uploaded-books.component';

describe('UploadedBooksComponent', () => {
  let component: UploadedBooksComponent;
  let fixture: ComponentFixture<UploadedBooksComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [UploadedBooksComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UploadedBooksComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
