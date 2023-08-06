import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Sehir } from '../models/Sehir';
import { environment } from 'environments/environment';


@Injectable({
  providedIn: 'root'
})
export class SehirService {

  constructor(private httpClient: HttpClient) { }


  getSehirList(): Observable<Sehir[]> {

    return this.httpClient.get<Sehir[]>(environment.getApiUrl + '/sehirs/getall')
  }

  getSehirById(id: number): Observable<Sehir> {
    return this.httpClient.get<Sehir>(environment.getApiUrl + '/sehirs/getbyid?sehirId='+id)
  }

  addSehir(sehir: Sehir): Observable<any> {

    return this.httpClient.post(environment.getApiUrl + '/sehirs/', sehir, { responseType: 'text' });
  }

  updateSehir(sehir: Sehir): Observable<any> {
    return this.httpClient.put(environment.getApiUrl + '/sehirs/', sehir, { responseType: 'text' });

  }

  deleteSehir(id: number) {
    return this.httpClient.request('delete', environment.getApiUrl + '/sehirs/', { body: { sehirId: id } });
  }

  addFile(formData:FormData): Observable<any>{
    return this.httpClient.post(environment.getApiUrl + '/sehirs/addPhoto', formData, { responseType: 'text' });
  }

}