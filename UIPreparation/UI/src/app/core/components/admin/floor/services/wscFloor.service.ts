import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'environments/environment';
import { WashingControll_Floor } from '../models/wscFloor';


@Injectable({
  providedIn: 'root'
})
export class FloorService {

  constructor(private httpClient: HttpClient) { }


  getWashingControll_FloorList(): Observable<WashingControll_Floor[]> {

    return this.httpClient.get<WashingControll_Floor[]>(environment.getApiUrl + '/washingControll_Floors/getall')
  }

  getWashingControll_FloorById(id: number): Observable<WashingControll_Floor> {
    return this.httpClient.get<WashingControll_Floor>(environment.getApiUrl + '/washingControll_Floors/getbyid?id='+id)
  }

  addWashingControll_Floor(washingControll_Floor: WashingControll_Floor): Observable<any> {

    return this.httpClient.post(environment.getApiUrl + '/washingControll_Floors/', washingControll_Floor, { responseType: 'text' });
  }

  updateWashingControll_Floor(washingControll_Floor: WashingControll_Floor): Observable<any> {
    return this.httpClient.put(environment.getApiUrl + '/washingControll_Floors/', washingControll_Floor, { responseType: 'text' });

  }

  deleteWashingControll_Floor(id: number) {
    return this.httpClient.request('delete', environment.getApiUrl + '/washingControll_Floors/', { body: { id: id } });
  }


}