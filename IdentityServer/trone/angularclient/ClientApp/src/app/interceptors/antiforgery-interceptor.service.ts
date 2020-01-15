import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { CookieService } from 'ngx-cookie-service';
import { catchError, finalize } from 'rxjs/operators';
import { Router } from '@angular/router';

@Injectable({
    providedIn: 'root'
})
export class AntiforgeryInterceptorService implements HttpInterceptor {
    constructor(
        private cookie: CookieService,
        private router: Router,
    ) { }

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

        let cookieValue = this.cookie.get('XSRF-REQUEST-TOKEN');

        const cloneReq = req.clone({
            headers: new HttpHeaders({
                'X-XSRF-TOKEN': cookieValue
            })
        });

        const started = Date.now();
        let ok: string;

        return next.handle(cloneReq).pipe(
            catchError((error: HttpErrorResponse) => {
                if (error.status == 404)
                    this.router.navigateByUrl('/notfound');
                return throwError(error);
            }),
            finalize(() => {
                const elapsed = Date.now() - started;
                const msg = `${req.method} "${req.urlWithParams}" ${ok} in ${elapsed} ms.`;
                console.log(msg);
            })
        );
    }
}
