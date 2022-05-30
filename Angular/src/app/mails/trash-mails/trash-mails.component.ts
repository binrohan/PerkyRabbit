import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ApiResponse } from 'src/app/models/ApiResponse';
import { Mail } from 'src/app/models/Mail';
import { MailService } from 'src/app/services/mail.service';
import { SendMailDialogComponent } from '../send-mail-dialog/send-mail-dialog.component';

@Component({
  selector: 'app-trash-mails',
  templateUrl: './trash-mails.component.html',
  styleUrls: ['./trash-mails.component.scss']
})
export class TrashMailsComponent implements OnInit {
  mails: Mail[] = new Array();
  displayedColumns: string[] = [
    'id',
    'to',
    'subject',
    'body',
    'createdAt',
    'actions',
  ];

  constructor(private mailService: MailService, public dialog: MatDialog) { }

  ngOnInit(): void {
    this.loadMails();
  }

  loadMails() {
    this.mailService.getMails(true).subscribe({
      next: (res: ApiResponse<Mail[]>) => {
        this.mails = res.data;
        console.log(this.mails);
      },
      error: (err) => console.log(err),
    });
  }

  deleteMail(mail: Mail) {
    this.mailService.removeMail(mail.id).subscribe({
      next: (res: ApiResponse) => {
        this.loadMails();
      },
      error: (err) => {
        console.log(err);
      },
    });
  }

  openDialog(mail: Mail) {
    this.dialog.open(SendMailDialogComponent, {
      width: '500px',
      data: mail,
    });
  }

}
