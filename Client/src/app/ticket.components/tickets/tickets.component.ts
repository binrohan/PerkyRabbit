import { Component, OnInit } from '@angular/core';
import { ApiResponse } from 'src/app/models/ApiResponse';
import { Ticket } from 'src/app/models/Ticket';
import { TicketService } from 'src/app/services/ticket.service';

@Component({
  selector: 'app-tickets',
  templateUrl: './tickets.component.html',
  styleUrls: ['./tickets.component.scss']
})
export class TicketsComponent implements OnInit {

  tickets: Ticket[];

  constructor(private ticketService: TicketService) { }

  ngOnInit(): void {
    this.loadTickets();
  }

  loadTickets() {
      this.ticketService
        .getTickets()
        .subscribe({
          next: (res : ApiResponse<Ticket[]>) => {this.tickets = res.data; console.log(this.tickets)},
          error: (err => alert(err))
        });
    }
}
