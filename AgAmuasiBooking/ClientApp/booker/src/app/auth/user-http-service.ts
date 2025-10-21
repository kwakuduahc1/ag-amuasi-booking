import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { IUserRoles, RegisterVm, URoles } from '../models/IUsers';
import { environment } from '../environments/environment';


@Injectable({ providedIn: 'root' })
export class UserProjectsHttpService {
  http = inject(HttpClient);

  list(id: string): Observable<IUserRoles[]> {
    return this.http.get<IUserRoles[]>(environment.AppUrl + `UserProjects/${id}`)
  }

  add(role: URoles): Observable<IUserRoles> {
    return this.http.post<IUserRoles>(environment.AppUrl + `UserProjects`, role);
  }

  unRole(uid: string, role: string): Observable<IUserRoles> {
    return this.http.delete<IUserRoles>(environment.AppUrl + `UserProjects/${uid}/${role}`);
  }

  register(user: RegisterVm): Observable<RegisterVm> {
    return this.http.post<RegisterVm>(environment.AppUrl + `Auth/Register`, user);
  }
}
