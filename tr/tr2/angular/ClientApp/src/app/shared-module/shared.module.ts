import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { StoreModule } from '@ngrx/store';
import { EffectsModule } from '@ngrx/effects';
import { ModuleWithProviders } from '@angular/compiler/src/core';
import { HomeComponent } from './components/home/home.component';
import { ProductSearchComponent } from '../admin-module/products/product-search/product-search.component';
import { appReducer } from './reducers/shared-reducer-selector';
import { InitAppEffects } from './reducers/app-init-reducer/app-init-effects';
import { AlertMessageComponent } from './components/alert-message/alert-message.component';
import { NavMenuComponent } from './components/nav-menu/nav-menu.component';
import { SpinerComponent } from './components/spiner/spiner.component';
import { CategoryPipe } from './pipes/category.pipe';
import { CategoriesService } from './services/categories.service';
import { SecurityService } from './services/security.service';
@NgModule({
  declarations: [
    AlertMessageComponent,
    NavMenuComponent,
    HomeComponent,
    SpinerComponent,
    CategoryPipe,
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
  ]
})
export class SharedModule {
  static ForRoot(): ModuleWithProviders {
    return {
      ngModule: SharedModule,
      providers: [SecurityService, CategoriesService]
    }
  }
}
