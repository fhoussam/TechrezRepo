import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from '../components/shared/home/home.component';
import { EditDetailsComponent } from '../components/admin/products/editDetails/editDetails.component';
import { CanDeactivateGuard } from '../services/can-deactivate-guard-service';
import { AuthGuardService } from '../services/auth-guard.service';
import { PagenotfoundComponent } from '../components/shared/pagenotfound/pagenotfound.component';
import { ProductsComponent as AdminProductsComponent } from '../components/admin/products/products.component';
import { ProductsComponent as TechrezUserProductsComponent } from '../components/admin/products/products.component';
import { OrdersComponent as AdminOrdersComponent} from '../components/techrezuser/orders/orders.component';
import { UsersComponent } from '../components/admin/users/users.component';


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
                path: ':id/details', component: EditDetailsComponent, canDeactivate: [CanDeactivateGuard]
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
        component: TechrezUserProductsComponent,
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
    imports: [RouterModule.forChild(approutes)],
  exports: [RouterModule]
})
export class MyRoutingModuleRoutingModule { }
