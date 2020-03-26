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

export function remoteCallStatusReducer(state: IRemoteCallStatus = initialState, action: RemoteCallAction): IRemoteCallStatus {
  if (action.type === "@ngrx/store/init") { //hacky, but it seems like "@ngrx/store/init" is actually the first action made by redux
    return initialState;
  }
  else
    return action.payload as IRemoteCallStatus;
}
