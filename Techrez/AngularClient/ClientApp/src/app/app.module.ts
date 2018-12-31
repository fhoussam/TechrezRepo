import { BrowserModule } from '@angular/platform-browser';
import { NgModule, APP_INITIALIZER } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ProductlistComponent } from './components/product/productlist/productlist.component';
import { ProductService } from './services/product.service';
import { FormsModule } from '@angular/forms';
import { ProductdetailsComponent } from './components/product/productdetails/productdetails.component';
import { AppLoadService } from './services/appsettings.service';
import { CategoryPipePipe } from './pipes/category-pipe.pipe';
import { ProducteditComponent } from './components/product/productedit/productedit.component';

export function get_settings(appLoadService: AppLoadService) {
  return () => appLoadService.getSettings();
}


@NgModule({
  declarations: [
    AppComponent,
    ProductlistComponent
    ,ProductdetailsComponent
    ,CategoryPipePipe,
    ProducteditComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule
  ],
  providers: [
    ProductService,
    AppLoadService,
    { provide: APP_INITIALIZER, useFactory: get_settings, deps: [AppLoadService], multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule {
  ngOnInit() {
    console.log('app start');
  }
}
