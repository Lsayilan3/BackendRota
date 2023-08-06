import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Rota } from '../models/Rota';
import { environment } from 'environments/environment';
import { RotaDetayi } from 'app/rotaDetayi/models/RotaDetayi';
import { RotaGaleri } from 'app/rotaGaleri/models/RotaGaleri';


@Injectable({
  providedIn: 'root'
})
export class RotaService {

  constructor(private httpClient: HttpClient) { }


  getRotaList(): Observable<Rota[]> {

    return this.httpClient.get<Rota[]>(environment.getApiUrl + '/rotas/getall')
  }

  getRotaById(id: number): Observable<Rota> {
    return this.httpClient.get<Rota>(environment.getApiUrl + '/rotas/getbyid?rotaId='+id)
  }

///////////
getRotaDetayById(id: number): Observable<RotaDetayi[]> {
  return this.httpClient.get<RotaDetayi[]>(environment.getApiUrl + '/rotaDetayis/getlist?rotaId=' + id);
}


  getRotaGaleriById(id: number): Observable<RotaGaleri[]> {
    return this.httpClient.get<RotaGaleri[]>(environment.getApiUrl + '/rotaGaleris/getlist?rotaId=' + id);
  }


  addRota(rota: Rota): Observable<any> {

    return this.httpClient.post(environment.getApiUrl + '/rotas/', rota, { responseType: 'text' });
  }

  updateRota(rota: Rota): Observable<any> {
    return this.httpClient.put(environment.getApiUrl + '/rotas/', rota, { responseType: 'text' });

  }

  deleteRota(id: number) {
    return this.httpClient.request('delete', environment.getApiUrl + '/rotas/', { body: { rotaId: id } });
  }

  addFile(formData:FormData): Observable<any>{
    return this.httpClient.post(environment.getApiUrl + '/rotas/addPhoto', formData, { responseType: 'text' });
  }

}