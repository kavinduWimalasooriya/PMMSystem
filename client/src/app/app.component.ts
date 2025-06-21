import { Component, inject, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NavComponent } from "./nav/nav.component";
import { RoleManagemantService } from './_services/role-managemant.service';
import { Role } from './_enums/roles';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, NavComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {
  title = 'client';
  roleService = inject(RoleManagemantService)
  ngOnInit(): void {
    this.roleService.setRole(Role.PropertyManager);
  }
  
}
