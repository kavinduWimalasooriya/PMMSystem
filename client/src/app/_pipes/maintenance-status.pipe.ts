import { Pipe, PipeTransform } from '@angular/core';
import { MaintenanceStatus } from '../_enums/maintenanceStatus';

@Pipe({
  name: 'maintenanceStatus'
})
export class MaintenanceStatusPipe implements PipeTransform {

  transform(statusCode: number): string {
    switch (statusCode) {
      case MaintenanceStatus.New:
        return 'New';
      case MaintenanceStatus.Accepted:
        return 'Accepted';
      case MaintenanceStatus.Rejected:
        return 'Rejected';
      default:
        return 'Unknown Status';
    }
  }

}
