import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpHeaders } from '@angular/common/http';
import { CookieService } from 'ngx-cookie-service';

@Injectable({
  providedIn: 'root'
})
export class HttpHeaderAppenderInterceptorService implements HttpInterceptor {

  constructor(private cookieService: CookieService) { }

  intercept(req: HttpRequest<any>, next: HttpHandler) {

    const headers = new HttpHeaders()
      .append('X-XSRF-TOKEN', this.cookieService.get('XSRF-REQUEST-TOKEN'))
      .append('content-type', 'application/json')
      ;

    const cloneReq = req.clone({ headers: headers });
    return next.handle(cloneReq);
  }
}
