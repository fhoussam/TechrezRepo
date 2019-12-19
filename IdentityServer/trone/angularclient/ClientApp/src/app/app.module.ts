import { BrowserModule } from '@angular/platform-browser';
import { NgModule, APP_INITIALIZER } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ProductsComponent as AdminProductsComponent } from './components/admin/products/products.component';
import { ProductsComponent as TechrezUserProductsComponent } from './components/techrezuser/products/products.component';
import { UsersComponent } from './components/admin/users/users.component';
import { OrdersComponent as TechrezuserOrdersComponent } from './components/techrezuser/orders/orders.component';
import { OrdersComponent as AdminOrdersComponent } from './components/admin/products/orders/orders.component';
import { EditDetailsComponent } from './components/admin/products/editDetails/editDetails.component';
import { Routes, RouterModule, UrlSerializer } from '@angular/router';
import { SearchComponent } from './components/admin/products/search/search.component';
import { ListComponent } from './components/admin/products/list/list.component';
import { PagenotfoundComponent } from './components/shared/pagenotfound/pagenotfound.component';


import { HttpClientModule } from '@angular/common/http'
import { ProductService } from './services/product.service';
import { ProductEventEmitterService } from './services/product-event-emitter.service';
import { AppInitService } from './services/app-init.service';
import { CategoryPipe } from './pipes/category.pipe';

import { FeedComponent } from './components/shared/feed/feed.component';

import { AuthGuardService } from './services/auth-guard.service';
import { AuthService } from './services/auth.service';
import { HomeComponent } from './components/shared/home/home.component';

import { FormsModule } from '@angular/forms';
import { CustomUrlSerializer } from './helpers/custom-url-serializer';
import { DatePipe } from '@angular/common';

export function get_settings(appLoadService: AppInitService) {
    return () => appLoadService.getSettings();
}

const approutes: Routes = [
    { path: 'home', component: HomeComponent },
    //{ path: '', component: HomeComponent },
    {
        path: 'admin/products',
        component: AdminProductsComponent,
        children: [
            { path: 'details', component: EditDetailsComponent },
            { path: 'orders', component: AdminOrdersComponent },
        ]
    },
    {
        path: 'admin/users',
        component: UsersComponent,
        canActivate: [AuthGuardService]
    },
    { path: 'techrezusers/products', component: TechrezUserProductsComponent },
    { path: 'techrezusers/orders', component: TechrezuserOrdersComponent },
    //{ path: '**', component: PagenotfoundComponent },
];

@NgModule({
    declarations: [
        AppComponent,
        AdminProductsComponent,
        TechrezUserProductsComponent,
        UsersComponent,
        TechrezuserOrdersComponent,
        AdminOrdersComponent,
        SearchComponent,
        ListComponent,
        PagenotfoundComponent,
        EditDetailsComponent,
        CategoryPipe,
        FeedComponent,
        HomeComponent
    ],
    imports: [
        BrowserModule,
        AppRoutingModule,
        RouterModule.forRoot(approutes),
        HttpClientModule,
        FormsModule,
    ],
    providers: [
        ProductService,
        ProductEventEmitterService,
        AppInitService,
        AuthGuardService,
        AuthService,
        { provide: APP_INITIALIZER, useFactory: get_settings, deps: [AppInitService], multi: true },
        { provide: UrlSerializer, useClass: CustomUrlSerializer },
        DatePipe,
    ],
    bootstrap: [AppComponent]
})
export class AppModule {
    constructor() {
    }
}
