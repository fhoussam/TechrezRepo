import { BrowserModule } from '@angular/platform-browser';
import { NgModule, APP_INITIALIZER } from '@angular/core';
import { AppComponent } from './app.component';
import { AdminModule } from './admin-module/admin.module';
import { SharedModule } from './shared-module/shared.module';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { SpinerInterceptorService } from './interceptors/spiner-interceptor.service';
import { EffectsModule } from '@ngrx/effects';
import { Store } from '@ngrx/store';
import { SecurityService } from './services/security.service';
import { CookieService } from 'ngx-cookie-service'
import { get_settings } from './shared-module/reducers/shared-reducer-selector';

@NgModule({
  declarations: [
    AppComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    AdminModule,
    SharedModule,
    EffectsModule.forFeature([])
  ],
  exports: [

  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: SpinerInterceptorService,
      multi: true,
    },
    {
      provide: APP_INITIALIZER,
      useFactory: get_settings,
      deps: [Store],
      multi: true
    },
    SecurityService,
    CookieService,
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
