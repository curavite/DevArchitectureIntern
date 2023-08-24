import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { FloorControllError } from '../models/FloorControllError';
import { environment } from 'environments/environment';
import { FloorErrorDto } from '../models/floorerrorDto';


@Injectable({
  providedIn: 'root'
})
export class FloorControllErrorService {

  constructor(private httpClient: HttpClient) { }


  getFloorControllErrorList(): Observable<FloorControllError[]> {

    return this.httpClient.get<FloorControllError[]>(environment.getApiUrl + '/floorControllErrors/getall')
  }
  getFloorErrorDtoList(): Observable<FloorErrorDto[]> {

    return this.httpClient.get<FloorErrorDto[]>(environment.getApiUrl + '/floorControllErrors/getalldto')
  }

  getFloorControllErrorById(id: number): Observable<FloorControllError> {
    return this.httpClient.get<FloorControllError>(environment.getApiUrl + '/floorControllErrors/getbyid?id='+id)
  }

  addFloorControllError(floorControllError: FloorControllError): Observable<any> {

    return this.httpClient.post(environment.getApiUrl + '/floorControllErrors/', floorControllError, { responseType: 'text' });
  }

  updateFloorControllError(floorControllError: FloorControllError): Observable<any> {
    return this.httpClient.put(environment.getApiUrl + '/floorControllErrors/', floorControllError, { responseType: 'text' });

  }

  deleteFloorControllError(id: number,errorName:string) {
    return this.httpClient.request('delete', environment.getApiUrl + '/floorControllErrors/', { body: { id: id,errorName:errorName } });
  }


}