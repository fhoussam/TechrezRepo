import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ProductDetailsComponent } from './products/product-details/product-details.component';
import { SupplierDetailsComponent } from './suppliers/supplier-details/supplier-details.component';
import { ProductEditComponent } from './products/product-edit/product-edit.component';
import { ProductSearchComponent } from './products/product-search/product-search.component';

const routes: Routes = [
  {
    path: 'admin/products',
    component: ProductSearchComponent,
    children: [
      {
        path: ':id',
        redirectTo: ':id/details'
      },
      {
        path: ':id/details',
        component: ProductDetailsComponent
      },
      {
        path: ':id/supplier',
        component: SupplierDetailsComponent
      },
      {
        path: ':id/edit',
        component: ProductEditComponent
      },
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdminRoutingModule { }
