import { Component, inject } from '@angular/core';
import { RoleManagemantService } from '../_services/role-managemant.service';

@Component({
  selector: 'app-nav',
  imports: [],
  templateUrl: './nav.component.html',
  styleUrl: './nav.component.css'
})
export class NavComponent {
  constructor(){}
  roleService = inject(RoleManagemantService);
  
}
