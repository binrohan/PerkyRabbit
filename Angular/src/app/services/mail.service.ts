import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { ApiResponse } from '../models/ApiResponse';
import { Mail } from '../models/Mail';

@Injectable({
  providedIn: 'root'
})
export class MailService {
  baseUrl = environment.apiUrl + 'mails/';

  constructor(private http: HttpClient) { }

  getMails(): Observable<ApiResponse<Mail[]>> {
    return this.http.get<ApiResponse<Mail[]>>(this.baseUrl);
  }

  deleteMail(id: number): Observable<ApiResponse> {
    return this.http.delete<ApiResponse>(this.baseUrl + id);
  }

  sendMail(mail: Mail): Observable<ApiResponse<Mail>> {
    return this.http.post<ApiResponse<Mail>>(this.baseUrl, mail);
  }
}
