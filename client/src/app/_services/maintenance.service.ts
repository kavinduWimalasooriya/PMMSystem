import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { MaintenanceRequest } from '../_models/maintenanceRequestModel';

@Injectable({
  providedIn: 'root'
})
export class MaintenanceService {

  constructor(private http :HttpClient) { }
  private baseUrl = "https://localhost:5001/api/";

  getMaitenanceRequests(search? : string,status?: string){
    let params = new HttpParams();
    if(search)
      params = params.set("search",search);
    if(status)
      params = params.set("status",status);
    console.log(params)
    return this.http.get<MaintenanceRequest[]>(this.baseUrl+"MaintenanceRequests/maintenance-requests",{params});
  }

  getMaintenanceRequestById(id:number){
    return this.http.get<MaintenanceRequest>(this.baseUrl+"MaintenanceRequests/maintenance-requests/"+id)
  }

  createMaintenanceRequest(formData: FormData){
    return this.http.post(this.baseUrl + "MaintenanceRequests", formData);
  }

  updateMaintenanceRequest(formData: FormData){
    return this.http.put(this.baseUrl + "MaintenanceRequests", formData);
  }
}
