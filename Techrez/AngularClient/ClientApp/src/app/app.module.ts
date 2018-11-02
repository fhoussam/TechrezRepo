import { BrowserModule } from '@angular/platform-browser';
import { NgModule, Pipe, PipeTransform } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { ProductListAdminComponent } from './components/product-list-admin/product-list-admin.component';
import { ProductEditComponent } from './components/product-edit/product-edit.component';
import { ProductserviceService } from './services/productservice.service';
import { UselessPipe } from './pipes/useless.pipe';
import { ProductListFrontComponent } from './components/product-list-front/product-list-front.component';
import { ProductListTmpComponent } from './components/product-list-tmp/product-list-tmp.component';
import { CaregoryService } from './services/categories.service';
import { CateogryDescriptionPipe } from './pipes/cateogryDescription.pipe';


@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    ProductListAdminComponent,
    ProductEditComponent,
    ProductListFrontComponent,
    ProductListTmpComponent,
    UselessPipe,
    CateogryDescriptionPipe,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'product-list-admin', component: ProductListAdminComponent },
      { path: 'product-list-front', component: ProductListFrontComponent },
      { path: 'product-list-tmp', component: ProductListTmpComponent },
    ])
  ],
  providers: [ProductserviceService, CaregoryService],
  bootstrap: [AppComponent]
})
export class AppModule { }
