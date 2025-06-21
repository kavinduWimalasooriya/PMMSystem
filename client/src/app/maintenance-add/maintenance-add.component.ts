import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { newMaintenanceRequest } from '../_models/newMaintenanceRequestData ';
import { MaintenanceService } from '../_services/maintenance.service';
import { Router, RouterLink } from '@angular/router';

@Component({
  selector: 'app-maintenance-add',
  imports: [FormsModule,RouterLink],
  templateUrl: './maintenance-add.component.html',
  styleUrl: './maintenance-add.component.css'
})
export class MaintenanceAddComponent {
  constructor(private maintenanceService : MaintenanceService,private router :Router){}
  newRequest: newMaintenanceRequest = {
    maintenanceEventName: "",
    propertyName: "",
    description: ""
  }

  selectedFile: File | null = null;
  onSubmit() {
    const formData = new FormData();
    formData.append('MaintenanceEventName', this.newRequest.maintenanceEventName);
    formData.append('PropertyName', this.newRequest.propertyName);
    formData.append('Description', this.newRequest.description);

    if (this.selectedFile) {
      formData.append('Image', this.selectedFile, this.selectedFile.name);
      console.log("file is selected  ", this.selectedFile.name)
    }
    this.createRequest(formData);
  }

  createRequest(data : FormData){
    this.maintenanceService.createMaintenanceRequest(data).subscribe({
      next: () => {
        console.log('Maintenance request created successfully!');
        this.resetForm();
        this.router.navigateByUrl("/");
      },
      error: (error) => {
        console.error('Error creating maintenance request:', error);
      }
    });
  }

  onfileSelected(event: Event) {
    const input = event.target as HTMLInputElement;
    if (input.files && input.files.length > 0) {
      this.selectedFile = input.files[0];
    } else {
      this.selectedFile = null;
    }
  }

  resetForm(): void {
    this.newRequest = {
      maintenanceEventName: "",
      propertyName: "",
      description: ""
    };
    this.selectedFile = null;
  }

}
