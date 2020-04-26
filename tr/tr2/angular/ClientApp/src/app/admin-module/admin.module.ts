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

import { CanDeactivateGuard } from '../guards/can-deactivate';
import { SharedModule } from '../shared-module/shared.module';
import { OrderDetailsService } from '../services/order-details.service';
import { OrderDetailsInfoComponent } from './order-details/order-details-info/order-details-info.component';
import { OrderDetailsEditComponent } from './order-details/order-details-edit/order-details-edit.component';
import { OrderDetailsSearchComponent } from './order-details/order-details-search/order-details-search.component';
import { OrderDetailsListComponent } from './order-details/order-details-list/order-details-list.component';
import { EffectsModule } from '@ngrx/effects';
import { OrderDetailsEffects } from './order-details/order-details-reducer/order-details-effects';

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
    OrderDetailsInfoComponent,
    OrderDetailsEditComponent,
    OrderDetailsSearchComponent,
    OrderDetailsListComponent,
  ],
  imports: [
    CommonModule,
    AdminRoutingModule,
    SharedModule.ForRoot(),
    EffectsModule.forFeature([OrderDetailsEffects]),
  ],
  providers: [
    ProductsService,
    OrderDetailsService,
    CanDeactivateGuard,
  ]
})
export class AdminModule { }
