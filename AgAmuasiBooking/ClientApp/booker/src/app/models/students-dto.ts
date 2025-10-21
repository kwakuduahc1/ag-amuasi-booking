/**
 * TypeScript equivalents of the C# StudentsDTOs
 * Generated from AccountingUltimate\Models\VirtualModels\StudentsDtos.cs
 */

/**
 * DTO for adding a new student
 */
export interface StudentAddDto {

  studentsID?: number;
  /**
   * The student's full name
   * Required field with maximum length of 75 characters
   */
  fullName: string;

  /**
   * The student's unique index/admission number
   * Required field used to uniquely identify students, must be between 3 and 20 characters
   */
  indexNumber: string;

  /**
   * The ID of the class to which the student belongs
   * Required field, must reference a valid ClassesID
   */
  classesID: number;

  sex: string;

  isBoarder: boolean;
}

/**
 * DTO for editing an existing student
 */
export interface StudentEditDto {
  /**
   * The unique identifier for the student record
   * Required field, must match an existing student ID
   */
  studentsID: number;

  /**
   * The student's full name
   * Required field with maximum length of 75 characters
   */
  fullName: string;

  /**
   * The student's unique index/admission number
   * Required field used to uniquely identify students, must be between 3 and 20 characters
   */
  indexNumber: string;

  /**
   * The ID of the class to which the student belongs
   * Required field, must reference a valid ClassesID
   */
  classesID: number;

  sex: string;

  isBoarder: boolean;
}

/**
 * DTO for listing students
 */
export interface StudentListDto {
  /**
   * The unique identifier for the student record
   */
  studentsID: number;

  /**
   * The student's full name
   */
  fullName: string;

  /**
   * The student's unique index/admission number
   */
  indexNumber: string;

  /**
   * The ID of the class to which the student belongs
   */
  classesID: number;

  /**
   * The name of the class to which the student belongs
   */
  className: string;

  sex: string;

  isBoarder: boolean;
}

/**
 * Response structure for student list grouped by class
 * Matches the JSON structure returned by the List() API endpoint
 */
export interface StudentListResponse {
  /**
   * The name of the class/grade
   */
  className: string;

  /**
   * List of students in this class
   */
  students: StudentListItem[];
}

/**
 * Student item in the grouped list response
 */
export interface StudentListItem {
  /**
   * The unique identifier for the student record
   */
  studentsID: number;

  /**
   * The student's full name
   */
  fullName: string;

  /**
   * The student's unique index/admission number
   */
  indexNumber: string;

  className: string;

  classesID: number;

  sex: string;

  isBoarder: boolean;
  classYearSemestersID: number
}

/**
 * DTO for adding a new class
 */
export interface ClassAddDto {
  /**
   * The name of the class/grade
   * Required field with length between 2 and 15 characters
   */
  className: string;
}

/**
 * DTO for editing an existing class
 */
export interface ClassEditDto {
  /**
   * The unique identifier for the class
   * Required field, must match an existing class ID
   */
  classesID: number;

  /**
   * The name of the class/grade
   * Required field with length between 2 and 15 characters
   */
  className: string;
}

/**
 * DTO for listing classes
 */
export interface ClassListDto {
  /**
   * The unique identifier for the class
   */
  classesID: number;

  /**
   * The name of the class/grade
   */
  className: string;

  students?: StudentListDto[];
}

export interface StdPayHistory {
  paymentDate: Date;
  accountName: string;
  bankName: string;
  depositReference: string;
  billDescription: string;
  amount: number
}

export interface ClassDetailDto {
  shortName: string;
  semester: number;
  className: string;
  classesID: number;
  bills: ClassBillItemDto[];
  classYearSemestersID: number;
}

export interface ClassBillItemDto {
  classSemesterBillItemsID: number;
  item: string;
  billItemsID: number;
  amount: number;
  disabled: boolean;
}

export interface AddClassSemesterBillItemDto {
  classesID: number;
  classYearSemestersID: number;
  billItemsID: number;
  amount: number;
  classSemesterBillItemsID?: number;
}

export interface StudentSemesterBillResponse {
  studentsID: number;
  fullName: string;
  indexNumber: string;
  classesID: number;
  className: string;
  semester: number;
  bills: BillItem[];
}

export interface BillItem {
  studentSemesterBillsID: number | null;
  classYearSemestersID: number | null;
  billItemsID: number | null;
  item: string;
  amount: number;
}
