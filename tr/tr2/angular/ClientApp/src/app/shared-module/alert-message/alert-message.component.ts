import { Component, Input, EventEmitter } from '@angular/core';
import { Store } from '@ngrx/store';
import { RemoteCallAction, SUCCESS } from '../remote-call-reducer/remote-call-actions';
import { IAppState } from '../shared-reducer-selector';

@Component({
  selector: 'app-alert-message',
  templateUrl: './alert-message.component.html',
  styleUrls: ['./alert-message.component.css']
})
export class AlertMessageComponent {

  @Input() message: string;

  constructor(private store:Store<IAppState>) { }

  onClose() {
    this.store.dispatch(new RemoteCallAction({ messageType: SUCCESS, messageValue: null }));
  }

}
