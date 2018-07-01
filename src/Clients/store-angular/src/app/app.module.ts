import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

import { Routing, RoutingProviders } from './app.routing';
import { AppComponent } from './app.component';
import { NavbarComponent } from './components/common/navbar/navbar.component';
import { ProductListComponent } from './components/product-list/product-list.component';
import { ProductListItemComponent } from './components/product-list-item/product-list-item.component';
import { FooterComponent } from './components/common/footer/footer.component';
import { SearchbarComponent } from './components/common/searchbar/searchbar.component';
import { TopbarComponent } from './components/common/topbar/topbar.component';
import { HeaderComponent } from './components/common/header/header.component';
import { NotFoundPageComponent } from './pages/notfound-page/notfound-page.component';
import { HomePageComponent } from './pages/home-page/home-page.component';
import { RatingComponent } from './components/common/rating/rating.component';
import { CategoriesPageComponent } from './pages/categories-page/categories-page.component';
import { ProductPageComponent } from './pages/product-page/product-page.component';
import { ShoppingCartPageComponent } from './pages/shoppingcart-page/shoppingcart-page.component';
import { LoginPageComponent } from './pages/login-page/login-page.component';

@NgModule({
  declarations: [
    AppComponent,
    HomePageComponent,
    NavbarComponent,
    ProductListComponent,
    ProductListItemComponent,
    FooterComponent,
    SearchbarComponent,
    TopbarComponent,
    HeaderComponent,
    NotFoundPageComponent,
    RatingComponent,
    CategoriesPageComponent,
    ProductPageComponent,
    ShoppingCartPageComponent,
    LoginPageComponent
  ],
  imports: [BrowserModule, FormsModule, ReactiveFormsModule, HttpClientModule, Routing],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
