import { Action } from "@ngrx/store";
import { Observable } from "rxjs";

export const SUCCESS = 'SUCCESS';
export const ERROR = 'ERROR';
export const ALERT = 'ALERT';
export const PENDING = 'PENDING';
export const CONFIRM = 'CONFIRM';

export class SuccessAction implements Action {
  readonly type: string = SUCCESS;
}

export class ErrorAction implements Action {
  readonly type: string = ERROR;
  constructor(public payload: string) { }
}

export class AlertAction implements Action {
  readonly type: string = ALERT;
  constructor(public payload: string) { }
}

export class PendingAction implements Action {
  readonly type: string = PENDING;
  constructor(public payload: string) { }
}

export class ConfirmAction implements Action {
  readonly type: string = CONFIRM;
  constructor(public payload: ConfirmPayload) { }
}

export interface ConfirmPayload {
  messageValue: string;
  yesAsyncCallback: Observable<any>;
}

export type TRemoteCallAction =
  SuccessAction
  | AlertAction
  | PendingAction
  | ConfirmAction;
