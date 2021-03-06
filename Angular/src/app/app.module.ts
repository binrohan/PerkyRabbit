import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule } from '@angular/common/http';

import {MatToolbarModule} from '@angular/material/toolbar';
import {MatIconModule} from '@angular/material/icon';
import {MatDialogModule} from '@angular/material/dialog';
import {MatTableModule} from '@angular/material/table';
import {MatButtonModule} from '@angular/material/button';
import {MatFormFieldModule} from '@angular/material/form-field';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import {MatInputModule} from '@angular/material/input';
import {MatTabsModule} from '@angular/material/tabs';

import { TicketsComponent } from './ticket.components/tickets/tickets.component';
import { AddTicketDialogComponent } from './ticket.components/add-ticket-dialog/add-ticket-dialog.component';
import { MailsComponent } from './mails/mails/mails.component';
import { ReadMailDialogComponent } from './mails/read-mail-dialog/read-mail-dialog.component';
import { SendMailDialogComponent } from './mails/send-mail-dialog/send-mail-dialog.component';
import { SentMailsComponent } from './mails/sent-mails/sent-mails.component';
import { TrashMailsComponent } from './mails/trash-mails/trash-mails.component';

@NgModule({
  declarations: [
    AppComponent,
    TicketsComponent,
    AddTicketDialogComponent,
    MailsComponent,
    ReadMailDialogComponent,
    SendMailDialogComponent,
    SentMailsComponent,
    TrashMailsComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MatToolbarModule,
    MatIconModule,
    MatDialogModule,
    MatTableModule,
    HttpClientModule,
    MatButtonModule,
    MatFormFieldModule,
    FormsModule,
    MatInputModule,
    ReactiveFormsModule,
    MatTabsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
