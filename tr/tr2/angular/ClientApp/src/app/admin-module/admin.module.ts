import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProductDetailsComponent } from './products/product-details/product-details.component';
import { ProductEditComponent } from './products/product-edit/product-edit.component';
import { SupplierDetailsComponent } from './suppliers/supplier-details/supplier-details.component';
import { SupplierEditComponent } from './suppliers/supplier-edit/supplier-edit.component';

import { AdminRoutingModule } from './admin-routing.module';
import { ProductMainComponent } from './products/product-main/product-main.component';
import { ProductsService } from '../services/products.service';
import { CategoriesService } from '../services/categories.service';

import { FormsModule, ReactiveFormsModule } from '@angular/forms';

@NgModule({
  exports: [
    ProductMainComponent,
  ],
  declarations: [
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
    ReactiveFormsModule,
  ],
  providers: [
    ProductsService,
    CategoriesService,
  ]
})
export class AdminModule { }
