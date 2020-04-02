import { InitAppAction, INIT_CATEGORIES_END, InitCategoriesEnd, INIT_ANTIFORGERY_END } from "./app-init-actions";
import { ICategory } from "../../../models/ICategory";

export interface IAppInitState {
  categories: ICategory[],
  antiforgery: boolean,
}

const appInitInitalState: IAppInitState = {
  categories: [],
  antiforgery: false,
}

export function appInitReducer(state: IAppInitState = appInitInitalState, action: InitAppAction): IAppInitState {
  switch (action.type) {

    case INIT_ANTIFORGERY_END:
      console.log('app settings - antiforgery initialized ');
      return { ...state, antiforgery: true };

    case INIT_CATEGORIES_END:
      console.log('app settings - categories initialized ', (action as InitCategoriesEnd).payload);
      return { ...state, categories: (action as InitCategoriesEnd).payload };

    default:
      return state;
  } 
}
