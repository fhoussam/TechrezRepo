import { Component, ChangeDetectorRef, AfterViewChecked } from '@angular/core';
import { Store } from '@ngrx/store';
import { IAppState } from './shared-module/reducers/shared-reducer-selector';
import { PENDING, ALERT, ERROR } from './shared-module/reducers/spiner-reducer/spiner-actions';

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
  message: string;

  constructor(
    private store: Store<IAppState>,
    private cdRef: ChangeDetectorRef,
  ) { }

  //still no idea how code below works
  ngAfterViewChecked(): void {
    this.cdRef.detectChanges();
  }

  ngOnInit() {
    //remoteCallStatus represents one of the props in IAppState structure
    this.store.select('remoteCallStatus').subscribe(x => {
      if (x) {
        console.log('aaaaaaaa',x.messageValue);
        this.isAlert = x.messageType == ALERT;
        this.isPending = x.messageType == PENDING;
        this.isError = x.messageType == ERROR;
        this.message = x.messageValue;
      }
    });
  }
}
