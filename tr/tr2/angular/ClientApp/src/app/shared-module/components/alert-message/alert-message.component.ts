import { Component, Input, EventEmitter } from '@angular/core';
import { Store } from '@ngrx/store';
import { SUCCESS, RemoteCallAction } from '../../reducers/spiner-reducer/spiner-actions';
import { IAppState } from '../../reducers/shared-reducer-selector';

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