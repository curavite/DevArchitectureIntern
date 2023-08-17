import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Error } from '../models/Error';
import { environment } from 'environments/environment';


@Injectable({
  providedIn: 'root'
})
export class ErrorService {

  constructor(private httpClient: HttpClient) { }


  getErrorList(): Observable<Error[]> {

    return this.httpClient.get<Error[]>(environment.getApiUrl + '/errors/getall')
  }

  getErrorById(id: number): Observable<Error> {
    return this.httpClient.get<Error>(environment.getApiUrl + '/errors/getbyid?id='+id)
  }

  addError(error: Error): Observable<any> {

    return this.httpClient.post(environment.getApiUrl + '/errors/', error, { responseType: 'text' });
  }

  updateError(error: Error): Observable<any> {
    return this.httpClient.put(environment.getApiUrl + '/errors/', error, { responseType: 'text' });

  }

  deleteError(id: number) {
    return this.httpClient.request('delete', environment.getApiUrl + '/errors/', { body: { id: id } });
  }


}