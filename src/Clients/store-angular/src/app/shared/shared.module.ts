import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavbarComponent } from './navbar/navbar.component';
import { FooterComponent } from './footer/footer.component';
import { SearchBarComponent } from './searchbar/searchbar.component';
import { TopbarComponent } from './topbar/topbar.component';
import { HeaderComponent } from './header/header.component';
import { RouterModule } from '@angular/router';
import { RatingComponent } from './rating/rating.component';

@NgModule({
  imports: [CommonModule, RouterModule],
  declarations: [
    NavbarComponent,
    FooterComponent,
    SearchBarComponent,
    TopbarComponent,
    HeaderComponent,
    RatingComponent,
  ],
  exports: [
    NavbarComponent,
    FooterComponent,
    SearchBarComponent,
    TopbarComponent,
    HeaderComponent,
    RatingComponent,
  ],
})
export class SharedModule {}
