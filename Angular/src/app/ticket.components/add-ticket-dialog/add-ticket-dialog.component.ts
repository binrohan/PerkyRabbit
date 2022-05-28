import { Component, Inject, OnInit } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Ticket } from 'src/app/models/Ticket';

@Component({
  selector: 'app-add-ticket-dialog',
  templateUrl: './add-ticket-dialog.component.html',
  styleUrls: ['./add-ticket-dialog.component.scss'],
})
export class AddTicketDialogComponent implements OnInit {
  title = new FormControl('', [Validators.required]);
  price = new FormControl('', [Validators.required, Validators.min(0)]);
  description = new FormControl('', [Validators.required, Validators.minLength(5)]);

  constructor(
    public dialogRef: MatDialogRef<AddTicketDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public ticket: Ticket
  ) {}

  ngOnInit(): void {}

  onNoClick() {
    this.dialogRef.close();
  }

  onAddTicket() {
    if(this.title.invalid || this.price.invalid || this.description.invalid){
      this.getTitleErrorMessage();
      this.getDescriptionErrorMessage();
      this.getPriceErrorMessage();
      return;
    }

    this.ticket.createdAt = new Date();
    this.dialogRef.close(this.ticket);
  }

  getTitleErrorMessage() {
    if(!this.title.invalid) return;
    
    if (this.title.hasError('required')) {
      return 'You must enter a value';
    }

    return 'Value is invalid';
  }

  getPriceErrorMessage() {
    if(!this.price.invalid) return;

    if (this.price.hasError('required')) {
      return 'You must enter a value';
    }
    return this.price.hasError('min') ? 'Price can not be less than 0' : '';
  }

  getDescriptionErrorMessage() {
    if(!this.description.invalid) return;

    if (this.price.hasError('required') && this.description.invalid) {
      return 'You must enter a value';
    }
    return this.price.hasError('minLength') ? 'Minimum 5 characters required' : '';
  }
}
