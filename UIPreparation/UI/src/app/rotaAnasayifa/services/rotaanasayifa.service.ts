import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { RotaAnasayifa } from '../models/RotaAnasayifa';
import { environment } from 'environments/environment';


@Injectable({
  providedIn: 'root'
})
export class RotaAnasayifaService {

  constructor(private httpClient: HttpClient) { }


  getRotaAnasayifaList(): Observable<RotaAnasayifa[]> {

    return this.httpClient.get<RotaAnasayifa[]>(environment.getApiUrl + '/rotaAnasayifas/getall')
  }

  getRotaAnasayifaById(id: number): Observable<RotaAnasayifa> {
    return this.httpClient.get<RotaAnasayifa>(environment.getApiUrl + '/rotaAnasayifas/getbyid?rotaAnasayifaId='+id)
  }

  addRotaAnasayifa(rotaAnasayifa: RotaAnasayifa): Observable<any> {

    return this.httpClient.post(environment.getApiUrl + '/rotaAnasayifas/', rotaAnasayifa, { responseType: 'text' });
  }

  updateRotaAnasayifa(rotaAnasayifa: RotaAnasayifa): Observable<any> {
    return this.httpClient.put(environment.getApiUrl + '/rotaAnasayifas/', rotaAnasayifa, { responseType: 'text' });

  }

  deleteRotaAnasayifa(id: number) {
    return this.httpClient.request('delete', environment.getApiUrl + '/rotaAnasayifas/', { body: { rotaAnasayifaId: id } });
  }
  
  addFile(formData:FormData): Observable<any>{
    return this.httpClient.post(environment.getApiUrl + '/rotaAnasayifas/addPhoto', formData, { responseType: 'text' });
  }

}