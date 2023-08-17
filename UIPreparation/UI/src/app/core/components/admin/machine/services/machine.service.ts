import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Machine } from '../models/Machine';
import { environment } from 'environments/environment';


@Injectable({
  providedIn: 'root'
})
export class MachineService {

  constructor(private httpClient: HttpClient) { }


  getMachineList(): Observable<Machine[]> {

    return this.httpClient.get<Machine[]>(environment.getApiUrl + '/machines/getall')
  }

  getMachineById(id: number): Observable<Machine> {
    return this.httpClient.get<Machine>(environment.getApiUrl + '/machines/getbyid?id='+id)
  }

  addMachine(machine: Machine): Observable<any> {

    return this.httpClient.post(environment.getApiUrl + '/machines/', machine, { responseType: 'text' });
  }

  updateMachine(machine: Machine): Observable<any> {
    return this.httpClient.put(environment.getApiUrl + '/machines/', machine, { responseType: 'text' });

  }

  deleteMachine(id: number) {
    return this.httpClient.request('delete', environment.getApiUrl + '/machines/', { body: { id: id } });
  }


}