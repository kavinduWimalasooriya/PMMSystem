import { Component, input } from '@angular/core';
import { MaintenanceRequest } from '../_models/maintenanceRequestModel';
import { RouterLink } from '@angular/router';
import { MaintenanceStatusPipe } from '../_pipes/maintenance-status.pipe';

@Component({
  selector: 'app-maintenance-card',
  imports: [RouterLink,MaintenanceStatusPipe],
  templateUrl: './maintenance-card.component.html',
  styleUrl: './maintenance-card.component.css'
})
export class MaintenanceCardComponent {

  maintenanceRequest = input.required<MaintenanceRequest>();
  imgBaseUrl = "https://localhost:5001/";
}
