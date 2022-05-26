import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { Ticket } from '../models/Ticket';
import { ApiResponse } from '../models/ApiResponse';

@Injectable({
  providedIn: 'root'
})

export class TicketService {
  baseUrl = environment.apiUrl + 'tickets/';

  constructor(private http: HttpClient) { }
  

  getTicket(id: number): Observable<ApiResponse<Ticket>> {
    return this.http.get<ApiResponse<Ticket>>(this.baseUrl + id);
  }

  getTickets(): Observable<ApiResponse<Ticket[]>> {
    return this.http.get<ApiResponse<Ticket[]>>(this.baseUrl);
  }

  updateUser(id: number, ticket: Ticket) {
    return this.http.put(this.baseUrl + id, ticket);
  }

  deleteTicket(id: number) {
    return this.http.delete(this.baseUrl + id);
  }

  createTicket(ticket: Ticket) {
    return this.http.post(this.baseUrl, ticket);
  }
}