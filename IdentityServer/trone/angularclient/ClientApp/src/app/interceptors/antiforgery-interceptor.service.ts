import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { CookieService } from 'ngx-cookie-service';

@Injectable({
    providedIn: 'root'
})
export class AntiforgeryInterceptorService implements HttpInterceptor {
    constructor(private cookie: CookieService) { }

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

        let cookieValue = this.cookie.get('XSRF-REQUEST-TOKEN');

        const cloneReq = req.clone({
            headers: new HttpHeaders({
                'X-XSRF-TOKEN': cookieValue
            })
        });

        return next.handle(cloneReq);
    }
}
