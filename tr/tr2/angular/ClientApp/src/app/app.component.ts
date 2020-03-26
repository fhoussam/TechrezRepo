import { Component, ChangeDetectorRef, AfterViewChecked } from '@angular/core';
import { Store } from '@ngrx/store';
import { IAppState } from './shared-module/remote-call-reducer/remote-call-reducer';
import { PENDING } from './shared-module/remote-call-reducer/remote-call-actions';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements AfterViewChecked{

  title = 'app';

  isProcessing: boolean;
  messageValue: string;

  constructor(
    private store: Store<IAppState>,
    private cdRef: ChangeDetectorRef
  ) { }

  //still no idea how code below works
  ngAfterViewChecked(): void {
    this.cdRef.detectChanges();
  }

  ngOnInit() {
    //remoteCallStatus represents one of the props in IAppState structure
    this.store.select('remoteCallStatus').subscribe(x => {
      //a switch case is needed here!
      this.isProcessing = x.messageType == PENDING;
      this.messageValue = x.messageValue;
    });
  }
}
