import { Component, ChangeDetectorRef, AfterViewChecked } from '@angular/core';
import { Store } from '@ngrx/store';
import { IAppState } from './shared-module/reducers/shared-reducer-selector';
import { PENDING, ALERT, ERROR, CONFIRM, ConfirmAction } from './shared-module/reducers/spiner-reducer/spiner-actions';
import { RemoteCallStatus } from './shared-module/reducers/spiner-reducer/spiner-reducer';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements AfterViewChecked {

  title = 'app';

  isPending: boolean;
  isAlert: boolean;
  isError: boolean;
  isConfirm: boolean;
  message: string;
  yesAsyncCallback: Observable<void>;

  constructor(
    private store: Store<IAppState>,
    private cdRef: ChangeDetectorRef,
  ) { }

  //still no idea how code below works
  ngAfterViewChecked(): void {
    this.cdRef.detectChanges();
  }

  onClose() {
    this.isError = false;
  }

  ngOnInit() {
    //remoteCallStatus represents one of the props in IAppState structure
    this.store.select('remoteCallStatus').subscribe((x: RemoteCallStatus) => {
      if (x) {
        this.isAlert = x.messageType === ALERT;
        this.isPending = x.messageType === PENDING;
        this.isError = x.messageType === ERROR;
        this.isConfirm = x.messageType === CONFIRM;
        this.message = x.messageValue;

        if (this.isConfirm) {
          this.yesAsyncCallback = x.yesAsyncCallback;
        }
      }
    });
  }
}
