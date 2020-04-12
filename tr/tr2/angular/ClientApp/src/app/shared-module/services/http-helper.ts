import { Injectable } from "@angular/core";
import { HttpParams } from "@angular/common/http";

@Injectable()
export class HttpHelperService
{
  public toHttpParams(object): HttpParams {
    let params = new HttpParams()
    Object.keys(object).forEach(function (item) {
      if (object[item])
        params = params.set(item, object[item]);
    });

    return params
  }
}
