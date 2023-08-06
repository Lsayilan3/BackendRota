import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Ulke } from '../models/Ulke';
import { environment } from 'environments/environment';


@Injectable({
  providedIn: 'root'
})
export class UlkeService {

  constructor(private httpClient: HttpClient) { }


  getUlkeList(): Observable<Ulke[]> {

    return this.httpClient.get<Ulke[]>(environment.getApiUrl + '/ulkes/getall')
  }

  getUlkeById(id: number): Observable<Ulke> {
    return this.httpClient.get<Ulke>(environment.getApiUrl + '/ulkes/getbyid?ulkeId='+id)
  }

  addUlke(ulke: Ulke): Observable<any> {

    return this.httpClient.post(environment.getApiUrl + '/ulkes/', ulke, { responseType: 'text' });
  }

  updateUlke(ulke: Ulke): Observable<any> {
    return this.httpClient.put(environment.getApiUrl + '/ulkes/', ulke, { responseType: 'text' });

  }

  deleteUlke(id: number) {
    return this.httpClient.request('delete', environment.getApiUrl + '/ulkes/', { body: { ulkeId: id } });
  }

  addFile(formData:FormData): Observable<any>{
    return this.httpClient.post(environment.getApiUrl + '/ulkes/addPhoto', formData, { responseType: 'text' });
  }


}