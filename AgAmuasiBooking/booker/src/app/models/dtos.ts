// DTOs for bank accounts
export interface BankAccountsAddDto {
  bankName: string;
  accountName: string;
  denomination: string;
  branch: string;
  description: string;
}

export interface BankAccountsEditDto {
  bankAccountsID: number;
  bankName: string;
  accountName: string;
  denomination: string;
  branch: string;
  description: string;
}

export interface BankDetailsDto {
  bankName: string;
  accountName: string;
  denomination: string;
  branch: string;
  bankAccountsID: number;
  deposits: number;
  description: string;
}

// DTOs for deposits
export interface DepositsAddDto {
  bankAccountsID: number;
  amount: number;
  receipt: string;
  depositor: string;
  description: string;
  depositDate: Date;
}

export interface DepositsEditDto {
  depositsID: number;
  bankAccountsID: number;
  amount: number;
  receipt: string;
  depositor: string;
  description: string;
  depositDate: Date;
}

// DTOs for memos
export interface AddMemoDto {
  title: string;
  description: string;
  amount: number;
  memoDate: Date;
  departmentID: number;
  memoOwner: string;
}

export interface UnapprovedMemoDto {
  memosID: number;
  title: string;
  description: string;
  amount: number;
  memoDate: Date;
  raiser: string;
  department: string;
  memoOwner: string;
}

export interface UnPaidDto {
  memosID: number;
  title: string;
  description: string;
  amount: number;
  approver: string;
  dateApproved: Date;
  department: string;
  memoOwner: string;
  memoDate: Date;
  selected: boolean;
}

// DTO for withdrawals
export interface AddWithdrawalDto {
  bankAccountsID: number;
  amount: number;
  chequeID: string;
  memos: number[];
  description: string;
}

export interface DepositsDto {
  dateAdded: Date;
  depositsID: number;
  depositDate: Date;
  depositor: string;
  description: string;
  amount: number;
  bankAccountsID: number;
  accountName: string;
  bankName: string;
}

/**
 * public record StudentPaymentsDto(int PaymentsID, int StudentsID, decimal Amount, string DepositReference, string BankName, DateTime DatePaid, short Semester, string FullName, string IndexNumber);
 */

export interface StudentPaymentsDto {
  paymentsID: number;
  studentsID: number;
  amount: number;
  depositReference: string;
  bankName: string;
  datePaid: Date;
  semester: number;
  fullName: string;
  indexNumber: string;
}
