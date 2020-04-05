import { SUCCESS, RemoteCallAction } from "./spiner-actions";

export interface IRemoteCallStatus {
  messageType: string;
  messageValue: string;
}

const initialState: IRemoteCallStatus = {
  messageType: SUCCESS,
  messageValue: null,
};

export function remoteCallStatusReducer(state: IRemoteCallStatus = initialState, action: RemoteCallAction): IRemoteCallStatus {

  console.log(action);

  if (action.type === "@ngrx/store/init") { //hacky, but it seems like "@ngrx/store/init" is actually the first action made by redux
    return initialState;
  }
  else
    return action.payload as IRemoteCallStatus;
}
