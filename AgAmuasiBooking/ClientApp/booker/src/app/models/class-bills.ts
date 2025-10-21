/**
 * Interface for adding a new class bill
 */
export interface AddClassBill {
    /**
     * ID of the class
     * Required field
     */
    classesID: number;

    /**
     * ID of the payment item
     * Required field
     */
    billItemsID: number;

    /**
     * Amount of the bill
     * Required field, must be greater than or equal to 0
     */
    amount: number;

    semester: number;
}

export interface ClassBillDto extends AddClassBill {
    className: string;
    item: string;
}
/**
 * Interface for editing an existing class bill
 */
export interface EditClassBill {
    /**
     * ID of the class bill item
     * Required field
     */
    classBillItemsID: number;

    /**
     * ID of the class bill
     * Required field
     */
    classBillsID: number;

    /**
     * ID of the class
     * Required field
     */
    classesID: number;

    /**
     * ID of the payment item
     * Required field
     */
    paymentItemsID: number;

    /**
     * Amount of the bill
     * Required field, must be greater than or equal to 0
     */
    amount: number;

    /**
     * Semester number
     * Required field
     */
    semester: number;
}

/**
 * Interface for listing class bills
 */
export interface ClassBillGroup {
    classBillsID: number;
    semester: number;
    billDescription: string;
    bills: BillItem[];
    classesID: number;
}

export interface ClassBills {
    classBillsID: number;
    billDescription: string;
    amount: number;
}

export interface BillItem {
    paymentItem: string;
    amount: number;
    classBillItemsID: number;
    paymentItemsID: number;
    classBillsID: number;
}
