import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ReadMailDialogComponent } from './read-mail-dialog.component';

describe('ReadMailDialogComponent', () => {
  let component: ReadMailDialogComponent;
  let fixture: ComponentFixture<ReadMailDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ReadMailDialogComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ReadMailDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
