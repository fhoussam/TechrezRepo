import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProductListComponent } from './products/product-list/product-list.component';
import { ProductSearchComponent } from './products/product-search/product-search.component';
import { ProductDetailsComponent } from './products/product-details/product-details.component';
import { ProductEditComponent } from './products/product-edit/product-edit.component';
import { SupplierDetailsComponent } from './suppliers/supplier-details/supplier-details.component';
import { SupplierEditComponent } from './suppliers/supplier-edit/supplier-edit.component';

import { AdminRoutingModule } from './admin-routing.module';
import { ProductMainComponent } from './products/product-main/product-main.component';
import { ProductsService } from '../services/products.service';
import { CategoriesService } from '../services/categories.service';
import { SuppliersService } from '../services/suppliers.service';

import { FormsModule } from '@angular/forms';

@NgModule({
  exports: [
    ProductMainComponent,
  ],
  declarations: [
    ProductListComponent,
    ProductSearchComponent,
    ProductDetailsComponent,
    ProductEditComponent,
    SupplierDetailsComponent,
    SupplierEditComponent,
    ProductMainComponent
  ],
  imports: [
    CommonModule,
    AdminRoutingModule,
    FormsModule,
  ],
  providers: [
    ProductsService,
    CategoriesService,
    SuppliersService,
  ]
})
export class AdminModule { }
