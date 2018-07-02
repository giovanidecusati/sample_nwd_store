import { ModuleWithProviders } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { AuthGuardService } from './services/auth-guard.service';

import { HomePageComponent } from './pages/home-page/home-page.component';
import { NotFoundPageComponent } from './pages/notfound-page/notfound-page.component';
import { CategoriesPageComponent } from './pages/categories-page/categories-page.component';
import { ProductPageComponent } from './pages/product-page/product-page.component';
import { ShoppingCartPageComponent } from './pages/shoppingcart-page/shoppingcart-page.component';
import { LoginPageComponent } from './pages/login-page/login-page.component';
import { SearchResultPageComponent } from './pages/search-result-page/search-result-page.component';

const appRoutes: Routes = [
  { path: '', component: HomePageComponent, pathMatch: 'full' },
  { path: 'search', component: SearchResultPageComponent },
  { path: 'categories', component: CategoriesPageComponent },
  { path: 'categories/:uri', component: CategoriesPageComponent },
  { path: 'products', component: ProductPageComponent },
  { path: 'products/:uri', component: ProductPageComponent },
  {
    path: 'shopping-cart',
    component: ShoppingCartPageComponent,
    canActivate: [AuthGuardService],
  },
  { path: 'login', component: LoginPageComponent },
  { path: 'not-found', component: NotFoundPageComponent },
  { path: '**', redirectTo: 'not-found', pathMatch: 'full' },
];

export const RoutingProviders: any[] = [];
export const Routing: ModuleWithProviders = RouterModule.forRoot(appRoutes);
