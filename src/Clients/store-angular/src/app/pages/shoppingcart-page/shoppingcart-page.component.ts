import { Component, OnInit } from '@angular/core';
import { ShoppingCartService } from '../../services/shopping-cart.service';
import { IShoppingCartModel } from '../../models/shoppingCartModel';
import { IProductModel } from '../../models/productModel';

@Component({
  selector: 'app-shoppingcart-page',
  templateUrl: './shoppingcart-page.component.html',
  styleUrls: ['./shoppingcart-page.component.css'],
})
export class ShoppingCartPageComponent implements OnInit {
  cart: IShoppingCartModel;
  constructor(private _cartService: ShoppingCartService) {}

  ngOnInit() {
    this.cart = this._cartService.getCart();
  }

  remove(product: IProductModel) {
    this._cartService.updateItem(product, 0);
    this.cart = this._cartService.getCart();
  }

  update(product: IProductModel, newQuantity: number) {
    this._cartService.updateItem(product, newQuantity);
    this.cart = this._cartService.getCart();
  }
}
