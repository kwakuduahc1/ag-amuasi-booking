export interface AddBookingDto {
    title: string;
    purpose?: string;
    dates: Date[];
    services: number[];
    guests: number;
    bookingServicesID?: number[];
}

export interface UserBookingDto {
    bookingsID: string;
    bookingDate: Date;
    title: string;
    purpose: string;
    dates: Date[];
    guests: number;
    isReviewed: boolean;
    isApproved: boolean;
    hasPaid: boolean;
    days: number;
    bookingServicesID: number;
    serviceCostsID: number;
    cost: number;
}
