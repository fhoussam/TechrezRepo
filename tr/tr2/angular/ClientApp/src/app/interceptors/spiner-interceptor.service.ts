import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEventType } from '@angular/common/http';
import { tap } from 'rxjs/operators';
import { IAppState } from '../shared-module/remote-call-reducer/remote-call-reducer';
import { Store } from '@ngrx/store';
import { RemoteCallAction, PENDING, SUCCESS } from '../shared-module/remote-call-reducer/remote-call-actions';

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

  constructor(private store: Store<IAppState>, ) {
    this.loadingMessages.push(
      new UrlForSpinner('api/products', 'GET'),
      new UrlForSpinner('api/products', 'POST'),
    );
  }

  intercept(req: HttpRequest<any>, next: HttpHandler) {

    let loadingMessage = this.loadingMessages.find(x => x.method === req.method && x.value === req.url);

    if (loadingMessage) {
      this.store.dispatch(new RemoteCallAction({
        messageType: PENDING,
        messageValue: "Loading, please wait ...",
      }));
    }

    return next.handle(req).pipe(tap(event => {
      if (event.type === HttpEventType.Response && loadingMessage) {
        this.store.dispatch(new RemoteCallAction({
          messageType: SUCCESS,
          messageValue: null,
        }));

      }
    }));
  }
}
