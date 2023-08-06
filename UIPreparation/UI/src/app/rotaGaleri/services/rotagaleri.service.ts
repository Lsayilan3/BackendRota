import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { RotaGaleri } from '../models/RotaGaleri';
import { environment } from 'environments/environment';


@Injectable({
  providedIn: 'root'
})
export class RotaGaleriService {

  constructor(private httpClient: HttpClient) { }


  getRotaGaleriList(): Observable<RotaGaleri[]> {

    return this.httpClient.get<RotaGaleri[]>(environment.getApiUrl + '/rotaGaleris/getall')
  }

  getRotaGaleriById(id: number): Observable<RotaGaleri> {
    return this.httpClient.get<RotaGaleri>(environment.getApiUrl + '/rotaGaleris/getbyid?rotaGaleriId='+id)
  }

  addRotaGaleri(rotaGaleri: RotaGaleri): Observable<any> {

    return this.httpClient.post(environment.getApiUrl + '/rotaGaleris/', rotaGaleri, { responseType: 'text' });
  }

  updateRotaGaleri(rotaGaleri: RotaGaleri): Observable<any> {
    return this.httpClient.put(environment.getApiUrl + '/rotaGaleris/', rotaGaleri, { responseType: 'text' });

  }

  deleteRotaGaleri(id: number) {
    return this.httpClient.request('delete', environment.getApiUrl + '/rotaGaleris/', { body: { rotaGaleriId: id } });
  }

  addFile(formData:FormData): Observable<any>{
    return this.httpClient.post(environment.getApiUrl + '/rotaGaleris/addPhoto', formData, { responseType: 'text' });
  }

}