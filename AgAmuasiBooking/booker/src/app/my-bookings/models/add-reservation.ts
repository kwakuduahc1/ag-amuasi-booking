export interface AddBookingDto {
    title: string;
    purpose?: string;
    dates: Date[];
    services: number[];
    guests: number;
    bookingServicesID?: number[];
}
