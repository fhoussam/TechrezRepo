import { Component, ChangeDetectorRef, AfterViewChecked } from '@angular/core';
import { Store } from '@ngrx/store';
import { PENDING, ALERT } from './shared-module/remote-call-reducer/remote-call-actions';
import { InitCategoriesBegin } from './shared-module/app-init-reducer/app-init-actions';
import { IAppState } from './shared-module/shared-reducer-selector';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements AfterViewChecked{

  title = 'app';

  isPending: boolean;
  isAlert: boolean;
  messageValue: string;

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
        this.isAlert = x.messageType == ALERT;
        this.isPending = x.messageType == PENDING;
        this.messageValue = x.messageValue;
      }
    });
  }
}
