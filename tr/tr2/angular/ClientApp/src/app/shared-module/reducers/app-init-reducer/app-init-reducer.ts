import { InitAppAction, INIT_CATEGORIES_END, InitCategoriesEnd, INIT_ANTIFORGERY_END } from "./app-init-actions";
import { DdlKeyValue } from "../../../models/config-models";

export interface AppInitState {
  categories: DdlKeyValue[];
  antiforgery: boolean;
}

const appInitInitalState: AppInitState = {
  categories: [],
  antiforgery: false,
}

export function appInitReducer(state: AppInitState = appInitInitalState, action: InitAppAction): AppInitState {
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
