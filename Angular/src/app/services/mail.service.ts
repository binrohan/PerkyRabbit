import { HttpClient, HttpParams } from '@angular/common/http';
import { FnParam } from '@angular/compiler/src/output/output_ast';
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

  getMails(isDeleted: boolean = false): Observable<ApiResponse<Mail[]>> {
    let params = new HttpParams().append('isDeleted', isDeleted);

    return this.http.get<ApiResponse<Mail[]>>(this.baseUrl, {params: {isDeleted: isDeleted}});
  }

  removeMail(id: number): Observable<ApiResponse> {
    return this.http.patch<ApiResponse>(this.baseUrl + 'remove/' + id, {});
  }

  sendMail(mail: Mail): Observable<ApiResponse<Mail>> {
    return this.http.post<ApiResponse<Mail>>(this.baseUrl + 'send', mail);
  }
}
