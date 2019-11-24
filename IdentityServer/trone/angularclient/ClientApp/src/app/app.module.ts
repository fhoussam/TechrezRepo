import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ProductsComponent as AdminProductsComponent } from './components/admin/products/products.component';
import { ProductsComponent as TechrezUserProductsComponent } from './components/techrezuser/products/products.component';
import { UsersComponent } from './components/admin/users/users.component';
import { OrdersComponent } from './components/techrezuser/orders/orders.component';
import { Routes, RouterModule } from '@angular/router';
import { SearchComponent } from './components/admin/products/search/search.component';
import { ListComponent } from './components/admin/products/list/list.component';
import { ExploreComponent } from './components/admin/products/explore/explore.component';
import { PagenotfoundComponent } from './components/shared/pagenotfound/pagenotfound.component';

import { HttpClientModule } from '@angular/common/http'
import { ProductService } from './services/product.service';
import { DetailsComponent } from './components/admin/products/explore/details/details.component';

const approutes: Routes = [
    { path: 'admin/products', component: AdminProductsComponent },
    { path: 'admin/users', component: UsersComponent },
    { path: 'techrezusers/products', component: TechrezUserProductsComponent },
    { path: 'techrezusers/orders', component: OrdersComponent },
    //{ path: '**', component: PagenotfoundComponent },
    { path: '**', component: AdminProductsComponent },
    { path: '', component: AdminProductsComponent },
];

@NgModule({
    declarations: [
        AppComponent,
        AdminProductsComponent,
        TechrezUserProductsComponent,
        UsersComponent,
        OrdersComponent,
        SearchComponent,
        ListComponent,
        ExploreComponent,
        PagenotfoundComponent,
        DetailsComponent
    ],
    imports: [
        BrowserModule,
        AppRoutingModule,
        RouterModule.forRoot(approutes),
        HttpClientModule
    ],
    providers: [
        ProductService
    ],
    bootstrap: [AppComponent]
})
export class AppModule { }
