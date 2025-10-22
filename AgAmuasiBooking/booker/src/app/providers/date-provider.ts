
import { Injectable } from '@angular/core';

@Injectable({ providedIn: 'root' })
export class DateProviderService {

    today = new Date();
    getLastWeekday(): Date {
        const dayOfWeek = this.today.getDay(); // 0 = Sunday, 1 = Monday, ..., 6 = Saturday

        // If today is Sunday (0) or Saturday (6), adjust to the last Friday
        if (dayOfWeek === 0) { // Sunday
            const lastFriday = new Date(this.today);
            lastFriday.setDate(this.today.getDate() - 2); // Go back 2 days to Friday
            return lastFriday;
        } else if (dayOfWeek === 6) { // Saturday
            const lastFriday = new Date(this.today);
            lastFriday.setDate(this.today.getDate() - 1); // Go back 1 day to Friday
            return lastFriday;
        }

        // Otherwise, today is already a weekday
        return this.today;
    }

    weekendFilter(date: Date | null): boolean {
        if (!date) return false;
        const day = date.getDay();
        return day !== 0 && day !== 6;
    }
}
