import { NgModule, APP_INITIALIZER } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { RouterModule, Router } from '@angular/router';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { StoreModule, Store } from '@ngrx/store';
import { EffectsModule } from '@ngrx/effects';
import { ModuleWithProviders } from '@angular/compiler/src/core';
import { HomeComponent } from './components/home/home.component';
import { ProductSearchComponent } from '../admin-module/products/product-search/product-search.component';
import { appReducer, getSettings } from './reducers/shared-reducer-selector';
import { InitAppEffects } from './reducers/app-init-reducer/app-init-effects';
import { AlertMessageComponent } from './components/alert-message/alert-message.component';
import { NavMenuComponent } from './components/nav-menu/nav-menu.component';
import { SpinerComponent } from './components/spiner/spiner.component';
import { CategoryPipe } from './pipes/category.pipe';
import { SecurityService } from './services/security.service';
import { CookieService } from 'ngx-cookie-service';
import { SpinerInterceptorService } from './interceptors/spiner-interceptor.service';
import { ModalComponent } from './components/modal/modal.component';
import { ErrorMessageComponent } from './components/error-message/error-message.component';
import { NotFoundComponent } from './components/not-found/not-found.component';
import { ErrorInterceptorService } from './interceptors/error-interceptor.service';
import { HttpHelperService } from './services/http-helper';
import { HttpHeaderAppenderInterceptorService } from './interceptors/http-header-appender-interceptor.service';
import { ConfirmationMessageComponent } from './components/confirmation-message/confirmation-message.component';

@NgModule({
  declarations: [
    AlertMessageComponent,
    NavMenuComponent,
    HomeComponent,
    SpinerComponent,
    CategoryPipe,
    ModalComponent,
    ErrorMessageComponent,
    NotFoundComponent,
    ConfirmationMessageComponent,
  ],
  imports: [
    CommonModule,
    HttpClientModule,
    ReactiveFormsModule,
    CommonModule,
    FormsModule,
    RouterModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'products', component: ProductSearchComponent },
    ]),
    StoreModule.forRoot(appReducer),
    EffectsModule.forRoot([InitAppEffects]),
  ],
  exports: [
    HttpClientModule,
    RouterModule,
    FormsModule,
    ReactiveFormsModule,
    CommonModule,
    AlertMessageComponent,
    NavMenuComponent,
    HomeComponent,
    SpinerComponent,
    CategoryPipe,
    ModalComponent,
    ErrorMessageComponent,
    NotFoundComponent,
    ConfirmationMessageComponent,
  ]
})
export class SharedModule {
  static ForRoot(): ModuleWithProviders {
    return {
      ngModule: SharedModule,
      providers: [
        {
          provide: HTTP_INTERCEPTORS,
          useClass: ErrorInterceptorService,
          multi: true,
          deps: [Store, Router]
        },
        {
          provide: HTTP_INTERCEPTORS,
          useClass: SpinerInterceptorService,
          multi: true,
        },
        {
          provide: HTTP_INTERCEPTORS,
          useClass: HttpHeaderAppenderInterceptorService,
          multi: true,
        },
        {
          provide: APP_INITIALIZER,
          useFactory: getSettings,
          deps: [Store],
          multi: true
        },
        CookieService,
        SecurityService,
        HttpHelperService,
        DatePipe,
      ]
    }
  }
}
