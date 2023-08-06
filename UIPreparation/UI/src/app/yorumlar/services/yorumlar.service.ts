import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Yorumlar } from '../models/Yorumlar';
import { environment } from 'environments/environment';


@Injectable({
  providedIn: 'root'
})
export class YorumlarService {

  constructor(private httpClient: HttpClient) { }


  getYorumlarList(): Observable<Yorumlar[]> {

    return this.httpClient.get<Yorumlar[]>(environment.getApiUrl + '/yorumlars/getall')
  }

  getYorumlarById(id: number): Observable<Yorumlar> {
    return this.httpClient.get<Yorumlar>(environment.getApiUrl + '/yorumlars/getbyid?yorumlarId='+id)
  }

  addYorumlar(yorumlar: Yorumlar): Observable<any> {

    return this.httpClient.post(environment.getApiUrl + '/yorumlars/', yorumlar, { responseType: 'text' });
  }

  updateYorumlar(yorumlar: Yorumlar): Observable<any> {
    return this.httpClient.put(environment.getApiUrl + '/yorumlars/', yorumlar, { responseType: 'text' });

  }

  deleteYorumlar(id: number) {
    return this.httpClient.request('delete', environment.getApiUrl + '/yorumlars/', { body: { yorumlarId: id } });
  }


}