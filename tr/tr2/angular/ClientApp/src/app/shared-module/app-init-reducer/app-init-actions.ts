import { Action } from "@ngrx/store";

export const INIT_APP_BEGIN = "INIT_APP_BEGIN";
export const INIT_ANTIFORGERY_BEGIN = "INIT_ANTIFORGERY_BEGIN";
export const INIT_ANTIFORGERY_END = "INIT_ANTIFORGERY_END";
export const INIT_CATEGORIES_BEGIN = "INIT_CATEGORIES_BEGIN";
export const INIT_CATEGORIES_END = "INIT_CATEGORIES_END";
export const INIT_APP_END = "INIT_APP_END";

export class InitAppBegin implements Action {
  readonly type: string = "INIT_APP_BEGIN";
  constructor() { }
}

export class InitAntiForgeryBegin implements Action {
  readonly type: string = "INIT_ANTIFORGERY_BEGIN";
  constructor() { }
}

export class InitAntiForgeryEnd implements Action {
  readonly type: string = "INIT_ANTIFORGERY_END";
  constructor() { }
}

export class InitCategoriesBegin implements Action {
  readonly type: string = "INIT_CATEGORIES_BEGIN";
  constructor() { }
}

export class InitCategoriesEnd implements Action {
  readonly type: string = "INIT_CATEGORIES_END";
  constructor(public payload: any) { }
}

export class InitAppEnd implements Action {
  readonly type: string = "INIT_APP_END";
  constructor() { }
}

export type InitAppAction =
  InitAppBegin |
  InitCategoriesBegin |
  InitCategoriesEnd |
  InitAppEnd;
