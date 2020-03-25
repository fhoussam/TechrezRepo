import { Component } from '@angular/core';
import { Store } from '@ngrx/store';
import { RemoteCallStatus } from './shared-module/remote-call-reducer/remote-call-reducer';
import { PENDING } from './shared-module/remote-call-reducer/remote-call-actions';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'app';

  isProcessing: boolean;
  messageValue: string;

  constructor(
    private store: Store<{ remoteCallStatusStoreKey: RemoteCallStatus }>
  ) { }

  ngOnInit() {
    this.store.select('remoteCallStatusStoreKey').subscribe(x => {
      this.isProcessing = x.messageType == PENDING;
      this.messageValue = x.messageValue;
    });
  }
}
