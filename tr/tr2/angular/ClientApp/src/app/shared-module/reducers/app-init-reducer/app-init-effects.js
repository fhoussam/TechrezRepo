"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var effects_1 = require("@ngrx/effects");
var operators_1 = require("rxjs/operators");
var core_1 = require("@angular/core");
var app_init_actions_1 = require("./app-init-actions");
var InitAppEffects = /** @class */ (function () {
    function InitAppEffects(actions$, categoryService, securityService) {
        var _this = this;
        this.actions$ = actions$;
        this.categoryService = categoryService;
        this.securityService = securityService;
        //ngrxOnRunEffects(resolvedEffects$: Observable<EffectNotification>): Observable<EffectNotification> {
        //  return this.actions$.pipe(
        //    ofType(INIT_APP_BEGIN),
        //    exhaustMap(() => resolvedEffects$.pipe(takeUntil(this.actions$.pipe(ofType(INIT_APP_END)))))
        //  );
        //}
        this.initAntiForgery = this.actions$.pipe(effects_1.ofType(app_init_actions_1.INIT_ANTIFORGERY_BEGIN), operators_1.switchMap(function (action) {
            return _this.securityService.getAntiForgery().pipe(operators_1.map(function (x) { return new app_init_actions_1.InitAntiForgeryEnd(); }));
        }));
        this.initCategories = this.actions$.pipe(effects_1.ofType(app_init_actions_1.INIT_CATEGORIES_BEGIN), operators_1.switchMap(function (initCategoriesBegin) {
            return _this.categoryService.getCategories().pipe(operators_1.map(function (resData) { return new app_init_actions_1.InitCategoriesEnd(resData); }));
        }));
    }
    __decorate([
        effects_1.Effect()
    ], InitAppEffects.prototype, "initAntiForgery", void 0);
    __decorate([
        effects_1.Effect()
    ], InitAppEffects.prototype, "initCategories", void 0);
    InitAppEffects = __decorate([
        core_1.Injectable()
    ], InitAppEffects);
    return InitAppEffects;
}());
exports.InitAppEffects = InitAppEffects;
//# sourceMappingURL=app-init-effects.js.map