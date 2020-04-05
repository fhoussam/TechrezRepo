import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppComponent } from './app.component';
import { AdminModule } from './admin-module/admin.module';
import { SharedModule } from './shared-module/shared.module';
import { EffectsModule } from '@ngrx/effects';
import { Routes, RouterModule } from '@angular/router';
import { NotFoundComponent } from './shared-module/components/not-found/not-found.component';

const routes: Routes = [
  {
    path: "**",
    component: NotFoundComponent
  }
]

@NgModule({
  declarations: [
    AppComponent,
  ],
  imports: [
    RouterModule.forRoot(routes),
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    AdminModule,
    SharedModule.ForRoot(),
    EffectsModule.forFeature([])
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
