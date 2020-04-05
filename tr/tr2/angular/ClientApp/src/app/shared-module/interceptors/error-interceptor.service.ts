import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpEventType, HttpErrorResponse, HttpResponse } from "@angular/common/http";
import { Observable, throwError } from "rxjs";
import { catchError, tap, filter } from "rxjs/operators";
import { IAppState } from "../reducers/shared-reducer-selector";
import { Router } from "@angular/router";
import { Store } from "@ngrx/store";
import { RemoteCallAction, ERROR } from "../reducers/spiner-reducer/spiner-actions";

export class ErrorInterceptorService implements HttpInterceptor
{
  constructor(private store:Store<IAppState>, private router: Router) { }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(req);
  }
}
