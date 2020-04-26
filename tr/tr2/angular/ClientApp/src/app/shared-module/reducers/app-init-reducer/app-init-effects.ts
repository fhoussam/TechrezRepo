import { Actions, ofType, Effect, OnRunEffects, EffectNotification } from '@ngrx/effects'
import { switchMap, map, exhaustMap, takeUntil } from 'rxjs/operators';
import { Injectable } from '@angular/core';
import { InitCategoriesEnd, INIT_APP_BEGIN, INIT_APP_END, INIT_CATEGORIES_BEGIN, INIT_ANTIFORGERY_BEGIN, InitAntiForgeryEnd } from './app-init-actions';
import { Observable } from 'rxjs';
import { SecurityService } from '../../services/security.service';
import { HttpClient } from '@angular/common/http';
import { DropDownListIdentifier } from '../../../models/config-models';

@Injectable()
export class InitAppEffects
  implements OnRunEffects
{

  constructor(private actions$: Actions, private http: HttpClient , private securityService: SecurityService) { }

  ngrxOnRunEffects(resolvedEffects$: Observable<EffectNotification>): Observable<EffectNotification> {
    return this.actions$.pipe(
      ofType(INIT_APP_BEGIN),
      exhaustMap(() => resolvedEffects$.pipe(takeUntil(this.actions$.pipe(ofType(INIT_APP_END)))))
    );
  }

  @Effect()
  initAntiForgery = this.actions$.pipe(
    ofType(INIT_ANTIFORGERY_BEGIN),
    switchMap(() => {
      return this.securityService.getAntiForgery().pipe(
        map(() => { return new InitAntiForgeryEnd(); })
      )
    })
  );

  @Effect()
  initCategories = this.actions$.pipe(
    ofType(INIT_CATEGORIES_BEGIN),
    switchMap(() => {
      return this.http.get('api/config/categories').pipe(
        map(resData => {
          return new InitCategoriesEnd(resData[DropDownListIdentifier.Categories]);
        }),
      );
    })
  );
}
