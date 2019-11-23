import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ProductsComponent as AdminProductsComponent } from './components/admin/products/products.component';
import { ProductsComponent as TechrezUserProductsComponent } from './components/techrezuser/products/products.component';
import { UsersComponent } from './components/admin/users/users.component';
import { OrdersComponent } from './components/techrezuser/orders/orders.component';
import { Routes, RouterModule } from '@angular/router';

const approutes: Routes = [
    { path: 'admin/products', component: AdminProductsComponent },
    { path: '', component: AdminProductsComponent },
    { path: 'admin/users', component: UsersComponent },
    { path: 'techrezusers/products', component: TechrezUserProductsComponent },
    { path: 'techrezusers/orders', component: OrdersComponent },
];

@NgModule({
    declarations: [
        AppComponent,
        AdminProductsComponent,
        TechrezUserProductsComponent,
        UsersComponent,
        OrdersComponent
    ],
    imports: [
        BrowserModule,
        AppRoutingModule,
        RouterModule.forRoot(approutes)
    ],
    providers: [],
    bootstrap: [AppComponent]
})
export class AppModule { }
