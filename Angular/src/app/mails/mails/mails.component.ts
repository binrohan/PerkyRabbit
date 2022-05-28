import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-mails',
  templateUrl: './mails.component.html',
  styleUrls: ['./mails.component.scss']
})
export class MailsComponent implements OnInit {
  displayedColumns: string[] = [
    'id',
    'title',
    'price',
    'description',
    'createdAt',
    'actions',
  ];

  constructor() { }

  ngOnInit(): void {
  }

}
