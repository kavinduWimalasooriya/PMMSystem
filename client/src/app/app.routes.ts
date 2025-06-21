import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { MaintenanceEditComponent } from './maintenance-edit/maintenance-edit.component';
import { MaintenanceAddComponent } from './maintenance-add/maintenance-add.component';
import { ServerErrorComponent } from './server-error/server-error.component';
import { NotFoundComponent } from './not-found/not-found.component';

export const routes: Routes = [
    {path:"",component : HomeComponent},
    {path:"maintenance/edit/:id",component:MaintenanceEditComponent},
    {path:"maintenance/add",component:MaintenanceAddComponent},
    {path:"server-error", component : ServerErrorComponent},
    {path:"not-found", component : NotFoundComponent},
    {path:"**", component : HomeComponent , pathMatch : "full"},
];
