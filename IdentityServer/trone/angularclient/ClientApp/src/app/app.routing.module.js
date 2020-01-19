//import { NgModule } from '@angular/core';
//import { Routes, RouterModule } from '@angular/router';
//import { AppComponent } from './app.component';
//import { ProductsComponent as AdminProductsComponent } from './components/admin/products/products.component';
//import { ProductsComponent as TechrezUserProductsComponent } from './components/techrezuser/products/products.component';
//import { UsersComponent } from './components/admin/users/users.component';
//import { OrdersComponent as TechrezuserOrdersComponent } from './components/techrezuser/orders/orders.component';
//import { OrdersComponent as AdminOrdersComponent } from './components/admin/products/orders/orders.component';
//import { EditDetailsComponent } from './components/admin/products/editDetails/editDetails.component';
//import { SearchComponent } from './components/admin/products/search/search.component';
//import { ListComponent } from './components/admin/products/list/list.component';
//import { PagenotfoundComponent } from './components/shared/pagenotfound/pagenotfound.component';
//import { CanDeactivateGuard } from './services/can-deactivate-guard-service';
//import { HomeComponent } from './components/shared/home/home.component';
//import { AuthGuardService } from './services/auth-guard.service';
//const approutes: Routes = [
//    {
//        path: 'home',
//        component: HomeComponent
//    },
//    {
//        path: 'admin/products',
//        component: AdminProductsComponent,
//        children: [
//            {
//                path: ':id', redirectTo: ':id/details'
//            },
//            {
//                path: ':id/details', component: EditDetailsComponent, canDeactivate: [CanDeactivateGuard]
//            },
//            {
//                path: ':id/orders', component: AdminOrdersComponent
//            },
//        ],
//        canActivate: [AuthGuardService],
//        data: {
//            allowedRoles: ['admin']
//        }
//    },
//    {
//        path: 'admin/users',
//        component: UsersComponent,
//        canActivate: [AuthGuardService],
//        data: {
//            allowedRoles: ['admin']
//        }
//    },
//    {
//        path: 'techrezusers/products',
//        component: TechrezUserProductsComponent,
//        canActivate: [AuthGuardService],
//        data: {
//            allowedRoles: ['techrezuser 03']
//        }
//    },
//    {
//        path: 'techrezusers/orders',
//        component: TechrezuserOrdersComponent,
//        canActivate: [AuthGuardService],
//        data: {
//            allowedRoles: ['techrezuser 03']
//        }
//    },
//    {
//        path: 'notfound',
//        component: PagenotfoundComponent
//    },
//    {
//        path: '**',
//        component: PagenotfoundComponent
//    },
//];
//@NgModule({
//    imports: [
//        RouterModule.forRoot(approutes),
//    ],
//    exports: [RouterModule]
//})
//export class AppRoutingModule {
//}
//# sourceMappingURL=app.routing.module.js.map