import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { MaintenanceRequest } from '../_models/maintenanceRequestModel';

@Injectable({
  providedIn: 'root'
})
export class MaintenanceService {

  constructor(private http :HttpClient) { }
  private baseUrl = "https://localhost:5001/api/";

  getMaitenanceRequests(){
    return this.http.get<MaintenanceRequest[]>(this.baseUrl+"MaintenanceRequests/maintenance-requests");
  }

  getMaintenanceRequestById(id:number){
    return this.http.get<MaintenanceRequest>(this.baseUrl+"MaintenanceRequests/maintenance-requests/"+id)
  }
}
