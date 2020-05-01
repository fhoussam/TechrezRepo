import { Component, Input } from '@angular/core';
import { Observable, of } from 'rxjs';
import { Store } from '@ngrx/store';
import { IAppState } from '../../reducers/shared-reducer-selector';
import { SuccessAction } from '../../reducers/spiner-reducer/spiner-actions';

@Component({
  selector: 'app-confirmation-message',
  templateUrl: './confirmation-message.component.html',
  styleUrls: ['./confirmation-message.component.css']
})
export class ConfirmationMessageComponent {

  constructor(private store: Store<IAppState>) { }

  @Input() message: string;
  @Input() yesAsyncCallback: Observable<void>

  onClose() {
    this.store.dispatch(new SuccessAction());
  }

  onConfirm() {
    //no need to dispatch success action as the spiner interceptor will do after a 200 server response
    this.yesAsyncCallback.subscribe(() => of({}));
  }
}
