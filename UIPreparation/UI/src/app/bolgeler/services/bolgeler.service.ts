import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Bolgeler } from '../models/Bolgeler';
import { environment } from 'environments/environment';


@Injectable({
  providedIn: 'root'
})
export class BolgelerService {

  constructor(private httpClient: HttpClient) { }


  getBolgelerList(): Observable<Bolgeler[]> {

    return this.httpClient.get<Bolgeler[]>(environment.getApiUrl + '/bolgelers/getall')
  }

  getBolgelerById(id: number): Observable<Bolgeler> {
    return this.httpClient.get<Bolgeler>(environment.getApiUrl + '/bolgelers/getbyid?bolgelerId='+id)
  }

  addBolgeler(bolgeler: Bolgeler): Observable<any> {

    return this.httpClient.post(environment.getApiUrl + '/bolgelers/', bolgeler, { responseType: 'text' });
  }

  getBolgelerByUlkeId(ulkeId: any): Observable<any> {
    return this.httpClient.get(environment.getApiUrl + `/bolgelers/getlist?ulkeId=`+ ulkeId);
  }
  

  updateBolgeler(bolgeler: Bolgeler): Observable<any> {
    return this.httpClient.put(environment.getApiUrl + '/bolgelers/', bolgeler, { responseType: 'text' });

  }

  deleteBolgeler(id: number) {
    return this.httpClient.request('delete', environment.getApiUrl + '/bolgelers/', { body: { bolgelerId: id } });
  }

  addFile(formData:FormData): Observable<any>{
    return this.httpClient.post(environment.getApiUrl + '/bolgelers/addPhoto', formData, { responseType: 'text' });
  }


}