import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Kategori } from '../models/Kategori';
import { environment } from 'environments/environment';


@Injectable({
  providedIn: 'root'
})
export class KategoriService {

  constructor(private httpClient: HttpClient) { }


  getKategoriList(): Observable<Kategori[]> {

    return this.httpClient.get<Kategori[]>(environment.getApiUrl + '/kategoris/getall')
  }

  getKategoriById(id: number): Observable<Kategori> {
    return this.httpClient.get<Kategori>(environment.getApiUrl + '/kategoris/getbyid?kategoriId='+id)
  }

  addKategori(kategori: Kategori): Observable<any> {

    return this.httpClient.post(environment.getApiUrl + '/kategoris/', kategori, { responseType: 'text' });
  }

  updateKategori(kategori: Kategori): Observable<any> {
    return this.httpClient.put(environment.getApiUrl + '/kategoris/', kategori, { responseType: 'text' });

  }

  deleteKategori(id: number) {
    return this.httpClient.request('delete', environment.getApiUrl + '/kategoris/', { body: { kategoriId: id } });
  }

  addFile(formData:FormData): Observable<any>{
    return this.httpClient.post(environment.getApiUrl + '/kategoris/addPhoto', formData, { responseType: 'text' });
  }

}