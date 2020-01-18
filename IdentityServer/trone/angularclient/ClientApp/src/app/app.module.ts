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


import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { ProductService } from './services/product.service';
import { ProductEventEmitterService } from './services/product-event-emitter.service';
import { AppInitService } from './services/app-init.service';
import { CategoryPipe } from './pipes/category.pipe';

import { FeedComponent } from './components/shared/feed/feed.component';

import { AuthGuardService } from './services/auth-guard.service';
import { AuthService } from './services/auth.service';
import { HomeComponent } from './components/shared/home/home.component';

import { FormsModule } from '@angular/forms';
//import { CustomUrlSerializer } from './helpers/custom-url-serializer';
import { DatePipe } from '@angular/common';

import { StoreModule } from '@ngrx/store';
import { feedReducer } from './Redux/Feed/feed.reducer';

import { ScrollingModule } from '@angular/cdk/scrolling'
import { SignalRService } from './services/signalr.service';
import { CookieService } from 'ngx-cookie-service';
import { MenuComponent } from './components/shared/menu/menu.component';
import { AntiforgeryInterceptorService } from './interceptors/antiforgery-interceptor.service';

export function get_settings(appLoadService: AppInitService) {
    return () => appLoadService.getSettings();
}

const approutes: Routes = [
    {
        path: 'home',
        component: HomeComponent
    },
    {
        path: 'admin/products',
        component: AdminProductsComponent,
        children: [
            {
                path: ':id', redirectTo: ':id/details'
            },
            {
                path: ':id/details', component: EditDetailsComponent
            },
            {
                path: ':id/orders', component: AdminOrdersComponent
            },
        ],
        canActivate: [AuthGuardService],
        data: {
            allowedRoles: ['admin']
        }
    },
    {
        path: 'admin/users',
        component: UsersComponent,
        canActivate: [AuthGuardService],
        data: {
            allowedRoles: ['admin']
        }
    },
    {
        path: 'techrezusers/products',
        component: TechrezUserProductsComponent,
        canActivate: [AuthGuardService],
        data: {
            allowedRoles: ['techrezuser 03']
        }
    },
    {
        path: 'techrezusers/orders',
        component: TechrezuserOrdersComponent,
        canActivate: [AuthGuardService],
        data: {
            allowedRoles: ['techrezuser 03']
        }
    },
    {
        path: 'notfound',
        component: PagenotfoundComponent
    },
    {
        path: '**',
        component: PagenotfoundComponent
    },
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
        HomeComponent,
        MenuComponent
    ],
    imports: [
        BrowserModule,
        AppRoutingModule,
        RouterModule.forRoot(approutes),
        HttpClientModule,
        FormsModule,
        StoreModule.forRoot({ feeds: feedReducer }),
        ScrollingModule,
    ],
    providers: [
        [{ provide: HTTP_INTERCEPTORS, useClass: AntiforgeryInterceptorService, multi: true }],
        ProductService,
        ProductEventEmitterService,
        AppInitService,
        AuthGuardService,
        AuthService,
        { provide: APP_INITIALIZER, useFactory: get_settings, deps: [AppInitService], multi: true },
        //{ provide: UrlSerializer, useClass: CustomUrlSerializer },
        DatePipe,
        SignalRService,
        CookieService,
    ],
    bootstrap: [AppComponent]
})
export class AppModule {
    constructor() {
    }
}
