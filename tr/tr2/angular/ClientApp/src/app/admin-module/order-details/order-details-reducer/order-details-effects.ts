import { Injectable } from "@angular/core";
import { OrderDetailsService } from "../../../services/order-details.service";
import { Actions, ofType, Effect } from "@ngrx/effects";
import { SEARCH_ORDER_DETAILS_BEGIN, SearchOrderDetailsEnd, SearchOrderDetailsBegin } from "./order-details-actions";
import { switchMap, map } from "rxjs/operators";
import { SearchOrderDetailsQuery, SearchOrderDetailsResponse } from "../../../models/order-details-models";
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
}
