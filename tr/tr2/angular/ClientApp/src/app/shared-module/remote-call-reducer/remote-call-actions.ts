import { Action } from "@ngrx/store";
import { RemoteCallStatus } from "./remote-call-reducer";

export const SUCCESS = 'SUCCESS';
export const ERROR = 'ERROR';
export const ALERT = 'ALERT';
export const PENDING = 'PENDING';

export class RemoteCallAction implements Action {
  readonly type;
  constructor(public payload: RemoteCallStatus) {
    this.type = payload.messageType;
  }
}
