import { Injectable } from "@angular/core";
import { OrderDetailsService } from "../../../services/order-details.service";
import { Actions, ofType, Effect } from "@ngrx/effects";
import { SEARCH_ORDER_DETAILS_BEGIN, SearchOrderDetailsEnd, SearchOrderDetailsBegin, SELECT_ORDER_DETAILS_BEGIN, SelectOrderDetailsBegin, SelectOrderDetailsEndForEdit, SelectOrderDetailsEndForDisplay, EDIT_ORDER_DETAILS_BEGIN, EditOrderDetailsBegin } from "./order-details-actions";
import { switchMap, map } from "rxjs/operators";
import { SearchOrderDetailsResponse, OrderDetails, GetOrderDetailsForEditResponse, GetOrderDetailsForDisplayResponse } from "../../../models/order-details-models";
import { PagedList } from "../../../models/PagedList";

@Injectable()
export class OrderDetailsEffects
{
  constructor(
    private orderDetailsService: OrderDetailsService,
    private actions$: Actions,
  ) { }

  @Effect()
  searchOrderDetails = this.actions$.pipe(
    ofType(SEARCH_ORDER_DETAILS_BEGIN),
    switchMap((x: SearchOrderDetailsBegin) => {
      return this.orderDetailsService.searchOrderDetails(x.payload).pipe(
        map(y => {
          const response = y as PagedList<SearchOrderDetailsResponse>;
          return new SearchOrderDetailsEnd(response);
        })
      );
    })
  );

  @Effect()
  selectOrderDetails = this.actions$.pipe(
    ofType(SELECT_ORDER_DETAILS_BEGIN),
    switchMap((x: SelectOrderDetailsBegin) => {
      return this.orderDetailsService.getOrderDetails(x.orderId, x.productId, x.forEdit).pipe(
        map((resp: OrderDetails) => {
          if (x.forEdit) {
            return new SelectOrderDetailsEndForEdit(resp as GetOrderDetailsForEditResponse);
          }
          else
            return new SelectOrderDetailsEndForDisplay(resp as GetOrderDetailsForDisplayResponse)
        })
      );
    })
  );

  @Effect()
  editOrderDetails = this.actions$.pipe(
    ofType(EDIT_ORDER_DETAILS_BEGIN),
    switchMap((x: EditOrderDetailsBegin) => {
      return this.orderDetailsService.editOrderDetails(x.payload).pipe(
        map(() => new SelectOrderDetailsBegin(x.payload.orderId, x.payload.productId, false))
      );
    })
  );
}
