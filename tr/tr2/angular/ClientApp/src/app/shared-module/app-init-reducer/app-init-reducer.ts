import { ICategory } from "../../models/ICategory";
import { InitAppAction, INIT_CATEGORIES_END, InitCategoriesEnd } from "./app-init-actions";

export interface IAppInitState {
  categories: ICategory[]
}

const appInitInitalState: IAppInitState = {
  categories : [],
}

export function appInitReducer(state: IAppInitState = appInitInitalState, action: InitAppAction): IAppInitState {
  switch (action.type) {

    case INIT_CATEGORIES_END:
      console.log('app settings initialized ', (action as InitCategoriesEnd).payload);
      return { ...state, categories: (action as InitCategoriesEnd).payload };

    default:
      return state;
  } 
}
