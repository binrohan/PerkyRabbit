import { Component, OnInit } from '@angular/core';
import { ApiResponse } from 'src/app/models/ApiResponse';
import { Ticket } from 'src/app/models/Ticket';
import { TicketService } from 'src/app/services/ticket.service';
import { MatDialog } from '@angular/material/dialog';
import { AddTicketDialogComponent } from '../add-ticket-dialog/add-ticket-dialog.component';

@Component({
  selector: 'app-tickets',
  templateUrl: './tickets.component.html',
  styleUrls: ['./tickets.component.scss'],
})
export class TicketsComponent implements OnInit {
  private tempTicket: Ticket;
  tickets: Ticket[] = new Array();
  displayedColumns: string[] = [
    'id',
    'title',
    'price',
    'description',
    'createdAt',
    'actions',
  ];

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
      },
    });
  }

  updateTicket(ticket: Ticket) {
    this.ticketService.updateTicket(ticket.id, ticket).subscribe({
      next: (res: ApiResponse) => {
        // update succeeded snackbar
      },
      error: (err) => {
        console.log(err);
        this.eagerUpdate(ticket, this.tempTicket);
        // update failed snackbar
      },
    });
  }

  deleteTicket(ticket: Ticket) {
    const tempTickets = [...this.tickets];
    this.tickets = this.tickets.filter(t => t.id !== ticket.id);

    this.ticketService.deleteTicket(ticket.id).subscribe({
      next: (res: ApiResponse) => {
        // delete succeeded snackbar
      },
      error: (err) => {
        console.log(err);
        this.tickets = [...tempTickets];
        // delete failed snackbar
      },
    });
  }

  eagerUpdate(ticket: Ticket, tempTicket: Ticket): void {
    this.tickets = this.tickets.map<Ticket>((t) => {
      if (t.id === ticket.id) t = tempTicket;
      return t;
    });
  }

  openDialog(ticket?: Ticket) {
    const dialogRef = this.dialog.open(AddTicketDialogComponent, {
      width: '500px',
      data: ticket ?? {},
    });

    dialogRef.afterClosed().subscribe((ticket: Ticket) => {
      if (!ticket) return;

      if (ticket?.id) {
        this.eagerUpdate(ticket, ticket);
        this.tempTicket = ticket;
        this.updateTicket(ticket);
        return;
      }

      this.tickets = [...this.tickets, ticket];
      this.addTicket(ticket);
    });
  }
}
