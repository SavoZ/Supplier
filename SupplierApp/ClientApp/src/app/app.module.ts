import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { NgSelectModule } from '@ng-select/ng-select';
import { ToastrModule } from 'ngx-toastr';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { ShopEditComponent } from './pages/shop/shop-edit/shop-edit.component';
import { ErrorInterceptor } from './_interceptors/error.interceptor';
import { ProductIndexComponent } from './pages/product/product-index/product-index.component';
import { ProductEditComponent } from './pages/product/product-edit/product-edit.component';
import { ShopIndexComponent } from './pages/shop/shop-index/shop-index.component';
import { DistributionIndexComponent } from './pages/distribution/distribution-index/distribution-index.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    ShopEditComponent,
    ProductEditComponent,
    ProductIndexComponent,
    ShopIndexComponent,
    DistributionIndexComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    NgSelectModule,
    ToastrModule.forRoot({
      autoDismiss: true,
      progressBar: true,
      progressAnimation: 'decreasing',
      preventDuplicates: true,
      newestOnTop: true,
      tapToDismiss: true,
      countDuplicates: true
    }),
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      {
        path: 'shop', children: [
          { path: '', component: ShopIndexComponent },
          { path: 'add', component: ShopEditComponent },
          { path: 'edit/:id', component: ShopEditComponent }
        ]
      },
      {
        path: 'product', children: [
          { path: '', component: ProductIndexComponent },
          { path: 'add', component: ProductEditComponent },
          { path: 'edit/:id', component: ProductEditComponent }
        ]
      },
      {
        path: 'distribution', children: [
          { path: '', component: DistributionIndexComponent },
        ]
      },
    ])
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
