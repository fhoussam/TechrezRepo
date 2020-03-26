import { RemoteCallAction, SUCCESS } from "./remote-call-actions";
import { ActionReducerMap, Action } from "@ngrx/store";

export interface IAppState {
  remoteCallStatus: IRemoteCallStatus;
}

export interface IRemoteCallStatus {
  messageType: string;
  messageValue: string;
}

const initialState: IRemoteCallStatus = {
  messageType: SUCCESS,
  messageValue: null,
};

export const appReducer: ActionReducerMap<IAppState> = {
  remoteCallStatus: remoteCallStatusReducer,
};

export function remoteCallStatusReducer(state = initialState, action: RemoteCallAction): IRemoteCallStatus {
  try {
    return action.payload as IRemoteCallStatus;
  } catch (e) {
    return initialState;
  }
}
