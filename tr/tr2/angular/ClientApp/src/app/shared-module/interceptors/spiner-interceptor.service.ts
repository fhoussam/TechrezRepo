import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEventType } from '@angular/common/http';
import { tap } from 'rxjs/operators';
import { Store } from '@ngrx/store';
import { CookieService } from 'ngx-cookie-service';
import { PendingAction, SuccessAction } from '../reducers/spiner-reducer/spiner-actions';
import { IAppState } from '../reducers/shared-reducer-selector';

class UrlForSpinner {
  value: string;
  method: string;

  constructor(value: string, method: string) {
    this.value = value;
    this.method = method;
  }
}

@Injectable({
  providedIn: 'root'
})
export class SpinerInterceptorService implements HttpInterceptor {

  loadingMessages: UrlForSpinner[] = [];

  constructor(private store: Store<IAppState>, private cookieService: CookieService) {
    this.loadingMessages.push(
      new UrlForSpinner('api/products', 'GET'),
      new UrlForSpinner('api/products', 'POST'),
    );
  }

  intercept(req: HttpRequest<any>, next: HttpHandler) {

    let loadingMessage = this.loadingMessages.find(x => x.method === req.method && x.value === req.url);

    if (loadingMessage) {
      this.store.dispatch(new PendingAction((req.method === "GET" ? "Loading" : "Saving") + ", please wait ..."));
    }

    return next.handle(req).pipe(tap(event => {
      if (event.type === HttpEventType.Response && loadingMessage) {
        this.store.dispatch(new SuccessAction());
      }
    }));
  }
}
