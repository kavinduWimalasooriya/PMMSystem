export interface MaintenanceRequest {
    id:number;
    maintenanceEventName: string;
    propertyName:string;
    description:string;
    status:number;
    imageUrl:string;
}