import { Component, inject } from '@angular/core';
import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';
import { Observable } from 'rxjs';
import { map, shareReplay } from 'rxjs/operators';
import { StatusProvider } from '../../providers/StatusProvider';
import { RouterLink, RouterOutlet } from '@angular/router';
import { MatIconModule } from '@angular/material/icon';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatListModule } from '@angular/material/list';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatMenuModule } from '@angular/material/menu';
import { environment } from '../../environments/environment';

@Component({
    selector: 'app-nav',
    templateUrl: './navbar.component.html',
    styleUrls: ['./navbar.component.scss'],
    imports: [
        CommonModule,
        MatMenuModule,
        RouterOutlet,
        RouterLink,
        MatIconModule,
        MatSidenavModule,
        MatToolbarModule,
        MatListModule,
        MatButtonModule
    ],
    standalone: true
})
export class NavBarComponent {
    links: Array<{ link: string, display: string, icon: string }> = [
        { icon: 'library_books', link: 'records/list', display: 'Records' },
        // { icon: 'account_circle', link: 'opd', display: 'OPD' },
        { icon: 'attach_money', link: 'accounts/search', display: 'Accounts' },
        { icon: 'record_voice_over', link: 'consult', display: 'Consulting' },
        { icon: 'biotech', link: 'labs/home', display: 'Laboratory' },
        { icon: 'pending_actions', link: 'dispensary/list', display: 'Dispensary' },
        { icon: 'medical_services', link: 'services/list', display: 'Services' },
        { icon: 'admin_panel_settings', link: 'administration/home', display: 'Administration' },
        { icon: 'shopping_cart', link: 'stores/items', display: 'Stores' },
        // { icon: 'money', link: 'expenditure', display: 'Expenditure' },
        { icon: 'business', link: 'management', display: 'Management' }
    ];
    bpo = inject(BreakpointObserver);
    env = environment;
    iconName = 'api';
    isHandset$: Observable<boolean> = this.bpo.observe(Breakpoints.Handset)
        .pipe(
            map(result => result.matches),
            shareReplay()
        );

    title = this.env.AppName;
    status = inject(StatusProvider);
    snack = inject(MatSnackBar);

    constructor(private breakpointObserver: BreakpointObserver) {
    }

    logout() {
        this.status.logout();
    }

    toggleMenu() { }
}
