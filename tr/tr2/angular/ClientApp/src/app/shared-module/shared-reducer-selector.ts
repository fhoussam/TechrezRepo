import { IRemoteCallStatus, remoteCallStatusReducer } from "./remote-call-reducer/remote-call-reducer";
import { IAppInitState, appInitReducer } from "./app-init-reducer/app-init-reducer";
import { ActionReducerMap, Store } from "@ngrx/store";
import { InitAppBegin, InitCategoriesBegin, InitAppEnd, InitAntiForgeryBegin } from "./app-init-reducer/app-init-actions";
import { CategoriesService } from "../services/categories.service";
import { filter, take, tap } from "rxjs/operators";
import { APP_SETTINGS } from "./models/APP_SETTINGS";

export interface IAppState {
  remoteCallStatus: IRemoteCallStatus;
  appInitState: IAppInitState;
}

export const appReducer: ActionReducerMap<IAppState> = {
  remoteCallStatus: remoteCallStatusReducer,
  appInitState: appInitReducer,
};

export function get_settings(store: Store<IAppState>) {
  
  let result = () => new Promise(resolve => {
    store.dispatch(new InitAppBegin());
    store.dispatch(new InitAntiForgeryBegin());
    store.select((state: IAppState) => state.appInitState.antiforgery).pipe(
      filter(antiforgery => antiforgery === true),
      take(1),
    ).subscribe(() => {
      store.dispatch(new InitCategoriesBegin());
      store.select((state: IAppState) => state.appInitState.categories)
        .pipe(
          filter(categories => categories !== null && categories !== undefined && categories.length > 0),
          tap(categories => APP_SETTINGS.categories = categories),
          take(1)
        )
        .subscribe((categories) => {
          store.dispatch(new InitAppEnd());
          resolve(true);
        });
    });

  });
  return result;
}
