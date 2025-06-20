import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { MaintenanceEditComponent } from './maintenance-edit/maintenance-edit.component';
import { MaintenanceAddComponent } from './maintenance-add/maintenance-add.component';

export const routes: Routes = [
    {path:"",component : HomeComponent},
    {path:"maintenance/edit/:id",component:MaintenanceEditComponent},
    {path:"maintenance/add",component:MaintenanceAddComponent}
];
