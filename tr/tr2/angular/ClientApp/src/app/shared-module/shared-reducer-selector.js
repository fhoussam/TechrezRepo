"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var remote_call_reducer_1 = require("./remote-call-reducer/remote-call-reducer");
var app_init_reducer_1 = require("./app-init-reducer/app-init-reducer");
exports.appReducer = {
    remoteCallStatus: remote_call_reducer_1.remoteCallStatusReducer,
    appInitState: app_init_reducer_1.appInitReducer,
};
//export function get_settings(store: Store<IAppState>, categoriesService: CategoriesService): Function {
//  return () => new Promise(resolve => {
//    categoriesService.getCategories().toPromise();
//    //store.dispatch(new InitAppBegin());
//    //store.dispatch(new InitCategoriesBegin());
//    //store.select((state: any) => state.appState.users)
//    //  .pipe(
//    //    filter(users => users !== null && users !== undefined && users.length > 0),
//    //    take(1)
//    //  ).subscribe((users) => {
//    //    store.dispatch(new InitAppEnd());
//    //    resolve(true);
//    //  });
//  })
//}
function get_settings(store, categoriesService) {
    //store.dispatch(new InitCategoriesBegin());
    var result = function () { return categoriesService.getCategories().toPromise(); };
    return result;
}
exports.get_settings = get_settings;
//# sourceMappingURL=shared-reducer-selector.js.map