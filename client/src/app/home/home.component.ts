import { Component, OnInit } from '@angular/core';
import { MaintenanceService } from '../_services/maintenance.service';
import { MaintenanceRequest } from '../_models/maintenanceRequestModel';
import { MaintenanceCardComponent } from "../maintenance-card/maintenance-card.component";
import { RouterLink } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { MaintenanceStatus } from '../_enums/maintenanceStatus';

@Component({
  selector: 'app-home',
  imports: [MaintenanceCardComponent, RouterLink, FormsModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent implements OnInit {

  constructor(private maintenanceService: MaintenanceService) { }

  maintenanceReq: MaintenanceRequest[] = [];

  searchTerm?: string;
  status?: string = "all";
  validMaintenanceStatusStrings = Object.values(MaintenanceStatus)
      .filter(value => typeof value === 'string') as string[];

  ngOnInit(): void {
    this.getAllMaintenanceCards();
  }

  getAllMaintenanceCards() {
    let statusToSend: string | undefined = this.status;

    if (statusToSend && statusToSend !== "" && !this.validMaintenanceStatusStrings.includes(statusToSend)) {
      statusToSend = "";
    }
    this.maintenanceService.getMaitenanceRequests(this.searchTerm, statusToSend).subscribe({
      next: res => this.maintenanceReq = res,
      error: error => console.log(error)
    });
  }

  resetSearchForm() {
    this.searchTerm = "",
    this.status = "all"
    this.getAllMaintenanceCards();
  }

}
