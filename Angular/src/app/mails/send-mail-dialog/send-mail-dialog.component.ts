import { Component, Inject, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Mail } from 'src/app/models/Mail';

@Component({
  selector: 'app-send-mail-dialog',
  templateUrl: './send-mail-dialog.component.html',
  styleUrls: ['./send-mail-dialog.component.scss']
})
export class SendMailDialogComponent implements OnInit {
  
  mailForm = new FormGroup({
    subject: new FormControl(this.mail.subject, Validators.required),
    to: new FormControl(this.mail.to, [Validators.required, Validators.email]),
    cc: new FormControl(this.mail.cc),
    bcc: new FormControl(this.mail.bcc),
    body: new FormControl(this.mail.body, Validators.required),
  });

  constructor(public dialogRef: MatDialogRef<SendMailDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public mail: Mail) { }

  ngOnInit(): void {
  }

  onNoClick(){
    this.dialogRef.close();
  }


  onSubmit() {
    if(this.mailForm.invalid) return;

    this.mail.createdAt =  new Date();
    
    this.dialogRef.close(this.mail);
  }

}
