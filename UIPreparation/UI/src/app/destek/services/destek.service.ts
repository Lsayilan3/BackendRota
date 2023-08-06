import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Destek } from '../models/Destek';
import { environment } from 'environments/environment';


@Injectable({
  providedIn: 'root'
})
export class DestekService {

  constructor(private httpClient: HttpClient) { }


  getDestekList(): Observable<Destek[]> {

    return this.httpClient.get<Destek[]>(environment.getApiUrl + '/desteks/getall')
  }

  getDestekById(id: number): Observable<Destek> {
    return this.httpClient.get<Destek>(environment.getApiUrl + '/desteks/getbyid?destekId='+id)
  }

  addDestek(destek: Destek): Observable<any> {

    return this.httpClient.post(environment.getApiUrl + '/desteks/', destek, { responseType: 'text' });
  }

  updateDestek(destek: Destek): Observable<any> {
    return this.httpClient.put(environment.getApiUrl + '/desteks/', destek, { responseType: 'text' });

  }

  deleteDestek(id: number) {
    return this.httpClient.request('delete', environment.getApiUrl + '/desteks/', { body: { destekId: id } });
  }

  addFile(formData:FormData): Observable<any>{
    return this.httpClient.post(environment.getApiUrl + '/desteks/addPhoto', formData, { responseType: 'text' });
  }

}