import { BrowserModule } from '@angular/platform-browser';
import { NgModule, APP_INITIALIZER } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ProductsComponent as AdminProductsComponent } from './components/admin/products/products.component';
import { ProductsComponent as TechrezUserProductsComponent } from './components/techrezuser/products/products.component';
import { UsersComponent } from './components/admin/users/users.component';
import { OrdersComponent as TechrezuserOrdersComponent } from './components/techrezuser/orders/orders.component';
import { OrdersComponent as AdminOrdersComponent } from './components/admin/products/explore/orders/orders.component';
import { Routes, RouterModule } from '@angular/router';
import { SearchComponent } from './components/admin/products/search/search.component';
import { ListComponent } from './components/admin/products/list/list.component';
import { ExploreComponent } from './components/admin/products/explore/explore.component';
import { PagenotfoundComponent } from './components/shared/pagenotfound/pagenotfound.component';

import { HttpClientModule } from '@angular/common/http'
import { ProductService } from './services/product.service';
import { DetailsComponent } from './components/admin/products/explore/details/details.component';
import { ProductEventEmitterService } from './services/product-event-emitter.service';
import { AppInitService } from './services/app-init.service';
import { CategoryPipe } from './pipes/category.pipe';

import { IAppState, rootReducer, INITIAL_STATE } from './models/appState';
import { NgReduxModule, NgRedux } from '@angular-redux/store';
import { FeedComponent } from './components/shared/feed/feed.component';


export function get_settings(appLoadService: AppInitService) {
    return () => appLoadService.getSettings();
}

const approutes: Routes = [
    {
        path: 'admin/products',
        component: AdminProductsComponent,
        children: [
            //product route tree
            { path: '', component: DetailsComponent },
            { path: 'explore/details/:id', component: DetailsComponent },
            { path: 'explore/orders/:id', component: AdminOrdersComponent },
        ]
    },
    { path: 'admin/users', component: UsersComponent },
    { path: 'techrezusers/products', component: TechrezUserProductsComponent },
    { path: 'techrezusers/orders', component: TechrezuserOrdersComponent },
    //{ path: '', component: AdminProductsComponent },
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
        ExploreComponent,
        PagenotfoundComponent,
        DetailsComponent,
        CategoryPipe,
        FeedComponent
    ],
    imports: [
        BrowserModule,
        AppRoutingModule,
        RouterModule.forRoot(approutes),
        HttpClientModule,
        NgReduxModule,
    ],
    providers: [
        ProductService,
        ProductEventEmitterService,
        AppInitService,
        { provide: APP_INITIALIZER, useFactory: get_settings, deps: [AppInitService], multi: true }
    ],
    bootstrap: [AppComponent]
})
export class AppModule {
    constructor(ngRedux: NgRedux<IAppState>) {
        ngRedux.configureStore(rootReducer, INITIAL_STATE)
    }
}
