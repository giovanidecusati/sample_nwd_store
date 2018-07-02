import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

import { Routing } from './app.routing';
import { AppComponent } from './app.component';
import { SharedModule } from './shared/shared.module';

import { ProductListComponent } from './components/product-list/product-list.component';
import { ProductListItemComponent } from './components/product-list-item/product-list-item.component';

import { NotFoundPageComponent } from './pages/notfound-page/notfound-page.component';
import { HomePageComponent } from './pages/home-page/home-page.component';
import { CategoriesPageComponent } from './pages/categories-page/categories-page.component';
import { ProductPageComponent } from './pages/product-page/product-page.component';
import { ShoppingCartPageComponent } from './pages/shoppingcart-page/shoppingcart-page.component';
import { LoginPageComponent } from './pages/login-page/login-page.component';
import { SearchResultPageComponent } from './pages/search-result-page/search-result-page.component';

@NgModule({
  declarations: [
    AppComponent,
    HomePageComponent,
    NotFoundPageComponent,
    CategoriesPageComponent,
    ProductPageComponent,
    ShoppingCartPageComponent,
    LoginPageComponent,
    SearchResultPageComponent,
    ProductListComponent,
    ProductListItemComponent,
  ],
  imports: [
    BrowserModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    Routing,
    SharedModule,
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
