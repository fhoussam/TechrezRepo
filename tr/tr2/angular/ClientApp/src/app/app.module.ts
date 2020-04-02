import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppComponent } from './app.component';
import { AdminModule } from './admin-module/admin.module';
import { SharedModule } from './shared-module/shared.module';
import { EffectsModule } from '@ngrx/effects';

@NgModule({
  declarations: [
    AppComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    AdminModule,
    SharedModule.ForRoot(),
    EffectsModule.forFeature([])
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
