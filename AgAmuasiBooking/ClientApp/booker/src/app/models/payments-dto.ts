/**
 * TypeScript equivalents of the C# PaymentsDTOs
 * Generated from AccountingUltimate\Models\VirtualModels\PaymentsDtos.cs
 */

/**
 * DTO for adding a new payment
 */
export interface PaymentAddDto {
    /**
     * The ID of the payment item
     * Required field, must reference a valid PaymentItemsID
     */
    paymentItemsID: number | undefined | null;

    /**
     * The ID of the student making the payment
     * Required field, must reference a valid StudentsID
     */
    studentsID: number | undefined | null;

    /**
     * The ID of the bank account receiving the payment
     * Required field, must reference a valid BankAccountsID
     */
    bankAccountsID: number | undefined | null;

    /**
     * The amount being paid
     * Required field, must be greater than zero
     */
    amount: number | undefined | null;

    /**
     * The reference number for the deposit
     * Required field with length between 5 and 30 characters
     */
    depositReference: string | undefined | null;

    /**
     * The name of the person who received the payment
     * Required field with maximum length of 30 characters
     */
    receiver: string | undefined | null;

    /**
     * The date when the payment was made
     * Required field
     */
    paymentDate: Date | undefined | null;
}

/**
 * DTO for listing payments
 */
export interface PaymentListDto {
    /**
     * The unique identifier for the payment
     */
    paymentsID: number;

    /**
     * The ID of the student making the payment
     */
    indexNumber: string;

    /**
     * The name of the student making the payment
     */
    fullName: string;

    /**
     * The name of the bank account receiving the payment
     */
    bankName: string;

    /**
     * The amount being paid
     */
    amount: number;

    /**
     * The reference number for the deposit
     */
    depositReference: string;

    /**
     * The name of the person who received the payment
     */
    receiver: string;

    /**
     * The date when the payment was recorded in the system
     */
    datePaid: Date;
}
