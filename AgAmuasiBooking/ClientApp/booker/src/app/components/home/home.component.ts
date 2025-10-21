import { Component, inject } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card'
import { StatusProvider } from '../../providers/StatusProvider';

@Component({
    selector: 'app-home',
    templateUrl: './home.component.html',
    styleUrls: ['./home.component.scss'],
    imports: [
        MatCardModule,
        MatIconModule,
        MatButtonModule,

    ]
})
export class HomeComponent {
    readonly status = inject(StatusProvider);
}
