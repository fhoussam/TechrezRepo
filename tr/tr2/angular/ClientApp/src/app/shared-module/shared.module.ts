import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AlertMessageComponent } from './alert-message/alert-message.component';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { ProductSearchComponent } from '../admin-module/products/product-search/product-search.component';
import { HttpClientModule } from '@angular/common/http';
import { StoreModule } from '@ngrx/store';
import { SpinerComponent } from './spiner/spiner.component';
import { EffectsModule } from '@ngrx/effects';
import { InitAppEffects } from './app-init-reducer/app-init-effects';
import { appReducer } from './shared-reducer-selector';

@NgModule({
  declarations: [
    AlertMessageComponent,
    NavMenuComponent,
    HomeComponent,
    SpinerComponent,
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
  ]
})
export class SharedModule { }
