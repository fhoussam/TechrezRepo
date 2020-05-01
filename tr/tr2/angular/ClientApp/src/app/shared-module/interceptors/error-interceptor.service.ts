import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpErrorResponse } from "@angular/common/http";
import { Observable, throwError, of } from "rxjs";
import { catchError, retry } from "rxjs/operators";
import { IAppState } from "../reducers/shared-reducer-selector";
import { Router } from "@angular/router";
import { Store } from "@ngrx/store";
import { ErrorAction } from "../reducers/spiner-reducer/spiner-actions";

export class ErrorInterceptorService implements HttpInterceptor {
  constructor(private store: Store<IAppState>, private router: Router) { }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(request)
      .pipe(
        //retry(1), //"We are even able to add the retry(1) function to our interceptor, so all http requests will be retried once before failing"
        catchError((error: HttpErrorResponse) => {
          let errorMessage = '';
          if (error.error instanceof ErrorEvent) {
            // client-side error
            errorMessage = `Error: ${error.error.message}`;
          } else {
            if (error.status) {
              // server-side error
              if (error.status === 404) {
                this.router.navigateByUrl('/not-found');
              }
              else {
                this.store.dispatch(new ErrorAction("An error has occured while processing your request, please try again or contact the administrator"));
              }
            }
          }

          //we return null here for two reasons,
          //this is the last interceptor in the pipline and because we are sure its a handeled 404 error
          return error.status === 404 ? of(null) : throwError(errorMessage);
        })
      )
  }
}
