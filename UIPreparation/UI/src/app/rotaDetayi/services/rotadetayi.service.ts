import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { RotaDetayi } from '../models/RotaDetayi';
import { environment } from 'environments/environment';


@Injectable({
  providedIn: 'root'
})
export class RotaDetayiService {

  constructor(private httpClient: HttpClient) { }


  getRotaDetayiList(): Observable<RotaDetayi[]> {

    return this.httpClient.get<RotaDetayi[]>(environment.getApiUrl + '/rotaDetayis/getall')
  }

  getRotaDetayiById(id: number): Observable<RotaDetayi> {
    return this.httpClient.get<RotaDetayi>(environment.getApiUrl + '/rotaDetayis/getbyid?rotaDetayiId='+id)
  }

  addRotaDetayi(rotaDetayi: RotaDetayi): Observable<any> {

    return this.httpClient.post(environment.getApiUrl + '/rotaDetayis/', rotaDetayi, { responseType: 'text' });
  }

  updateRotaDetayi(rotaDetayi: RotaDetayi): Observable<any> {
    return this.httpClient.put(environment.getApiUrl + '/rotaDetayis/', rotaDetayi, { responseType: 'text' });

  }

  deleteRotaDetayi(id: number) {
    return this.httpClient.request('delete', environment.getApiUrl + '/rotaDetayis/', { body: { rotaDetayiId: id } });
  }

  addFile(formData:FormData): Observable<any>{
    return this.httpClient.post(environment.getApiUrl + '/rotaDetayis/addPhoto', formData, { responseType: 'text' });
  }


}