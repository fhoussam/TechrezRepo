import { SearchOrderDetailsResponse, GetOrderDetailsForDisplayResponse, GetOrderDetailsForEditResponse } from "../../../models/order-details-models";
import { OrderDetailsAction, SEARCH_ORDER_DETAILS_END, SearchOrderDetailsEnd, SELECT_ORDER_DETAILS_BEGIN, SelectOrderDetailsBegin, SelectOrderDetailsEndForDisplay, SelectOrderDetailsEndForEdit, SELECT_ORDER_DETAILS_END_FOR_DISPLAY, SELECT_ORDER_DETAILS_END_FOR_EDIT } from "./order-details-actions";
import { PagedList } from "../../../models/PagedList";

export interface OrderDetailsState
{
  list: PagedList<SearchOrderDetailsResponse>;
  selectedItemId: number;
  selectedItemForDisplay: GetOrderDetailsForDisplayResponse;
  selectedItemForEdit: GetOrderDetailsForEditResponse;
  isEditMode: boolean;
}

const OrderDetailsStateInitalState: OrderDetailsState =
{
  list: null,
  selectedItemId: null,
  selectedItemForDisplay: null,
  selectedItemForEdit: null,
  isEditMode: false,
}

export function OrderDetailsReducer(state = OrderDetailsStateInitalState, action: OrderDetailsAction) {
  switch (action.type) {
    case SEARCH_ORDER_DETAILS_END:
      return {
        ...state,
        list: (action as SearchOrderDetailsEnd).payload,
      }
    case SELECT_ORDER_DETAILS_BEGIN:
      return {
        ...state,
      }
    case SELECT_ORDER_DETAILS_END_FOR_DISPLAY:
      return {
        ...state,
        selectedItemForDisplay: (action as SelectOrderDetailsEndForDisplay).payload,
        selectedItemId: (action as SelectOrderDetailsEndForDisplay).payload.orderId,
        isEditMode: false,
      }
    case SELECT_ORDER_DETAILS_END_FOR_EDIT:
      return {
        ...state,
        selectedItemForEdit: (action as SelectOrderDetailsEndForEdit).payload,
        selectedItemId: (action as SelectOrderDetailsEndForDisplay).payload.orderId,
        isEditMode: true,
      }
    default:
      state;
  }
}
