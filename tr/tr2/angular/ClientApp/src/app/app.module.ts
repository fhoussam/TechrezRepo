import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { AdminModule } from './admin-module/admin.module';
import { SuppliersService } from './services/suppliers.service';
import { ProductMainComponent } from './admin-module/products/product-main/product-main.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    AdminModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'products', component: ProductMainComponent },
    ])
  ],
  exports: [
    AdminModule,
  ],
  providers: [SuppliersService],
  bootstrap: [AppComponent]
})
export class AppModule { }
