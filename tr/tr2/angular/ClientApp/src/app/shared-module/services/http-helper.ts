import { Injectable } from "@angular/core";
import { HttpParams } from "@angular/common/http";
import { DatePipe } from "@angular/common";
import { APP_SETTINGS } from "../models/APP_SETTINGS";

@Injectable()
export class HttpHelperService
{
  constructor(private datepipe: DatePipe) { }

  public toHttpParams(object): HttpParams {
    let params = new HttpParams()
    Object.keys(object).forEach((item) => {
      if (object[item]) {
        //is date, we can use typeof operator for other keys if we want later
        if (object[item].getMonth) { 
          params = params.set(item, this.datepipe.transform(object[item], APP_SETTINGS.queryStringDateFormat));
        }
        else {
          params = params.set(item, object[item]);
        }
      }
        
    });

    return params
  }
}
