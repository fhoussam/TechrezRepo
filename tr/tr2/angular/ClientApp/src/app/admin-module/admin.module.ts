import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProductDetailsComponent } from './products/product-details/product-details.component';
import { ProductEditComponent } from './products/product-edit/product-edit.component';
import { SupplierDetailsComponent } from './suppliers/supplier-details/supplier-details.component';
import { SupplierEditComponent } from './suppliers/supplier-edit/supplier-edit.component';

import { AdminRoutingModule } from './admin-routing.module';
import { ProductSearchComponent } from './products/product-search/product-search.component';
import { ProductsService } from '../services/products.service';
import { ProductListComponent } from './products/product-list/product-list.component';

import { SuppliersService } from '../services/suppliers.service';
import { CanDeactivateGuard } from '../guards/can-deactivate';
import { SharedModule } from '../shared-module/shared.module';

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
    ProductListComponent,
  ],
  imports: [
    CommonModule,
    AdminRoutingModule,
    SharedModule.ForRoot(),
  ],
  providers: [
    SuppliersService,
    ProductsService,
    CanDeactivateGuard,
  ]
})
export class AdminModule { }
