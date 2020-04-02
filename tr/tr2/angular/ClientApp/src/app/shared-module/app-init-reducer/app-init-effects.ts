import { Actions, createEffect, ofType, Effect, OnRunEffects, EffectNotification } from '@ngrx/effects'
import { switchMap, map, exhaustMap, takeUntil } from 'rxjs/operators';
import { Injectable } from '@angular/core';
import { CategoriesService } from '../../services/categories.service';
import { InitCategoriesEnd, InitCategoriesBegin, INIT_APP_BEGIN, InitAppBegin, InitAppEnd, INIT_APP_END } from './app-init-actions';
import { Observable } from 'rxjs';

@Injectable()
export class InitAppEffects
  implements OnRunEffects
{

  constructor(private actions$: Actions, private categoryService: CategoriesService) { }

  ngrxOnRunEffects(resolvedEffects$: Observable<EffectNotification>): Observable<EffectNotification> {
    return this.actions$.pipe(
      ofType(INIT_APP_BEGIN),
      exhaustMap(() => resolvedEffects$.pipe(takeUntil(this.actions$.pipe(ofType(INIT_APP_END)))))
    );
  }

  @Effect()
  initCategories = this.actions$.pipe(
    ofType('INIT_CATEGORIES_BEGIN'),
    switchMap((initCategoriesBegin: InitCategoriesBegin) => {
      return this.categoryService.getCategories().pipe(
        map(resData => { return new InitCategoriesEnd(resData); }),
      );
    })
  );
}
