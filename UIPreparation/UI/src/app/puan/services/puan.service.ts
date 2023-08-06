import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Puan } from '../models/Puan';
import { environment } from 'environments/environment';


@Injectable({
  providedIn: 'root'
})
export class PuanService {

  constructor(private httpClient: HttpClient) { }


  getPuanList(): Observable<Puan[]> {

    return this.httpClient.get<Puan[]>(environment.getApiUrl + '/puans/getall')
  }

  getPuanById(id: number): Observable<Puan> {
    return this.httpClient.get<Puan>(environment.getApiUrl + '/puans/getbyid?puanId='+id)
  }

  addPuan(puan: Puan): Observable<any> {

    return this.httpClient.post(environment.getApiUrl + '/puans/', puan, { responseType: 'text' });
  }

  updatePuan(puan: Puan): Observable<any> {
    return this.httpClient.put(environment.getApiUrl + '/puans/', puan, { responseType: 'text' });

  }

  deletePuan(id: number) {
    return this.httpClient.request('delete', environment.getApiUrl + '/puans/', { body: { puanId: id } });
  }


}