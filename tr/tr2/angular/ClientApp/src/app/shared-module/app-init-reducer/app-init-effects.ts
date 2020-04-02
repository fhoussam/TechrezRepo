import { Actions, createEffect, ofType, Effect, OnRunEffects, EffectNotification } from '@ngrx/effects'
import { switchMap, map, exhaustMap, takeUntil } from 'rxjs/operators';
import { Injectable } from '@angular/core';
import { CategoriesService } from '../../services/categories.service';
import { InitCategoriesEnd, InitCategoriesBegin, INIT_APP_BEGIN, InitAppBegin, InitAppEnd, INIT_APP_END, INIT_CATEGORIES_BEGIN, INIT_ANTIFORGERY_BEGIN, InitAntiForgeryBegin, InitAntiForgeryEnd } from './app-init-actions';
import { Observable } from 'rxjs';
import { SecurityService } from '../../services/security.service';

@Injectable()
export class InitAppEffects
  implements OnRunEffects
{

  constructor(private actions$: Actions, private categoryService: CategoriesService, private securityService: SecurityService) { }

  ngrxOnRunEffects(resolvedEffects$: Observable<EffectNotification>): Observable<EffectNotification> {
    return this.actions$.pipe(
      ofType(INIT_APP_BEGIN),
      exhaustMap(() => resolvedEffects$.pipe(takeUntil(this.actions$.pipe(ofType(INIT_APP_END)))))
    );
  }

  @Effect()
  initAntiForgery = this.actions$.pipe(
    ofType(INIT_ANTIFORGERY_BEGIN),
    switchMap((action: InitAntiForgeryBegin) => {
      return this.securityService.getAntiForgery().pipe(
        map(x => { return new InitAntiForgeryEnd(); })
      )
    })
  );

  @Effect()
  initCategories = this.actions$.pipe(
    ofType(INIT_CATEGORIES_BEGIN),
    switchMap((initCategoriesBegin: InitCategoriesBegin) => {
      return this.categoryService.getCategories().pipe(
        map(resData => { return new InitCategoriesEnd(resData); }),
      );
    })
  );
}
