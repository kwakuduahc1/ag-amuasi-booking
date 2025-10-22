/**
 * DTOs for Services API
 * Maps to C# DTOs from ServicesController
 */

/**
 * Service DTO with associated cost information
 * Maps to: ServicesDto record in C#
 */
export interface ServicesDto {
  servicesID: number;
  serviceName: string;
  cost: number;
  serviceCostsID: number;
}

/**
 * DTO for adding or updating a service
 * Maps to: AddServiceDto record in C#
 * If servicesID > 0: Updates existing service name
 * If servicesID = 0: Creates new service with initial cost
 */
export interface AddServiceDto {
  servicesID: number;
  serviceName: string;
  cost: number;
  perPerson: boolean;
}

/**
 * Service cost information
 * Maps to: ServiceCostDto record in C#
 */
export interface ServiceCostDto {
  id: number;
  cost: number;
}

/**
 * Response from GET /api/Services
 * Represents grouped services with their cost history
 */
export interface ServiceListResponse {
  servicesID: number;
  serviceName: string;
  costs: ServiceCost[];
}

/**
 * Individual service cost in the list response
 */
export interface ServiceCost {
  serviceCostsID: number;
  cost: number;
}

/**
 * Response from POST /api/Services (create new service)
 */
export interface AddServiceResponse {
  id: number;        // servicesID
  sid: number;       // serviceCostsID
}
