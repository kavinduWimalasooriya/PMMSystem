import { Component, OnInit } from '@angular/core';
import { MaintenanceService } from '../_services/maintenance.service';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { MaintenanceRequest } from '../_models/maintenanceRequestModel';
import { FormsModule } from '@angular/forms';
import { MaintenanceStatus } from '../_enums/maintenanceStatus';
import { RoleManagemantService } from '../_services/role-managemant.service';
import { ToastrService } from 'ngx-toastr';
import { Role } from '../_enums/roles';

@Component({
  selector: 'app-maintenance-edit',
  imports: [FormsModule,RouterLink],
  templateUrl: './maintenance-edit.component.html',
  styleUrl: './maintenance-edit.component.css'
})
export class MaintenanceEditComponent implements OnInit {

  constructor(private maintenanceService: MaintenanceService, 
    private route: ActivatedRoute,
    public roleService : RoleManagemantService,
    private toastr : ToastrService
  ) { }

  maintenanceRequest?: MaintenanceRequest;
  imgBaseUrl = "https://localhost:5001/";
  selectedFile: File | null = null;
  enum = MaintenanceStatus

  ngOnInit() {
    this.loadMaintenance();
  }

  onfileSelected(event: Event) {
    const input = event.target as HTMLInputElement;
    if (input.files && input.files.length > 0) {
      this.selectedFile = input.files[0];
    } else {
      this.selectedFile = null;
    }
  }
  onSubmit() {
    if (!this.maintenanceRequest) {
      this.toastr.error('Maintenance request is not loaded.');
      return;
    }


    const formData = new FormData();
    formData.append('Id', this.maintenanceRequest.id.toString());
    formData.append('MaintenanceEventName', this.maintenanceRequest.maintenanceEventName);
    formData.append('PropertyName', this.maintenanceRequest.propertyName);
    formData.append('Description', this.maintenanceRequest.description);
    formData.append('Status', this.maintenanceRequest.status.toString());
    let roleValueToSend: number;
    if (this.roleService.currentRole() == Role.Admin) {
        roleValueToSend = 0; 
    } else  {
        roleValueToSend = 1; 
    } 
    formData.append('Role', roleValueToSend.toString());

    if (this.selectedFile) {
      formData.append('Image', this.selectedFile, this.selectedFile.name);
      console.log("file is selected  ", this.selectedFile.name)
    }
    this.updateMaintenance(formData)

  }

  updateMaintenance(data: FormData) {
    this.maintenanceService.updateMaintenanceRequest(data).subscribe({
      next: res => {
        window.location.reload();
      },
      error: error => console.log(error)
    });
  }

  loadMaintenance() {
    const idParam = this.route.snapshot.paramMap.get("id");
    if (!idParam) return;

    const id: number = parseInt(idParam, 10);

    if (isNaN(id)) return;

    this.maintenanceService.getMaintenanceRequestById(id).subscribe({
      next: res => this.maintenanceRequest = res,
      error: error => console.log(error)
    });
  }

}
