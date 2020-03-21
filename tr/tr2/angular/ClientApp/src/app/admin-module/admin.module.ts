import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProductDetailsComponent } from './products/product-details/product-details.component';
import { ProductEditComponent } from './products/product-edit/product-edit.component';
import { SupplierDetailsComponent } from './suppliers/supplier-details/supplier-details.component';
import { SupplierEditComponent } from './suppliers/supplier-edit/supplier-edit.component';

import { AdminRoutingModule } from './admin-routing.module';
import { ProductSearchComponent } from './products/product-search/product-search.component';
import { ProductsService } from '../services/products.service';
import { CategoriesService } from '../services/categories.service';

import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ProductListComponent } from './products/product-list/product-list.component';

@NgModule({
  exports: [
    ProductSearchComponent,
  ],
  declarations: [
    ProductDetailsComponent,
    ProductEditComponent,
    SupplierDetailsComponent,
    SupplierEditComponent,
    ProductSearchComponent,
    ProductListComponent
  ],
  imports: [
    CommonModule,
    AdminRoutingModule,
    FormsModule,
    ReactiveFormsModule,
  ],
  providers: [
    ProductsService,
    CategoriesService,
  ]
})
export class AdminModule { }
