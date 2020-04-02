import { RemoteCallAction, SUCCESS } from "./remote-call-actions";

export interface IRemoteCallStatus {
  messageType: string;
  messageValue: string;
}

const initialState: IRemoteCallStatus = {
  messageType: SUCCESS,
  messageValue: null,
};

export function remoteCallStatusReducer(state: IRemoteCallStatus = initialState, action: RemoteCallAction): IRemoteCallStatus {
  if (action.type === "@ngrx/store/init") { //hacky, but it seems like "@ngrx/store/init" is actually the first action made by redux
    return initialState;
  }
  else
    return action.payload as IRemoteCallStatus;
}
