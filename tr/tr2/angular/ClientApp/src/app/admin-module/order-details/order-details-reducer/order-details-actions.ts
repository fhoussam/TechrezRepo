import { Action } from "@ngrx/store";
import { SearchOrderDetailsResponse, SearchOrderDetailsQuery, OrderDetails, GetOrderDetailsForDisplayResponse, GetOrderDetailsForEditResponse, EditOrderDetailCommand } from "../../../models/order-details-models";
import { PagedList } from "../../../models/PagedList";

export const SEARCH_ORDER_DETAILS_BEGIN = "SEARCH_ORDER_DETAILS_BEGIN";
export const SEARCH_ORDER_DETAILS_END = "SEARCH_ORDER_DETAILS_END";
export const SELECT_ORDER_DETAILS_BEGIN = "SELECT_ORDER_DETAILS_BEGIN";
export const SELECT_ORDER_DETAILS_END_FOR_DISPLAY = "SELECT_ORDER_DETAILS_END_FOR_DISPLAY";
export const SELECT_ORDER_DETAILS_END_FOR_EDIT = "SELECT_ORDER_DETAILS_END_FOR_EDIT";
export const SHOWINFO_ORDER_DETAILS_BEGIN = "SHOWINFO_ORDER_DETAILS_BEGIN";
export const SHOWINFO_ORDER_DETAILS_END = "SHOWINFO_ORDER_DETAILS_END";
export const EDIT_ORDER_DETAILS_BEGIN = "EDIT_ORDER_DETAILS_BEGIN";
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
  constructor(public orderId: number, public productId: number, public forEdit: boolean) { }
}

export class SelectOrderDetailsEndForDisplay implements Action {
  readonly type: string = SELECT_ORDER_DETAILS_END_FOR_DISPLAY;
  constructor(public payload: GetOrderDetailsForDisplayResponse) { }
}

export class SelectOrderDetailsEndForEdit implements Action {
  readonly type: string = SELECT_ORDER_DETAILS_END_FOR_EDIT;
  constructor(public payload: GetOrderDetailsForEditResponse) { }
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
  constructor(public payload: EditOrderDetailCommand) { }
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
  SelectOrderDetailsEndForDisplay |
  SelectOrderDetailsEndForEdit |
  ShowinfoOrderDetailsBegin |
  ShowinfoOrderDetailsEnd |
  EditOrderDetailsBegin |
  DeleteOrderDetailsEnd |
  DeleteOrderDetailsBegin
  ;
