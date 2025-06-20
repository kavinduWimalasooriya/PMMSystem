import { Component, OnInit } from '@angular/core';
import { MaintenanceService } from '../_services/maintenance.service';
import { ActivatedRoute } from '@angular/router';
import { MaintenanceRequest } from '../_models/maintenanceRequestModel';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-maintenance-edit',
  imports: [FormsModule],
  templateUrl: './maintenance-edit.component.html',
  styleUrl: './maintenance-edit.component.css'
})
export class MaintenanceEditComponent implements OnInit{
onfileSelected($event: Event) {
throw new Error('Method not implemented.');
}
onSubmit() {
throw new Error('Method not implemented.');
}

  constructor(private maintenanceService : MaintenanceService,private route : ActivatedRoute){}
    
  maintenanceRequest? : MaintenanceRequest;
  imgBaseUrl = "https://localhost:5001/";

  ngOnInit(){
    this.loadMaintenance();
  }

  loadMaintenance(){
    const idParam = this.route.snapshot.paramMap.get("id");
    console.log(idParam)
    if(!idParam) return;

    const id: number = parseInt(idParam, 10);

    if (isNaN(id))return;

    console.log(id)

    this.maintenanceService.getMaintenanceRequestById(id).subscribe({
      next: res => this.maintenanceRequest = res,
      error: error => console.log(error)
    });

  }

}
