import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ProductMainComponent } from './products/product-main/product-main.component';
import { ProductDetailsComponent } from './products/product-details/product-details.component';
import { SupplierDetailsComponent } from './suppliers/supplier-details/supplier-details.component';
import { ProductEditComponent } from './products/product-edit/product-edit.component';

const routes: Routes = [
  {
    path: 'admin/products',
    component: ProductMainComponent,
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
