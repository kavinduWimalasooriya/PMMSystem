import { Injectable, signal } from '@angular/core';
import { Role } from '../_enums/roles';

@Injectable({
  providedIn: 'root'
})
export class RoleManagemantService {

  constructor() { }

  currentRole = signal<string | null>(null);

  setRole(role: Role) {
    localStorage.setItem("userRole", role);
    this.currentRole.set(role);
  }
}
