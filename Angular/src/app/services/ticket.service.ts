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

  updateTicket(id: number, ticket: Ticket): Observable<ApiResponse> {
    return this.http.put<ApiResponse>(this.baseUrl + id, ticket);
  }

  deleteTicket(id: number): Observable<ApiResponse> {
    return this.http.delete<ApiResponse>(this.baseUrl + id);
  }

  addTicket(ticket: Ticket): Observable<ApiResponse<Ticket>> {
    return this.http.post<ApiResponse<Ticket>>(this.baseUrl, ticket);
  }
}