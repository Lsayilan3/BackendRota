import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Gunler } from '../models/Gunler';
import { environment } from 'environments/environment';


@Injectable({
  providedIn: 'root'
})
export class GunlerService {

  constructor(private httpClient: HttpClient) { }


  getGunlerList(): Observable<Gunler[]> {

    return this.httpClient.get<Gunler[]>(environment.getApiUrl + '/gunlers/getall')
  }

  getGunlerById(id: number): Observable<Gunler> {
    return this.httpClient.get<Gunler>(environment.getApiUrl + '/gunlers/getbyid?gunlerId='+id)
  }

  addGunler(gunler: Gunler): Observable<any> {

    return this.httpClient.post(environment.getApiUrl + '/gunlers/', gunler, { responseType: 'text' });
  }

  updateGunler(gunler: Gunler): Observable<any> {
    return this.httpClient.put(environment.getApiUrl + '/gunlers/', gunler, { responseType: 'text' });

  }

  deleteGunler(id: number) {
    return this.httpClient.request('delete', environment.getApiUrl + '/gunlers/', { body: { gunlerId: id } });
  }


}