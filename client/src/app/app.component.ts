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
    this.setCurrentUser();
  }

  setCurrentUser() {
    const userString = localStorage.getItem("userRole");
    if (!userString)
      this.roleService.setRole(Role.PropertyManager);
    else {
      this.roleService.currentRole.set(userString as Role);
    }
  }

}
