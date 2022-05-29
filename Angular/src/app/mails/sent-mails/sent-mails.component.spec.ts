import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SentMailsComponent } from './sent-mails.component';

describe('SentMailsComponent', () => {
  let component: SentMailsComponent;
  let fixture: ComponentFixture<SentMailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SentMailsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SentMailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
