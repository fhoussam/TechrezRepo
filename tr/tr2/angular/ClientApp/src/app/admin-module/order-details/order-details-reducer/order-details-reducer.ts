import { SearchOrderDetailsResponse } from "../../../models/order-details-models";
import { OrderDetailsAction, SEARCH_ORDER_DETAILS_END, SearchOrderDetailsEnd } from "./order-details-actions";
import { PagedList } from "../../../models/PagedList";

export interface OrderDetailsState
{
  list: PagedList<SearchOrderDetailsResponse>;
  selectedItem: number;
}

const OrderDetailsStateInitalState: OrderDetailsState =
{
  list: null,
  selectedItem: null,
}

export function OrderDetailsReducer(state = OrderDetailsStateInitalState, action: OrderDetailsAction) {
  switch (action.type) {
    case SEARCH_ORDER_DETAILS_END:
      return {
        ...state,
        list: (action as SearchOrderDetailsEnd).payload,
      }
    default:
      state;
  }
}
