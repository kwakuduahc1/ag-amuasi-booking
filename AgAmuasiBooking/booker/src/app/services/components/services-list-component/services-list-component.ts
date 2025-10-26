import { Component, inject, model, signal } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatIcon } from '@angular/material/icon';
import { ActivityProvider } from '../../../providers/ActivityProvider';
import { StatusProvider } from '../../../providers/StatusProvider';
import { CommonModule } from '@angular/common';
import { ServicesDto, AddServiceDto, ServiceCostDto } from '../../models/services.dto';
import { ServicesAddComponent } from '../services-add-component/services-add-component';
import { ConfirmationComponent } from '../../../components/confirmation/confirmation.component';
import { filter, map, switchMap, tap } from 'rxjs';
import { ServicesHttpService } from '../../services-http.service';
import { log } from 'console';
import { MatCheckbox } from "@angular/material/checkbox";
import { ServicesCostsComponent } from '../services-costs-component/services-costs-component';

@Component({
  selector: 'app-services-list-component',
  imports: [
    CommonModule,
    MatIcon,
    MatCheckbox
  ],
  templateUrl: './services-list-component.html',
  styleUrl: './services-list-component.scss'
})
export class ServicesListComponent {
  protected services = model.required<ServicesDto[]>();
  private diag = inject(MatDialog);
  private snack = inject(MatSnackBar);
  private act = inject(ActivityProvider);
  private http = inject(ServicesHttpService);
  service = signal<ServicesDto | null>(null);
  status = inject(StatusProvider);



  addService(service: ServicesDto | null = null) {
    if (service)
      this.service.set(service)
    this.diag.open<ServicesAddComponent, {}, AddServiceDto | null>(ServicesAddComponent, {
      data: this.service(),
      width: '500px',
      disableClose: false
    })
      .afterClosed()
      .pipe(
        tap(p => {
          if (p)
            this.service.set({
              perPerson: p.perPerson,
              cost: p.cost,
              serviceName: p.serviceName,
              servicesID: this.service()?.servicesID || 0,
              costs: this.service()?.costs || []
            })
        }),
        filter(x => !!x),
        switchMap(x => this.diag.open<ConfirmationComponent, {}, boolean>(ConfirmationComponent, {
          data: 'Are you sure you want to add this service?'
        }).afterClosed()
          .pipe(
            filter(r => !!r),
            map(() => x)
          )),
        switchMap(x => this.http.addService({
          servicesID: this.service()?.servicesID || 0,
          serviceName: this.service()!.serviceName,
          cost: x!.cost,
          perPerson: x!.perPerson
        }))
      )
      .subscribe({
        next: resp => {
          this.snack.open('Service added successfully', 'Close');
          if (resp.sid < 1) {
            this.services.update(p =>
              p.map(o => o.servicesID === resp.id ? { ...o, serviceName: this.service()!.serviceName, perPerson: this.service()!.perPerson } : o)
            )
          }
          else {
            this.services.update(p => [{
              servicesID: resp.id,
              serviceName: this.service()!.serviceName,
              perPerson: this.service()?.perPerson || false,
              cost: this.service()!.cost,
              costs: [
                { serviceCostsID: resp.sid, cost: this.service()!.cost }
              ]
            }, ...p]);
          }
        },
        error: err => this.addService(service)
      });
  }

  removeService(serviceID: number) {
    this.diag.open<ConfirmationComponent, {}, boolean>(ConfirmationComponent, {
      data: 'Are you sure you want to delete this service?'
    }).afterClosed()
      .pipe(
        filter(r => !!r),
        switchMap(() => this.http.removeService(serviceID))
      )
      .subscribe({
        next: () => {
          this.snack.open('Service removed successfully', 'Close');
          this.services.update(p => p.filter(o => o.servicesID !== serviceID));
        },
        error: err => this.snack.open('Error removing service', 'Close')
      });
  }

  costs(serv: ServicesDto) {
    console.log('Opening costs for service:', serv);
    this.diag.open<ServicesCostsComponent, {}, ServiceCostDto>(ServicesCostsComponent, {
      data: serv
    })
      .afterClosed()
      .pipe()
      .subscribe()
  }
}
