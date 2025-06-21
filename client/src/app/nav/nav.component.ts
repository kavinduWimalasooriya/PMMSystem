import { Component, inject, OnInit } from '@angular/core';
import { RoleManagemantService } from '../_services/role-managemant.service';
import { NgToggleComponent } from 'ng-toggle-button';
import { Role } from '../_enums/roles';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-nav',
  imports: [NgToggleComponent,RouterLink],
  templateUrl: './nav.component.html',
  styleUrl: './nav.component.css'
})
export class NavComponent implements OnInit {
  constructor(){}
  roleService = inject(RoleManagemantService);
  isAdmin = false;

  ngOnInit(): void {
    this.isAdmin = this.roleService.currentRole() == Role.Admin;
  }


  loadCorrectRole(){
    this.isAdmin = !this.isAdmin;
    if(this.isAdmin){
      this.roleService.setRole(Role.Admin);
    }
    else{
      this.roleService.setRole(Role.PropertyManager);
    }
      
  }
  
}
