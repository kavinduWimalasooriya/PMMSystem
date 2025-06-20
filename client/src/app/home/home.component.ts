import { Component, OnInit } from '@angular/core';
import { MaintenanceService } from '../_services/maintenance.service';
import { MaintenanceRequest } from '../_models/maintenanceRequestModel';
import { MaintenanceCardComponent } from "../maintenance-card/maintenance-card.component";
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-home',
  imports: [MaintenanceCardComponent,RouterLink],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent implements OnInit{

  constructor(private maintenanceService : MaintenanceService){}
  
  maintenanceReq : MaintenanceRequest[] = [];

  ngOnInit(): void {
    this.getAllMaintenanceCards();
  }

  getAllMaintenanceCards(){
    this.maintenanceService.getMaitenanceRequests().subscribe({
      next : res => this.maintenanceReq = res,
      error : error => console.log(error)
    });
  }

}
