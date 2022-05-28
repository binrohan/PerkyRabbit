import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MailsComponent } from './mails/mails/mails.component';
import { TicketsComponent } from './ticket.components/tickets/tickets.component';

const routes: Routes = [
  { path: '', component: TicketsComponent },
  { path: 'tickets', component: TicketsComponent },
  { path: 'mails', component: MailsComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
