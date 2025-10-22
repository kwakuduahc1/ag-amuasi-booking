/**
 * DTO for adding a new department
 */
export interface DepartmentAddDto {
  // @Required
  // @MaxLength(100)
  department: string;
}

/**
 * DTO for editing an existing department
 */
export interface DepartmentEditDto {
  // @Required
  departmentsID: number;

  // @Required
  // @MaxLength(100)
  department: string;
}

/**
 * DTO for listing departments
 */
export interface DepartmentListDto {
  departmentsID: number;
  department: string;
  isActive: boolean;
}
