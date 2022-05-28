import { Component, OnInit } from '@angular/core';
import { ApiResponse } from 'src/app/models/ApiResponse';
import { Ticket } from 'src/app/models/Ticket';
import { TicketService } from 'src/app/services/ticket.service';
import {MatDialog, MatDialogRef} from '@angular/material/dialog';
import { AddTicketDialogComponent } from '../add-ticket-dialog/add-ticket-dialog.component';

@Component({
  selector: 'app-tickets',
  templateUrl: './tickets.component.html',
  styleUrls: ['./tickets.component.scss'],
})
export class TicketsComponent implements OnInit {
  tickets: Ticket[] = new Array();
  displayedColumns: string[] = ['id', 'title', 'price', 'description', 'createdAt', 'actions'];

  constructor(private ticketService: TicketService, public dialog: MatDialog) {}

  ngOnInit(): void {
    this.loadTickets();
  }

  loadTickets() {
    this.ticketService.getTickets().subscribe({
      next: (res: ApiResponse<Ticket[]>) => {
        this.tickets = res.data;
        console.log(this.tickets);
      },
      error: (err) => console.log(err),
  });
  }

  addTicket(ticket: Ticket) {
    this.ticketService.addTicket(ticket).subscribe({
      next: (res: ApiResponse<Ticket>) => {
        this.tickets.pop();
        this.tickets = [...this.tickets, res.data];
      },
      error: (err) => {
        console.log(err);
        this.tickets = this.tickets.slice(0, -1);
      }
    });
  }

  updateTicket(ticket: Ticket) {

  }

  openDialog(ticket?: Ticket) {
    const dialogRef = this.dialog.open(AddTicketDialogComponent, {
      width: '500px',
      data: ticket ?? {},
    });

    dialogRef.afterClosed().subscribe((ticket: Ticket) => {
      if(ticket?.id) this.updateTicket(ticket);

      if(ticket) {
        this.tickets = [...this.tickets, ticket];
        this.addTicket(ticket);
      }
    });
  }
}
