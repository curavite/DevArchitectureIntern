import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'environments/environment';
import { WashingControll_FloorControll } from '../models/washingcontroll_floorcontroll';


@Injectable({
  providedIn: 'root'
})
export class WashingControll_FloorControllService {

  constructor(private httpClient: HttpClient) { }


  getWashingControll_FloorControllList(): Observable<WashingControll_FloorControll[]> {

    return this.httpClient.get<WashingControll_FloorControll[]>(environment.getApiUrl + '/washingControll_FloorControlls/getall')
  }

  getWashingControll_FloorControllById(id: number): Observable<WashingControll_FloorControll> {
    return this.httpClient.get<WashingControll_FloorControll>(environment.getApiUrl + '/washingControll_FloorControlls/getbyid?id='+id)
  }

  addWashingControll_FloorControll(washingControll_FloorControll: WashingControll_FloorControll): Observable<any> {

    return this.httpClient.post(environment.getApiUrl + '/washingControll_FloorControlls/', washingControll_FloorControll, { responseType: 'text' });
  }



  updateWashingControll_FloorControll(washingControll_FloorControll: WashingControll_FloorControll): Observable<any> {
    return this.httpClient.put(environment.getApiUrl + '/washingControll_FloorControlls/', washingControll_FloorControll, { responseType: 'text' });

  }

  deleteWashingControll_FloorControll(id: number) {
    return this.httpClient.request('delete', environment.getApiUrl + '/washingControll_FloorControlls/', { body: { id: id } });
  }






}