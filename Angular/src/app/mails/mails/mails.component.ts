import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ApiResponse } from 'src/app/models/ApiResponse';
import { Mail } from 'src/app/models/Mail';
import { MailService } from 'src/app/services/mail.service';
import { SendMailDialogComponent } from '../send-mail-dialog/send-mail-dialog.component';

@Component({
  selector: 'app-mails',
  templateUrl: './mails.component.html',
  styleUrls: ['./mails.component.scss']
})
export class MailsComponent implements OnInit {
  constructor() { }

  ngOnInit(): void {

  }
}
