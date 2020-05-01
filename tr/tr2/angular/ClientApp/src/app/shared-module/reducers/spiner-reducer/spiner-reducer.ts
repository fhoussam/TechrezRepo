import { SUCCESS, TRemoteCallAction, ErrorAction, PendingAction, AlertAction, ALERT, PENDING, CONFIRM, ConfirmPayload, ConfirmAction } from "./spiner-actions";
import { Observable } from "rxjs";
import { ERROR } from "./spiner-actions";

export interface RemoteCallStatus {
  messageType: string;
  messageValue: string;
  yesAsyncCallback: Observable<any>;
}

const initialState: RemoteCallStatus = {
  messageType: SUCCESS,
  messageValue: null,
  yesAsyncCallback: null,
};

export function remoteCallStatusReducer(state: RemoteCallStatus = initialState, action: TRemoteCallAction): RemoteCallStatus {

  switch (action.type) {

    case ERROR:
      return { ...state, messageType: SUCCESS, messageValue: (action as ErrorAction).payload }

    case ALERT:
      return { ...state, messageType: ALERT, messageValue: (action as AlertAction).payload }

    case PENDING:
      return { ...state, messageType: PENDING, messageValue: (action as PendingAction).payload }

    case SUCCESS:
      return { ...state, messageType: SUCCESS, messageValue: null, yesAsyncCallback: null }

    case CONFIRM: {
      const confirmPayload = (action as ConfirmAction).payload as ConfirmPayload;
      return { ...state, messageType: CONFIRM, messageValue: confirmPayload.messageValue, yesAsyncCallback: confirmPayload.yesAsyncCallback };
    }

    default:
      return initialState;
  }
}
