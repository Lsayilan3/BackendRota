import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Partner } from '../models/Partner';
import { environment } from 'environments/environment';


@Injectable({
  providedIn: 'root'
})
export class PartnerService {

  constructor(private httpClient: HttpClient) { }


  getPartnerList(): Observable<Partner[]> {

    return this.httpClient.get<Partner[]>(environment.getApiUrl + '/partners/getall')
  }

  getPartnerById(id: number): Observable<Partner> {
    return this.httpClient.get<Partner>(environment.getApiUrl + '/partners/getbyid?partnerId='+id)
  }

  addPartner(partner: Partner): Observable<any> {

    return this.httpClient.post(environment.getApiUrl + '/partners/', partner, { responseType: 'text' });
  }

  updatePartner(partner: Partner): Observable<any> {
    return this.httpClient.put(environment.getApiUrl + '/partners/', partner, { responseType: 'text' });

  }

  deletePartner(id: number) {
    return this.httpClient.request('delete', environment.getApiUrl + '/partners/', { body: { partnerId: id } });
  }

  addFile(formData:FormData): Observable<any>{
    return this.httpClient.post(environment.getApiUrl + '/partners/addPhoto', formData, { responseType: 'text' });
  }

}