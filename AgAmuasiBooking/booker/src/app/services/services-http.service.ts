import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../environments/environment';
import {
  AddServiceDto,
  AddServiceResponse,
  ServiceCostDto,
  ServicesDto,
} from './models/services.dto';

@Injectable({
  providedIn: 'root'
})
export class ServicesHttpService {
  private http = inject(HttpClient);
  private readonly apiUrl = `${environment.AppUrl}Services`;

  /**
   * Get all services with their cost history (up to 5 most recent costs per service)
   * GET /api/Services
   * @returns Observable of service list with grouped costs
   */
  getServices(): Observable<ServicesDto[]> {
    return this.http.get<ServicesDto[]>(this.apiUrl);
  }

  /**
   * Add a new service or update an existing service
   * POST /api/Services
   * 
   * If servicesID > 0: Updates existing service name, returns servicesID
   * If servicesID = 0: Creates new service with initial cost, returns { id, sid }
   * 
   * @param service - The service data to add or update
   * @returns Observable of either servicesID (number) for update, or AddServiceResponse for create
   */
  addService(service: AddServiceDto): Observable<{ id: number, sid: number }> {
    return this.http.post<{ id: number, sid: number }>(this.apiUrl, service);
  }

  /**
   * Update an existing service name
   * Convenience method that sets servicesID and calls addService
   * 
   * @param servicesID - ID of the service to update
   * @param serviceName - New service name
   * @returns Observable of servicesID
   */
  updateService(servicesID: number, serviceName: string): Observable<number> {
    const dto: AddServiceDto = {
      servicesID,
      serviceName,
      cost: 0, // Cost is ignored for updates
      perPerson: false // PerPerson is ignored for updates
    };
    return this.http.post<number>(this.apiUrl, dto);
  }

  /**
   * Create a new service with initial cost
   * Convenience method that sets servicesID to 0 and calls addService
   * 
   * @param serviceName - Name of the new service
   * @param cost - Initial cost for the service
   * @param perPerson - Whether the cost is per person
   * @returns Observable of AddServiceResponse with new IDs
   */
  createService(serviceName: string, cost: number, perPerson: boolean): Observable<AddServiceResponse> {
    const dto: AddServiceDto = {
      servicesID: 0,
      serviceName,
      cost,
      perPerson
    };
    return this.http.post<AddServiceResponse>(this.apiUrl, dto);
  }

  removeService(servicesID: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${servicesID}`);
  }

  addCost(serv: ServiceCostDto): Observable<number> {
    return this.http.post<number>(`${this.apiUrl}/Cost`, serv);
  }
}
