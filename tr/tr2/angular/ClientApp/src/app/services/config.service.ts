import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { APP_SETTINGS } from '../shared-module/models/APP_SETTINGS';
import { HttpHelperService } from '../shared-module/services/http-helper';

@Injectable({
  providedIn: 'root'
})
export class ConfigService {

  constructor(private http: HttpClient, private httpHelper: HttpHelperService) { }

  getDropDownLists(dropDownListIdentifiers: DropDownListIdentifier[]): Observable<any>{
    return this.http.get<any>(APP_SETTINGS.baseUrl + 'config/dropdownlists', { params: this.httpHelper.toHttpParams(dropDownListIdentifiers) });
  }
}

export enum DropDownListIdentifier {
  Employees = 0,
  Suppliers = 1,
  Categories = 2,
  Customers = 3,
}
