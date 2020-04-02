import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpHeaders } from '@angular/common/http';
import { CookieService } from 'ngx-cookie-service';

@Injectable({
  providedIn: 'root'
})
export class AntiForgeryInterceptorService implements HttpInterceptor {

  constructor(private cookieService: CookieService) { }

  intercept(req: HttpRequest<any>, next: HttpHandler) {

    let antiForgeryCookieValue = this.cookieService.get('XSRF-REQUEST-TOKEN');
    const cloneReq = req.clone({
      headers: new HttpHeaders({ 'X-XSRF-TOKEN': antiForgeryCookieValue })
    });

    return next.handle(cloneReq);
  }
}
