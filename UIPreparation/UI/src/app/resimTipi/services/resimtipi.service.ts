import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ResimTipi } from '../models/ResimTipi';
import { environment } from 'environments/environment';


@Injectable({
  providedIn: 'root'
})
export class ResimTipiService {

  constructor(private httpClient: HttpClient) { }


  getResimTipiList(): Observable<ResimTipi[]> {

    return this.httpClient.get<ResimTipi[]>(environment.getApiUrl + '/resimTipis/getall')
  }

  getResimTipiById(id: number): Observable<ResimTipi> {
    return this.httpClient.get<ResimTipi>(environment.getApiUrl + '/resimTipis/getbyid?resimTipiId='+id)
  }

  addResimTipi(resimTipi: ResimTipi): Observable<any> {

    return this.httpClient.post(environment.getApiUrl + '/resimTipis/', resimTipi, { responseType: 'text' });
  }

  updateResimTipi(resimTipi: ResimTipi): Observable<any> {
    return this.httpClient.put(environment.getApiUrl + '/resimTipis/', resimTipi, { responseType: 'text' });

  }

  deleteResimTipi(id: number) {
    return this.httpClient.request('delete', environment.getApiUrl + '/resimTipis/', { body: { resimTipiId: id } });
  }


}