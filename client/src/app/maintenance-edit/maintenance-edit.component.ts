import { Component, OnInit } from '@angular/core';
import { MaintenanceService } from '../_services/maintenance.service';
import { ActivatedRoute } from '@angular/router';
import { MaintenanceRequest } from '../_models/maintenanceRequestModel';

@Component({
  selector: 'app-maintenance-edit',
  imports: [],
  templateUrl: './maintenance-edit.component.html',
  styleUrl: './maintenance-edit.component.css'
})
export class MaintenanceEditComponent implements OnInit{

  constructor(private maintenanceService : MaintenanceService,private route : ActivatedRoute){}
    
  maintenanceRequest? : MaintenanceRequest;
  imgBaseUrl = "https://localhost:5001/";

  ngOnInit(){
    this.loadMaintenance();
  }

  loadMaintenance(){
    const idParam = this.route.snapshot.paramMap.get("id");
    if(!idParam) return;

    const id: number = parseInt(idParam, 10);

    if (isNaN(id))return;

    this.maintenanceService.getMaintenanceRequestById(id).subscribe({
      next: res => this.maintenanceRequest = res,
      error: error => console.log(error)
    });

  }

}
