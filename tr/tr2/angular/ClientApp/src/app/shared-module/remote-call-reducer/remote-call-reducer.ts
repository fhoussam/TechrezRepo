import { RemoteCallAction, SUCCESS } from "./remote-call-actions";

export class RemoteCallStatus {
  messageType: string;
  messageValue: string;
}

const initialState = {
  messageType: SUCCESS,
  messageValue: null,
};

export function remoteCallStatusReducer(state = initialState, action: RemoteCallAction) {
  try {
    let data = action.payload as RemoteCallStatus;
    return {
      messageType: data.messageType,
      messageValue: data.messageValue,
    }
  } catch (e) {
    return state;
  }
}
