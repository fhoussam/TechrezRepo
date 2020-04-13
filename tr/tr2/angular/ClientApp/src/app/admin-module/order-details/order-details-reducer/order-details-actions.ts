import { Action } from "@ngrx/store";
import { SearchOrderDetailsResponse, SearchOrderDetailsQuery } from "../../../models/order-details-models";
import { PagedList } from "../../../models/PagedList";

export const SEARCH_ORDER_DETAILS_BEGIN = "SEARCH_ORDER_DETAILS_BEGIN";
export const SEARCH_ORDER_DETAILS_END = "SEARCH_ORDER_DETAILS_END";
export const SELECT_ORDER_DETAILS_BEGIN = "SELECT_ORDER_DETAILS_BEGIN";
export const SELECT_ORDER_DETAILS_END = "SELECT_ORDER_DETAILS_END";
export const SHOWINFO_ORDER_DETAILS_BEGIN = "SHOWINFO_ORDER_DETAILS_BEGIN";
export const SHOWINFO_ORDER_DETAILS_END = "SHOWINFO_ORDER_DETAILS_END";
export const EDIT_ORDER_DETAILS_BEGIN = "EDIT_ORDER_DETAILS_BEGIN";
export const EDIT_ORDER_DETAILS_END = "EDIT_ORDER_DETAILS_END";
export const DELETE_ORDER_DETAILS_BEGIN = "DELETE_ORDER_DETAILS_BEGIN";
export const DELETE_ORDER_DETAILS_END = "DELETE_ORDER_DETAILS_END";

export class SearchOrderDetailsBegin implements Action {
  readonly type: string = SEARCH_ORDER_DETAILS_BEGIN;
  constructor(public payload: SearchOrderDetailsQuery) { }
}

export class SearchOrderDetailsEnd implements Action {
  readonly type: string = SEARCH_ORDER_DETAILS_END;
  constructor(public payload: PagedList<SearchOrderDetailsResponse>) { }
}

export class SelectOrderDetailsBegin implements Action {
  readonly type: string = SELECT_ORDER_DETAILS_BEGIN;
}

export class SelectOrderDetailsEnd implements Action {
  readonly type: string = SELECT_ORDER_DETAILS_END;
  constructor(public payload: SearchOrderDetailsResponse) { }
}

export class ShowinfoOrderDetailsBegin implements Action {
  readonly type: string = SHOWINFO_ORDER_DETAILS_BEGIN;
}

export class ShowinfoOrderDetailsEnd implements Action {
  readonly type: string = SHOWINFO_ORDER_DETAILS_END;
  constructor(public payload: any) { }
}

export class EditOrderDetailsBegin implements Action {
  readonly type: string = EDIT_ORDER_DETAILS_BEGIN;
}

export class EditOrderDetailsEnd implements Action {
  readonly type: string = EDIT_ORDER_DETAILS_END;
  constructor(public payload: any) { }
}

export class DeleteOrderDetailsBegin implements Action {
  readonly type: string = DELETE_ORDER_DETAILS_BEGIN;
}

export class DeleteOrderDetailsEnd implements Action {
  readonly type: string = DELETE_ORDER_DETAILS_END;
  constructor(public payload: any) { }
}

export type OrderDetailsAction =
  SearchOrderDetailsBegin |
  SearchOrderDetailsEnd |
  SelectOrderDetailsBegin |
  SelectOrderDetailsEnd |
  ShowinfoOrderDetailsBegin |
  ShowinfoOrderDetailsEnd |
  EditOrderDetailsBegin |
  EditOrderDetailsEnd |
  DeleteOrderDetailsEnd |
  DeleteOrderDetailsBegin
  ;
