import { Injectable } from '@angular/core';
import { IProductModel } from '../models/productModel';
import { IShoppingCartModel } from '../models/shoppingCartModel';
import { Cart } from '../domain/cart';

@Injectable({
  providedIn: 'root',
})
export class ShoppingCartService {
  constructor() {}

  addItem(product: IProductModel) {
    let cart = new Cart(JSON.parse(localStorage.getItem('cart')));
    cart.addItem(product);
    localStorage.setItem('cart', JSON.stringify(cart));
  }

  updateItem(product: IProductModel, newQuantity: number) {
    let cart = new Cart(JSON.parse(localStorage.getItem('cart')));
    cart.updateItem(product, newQuantity);
    localStorage.setItem('cart', JSON.stringify(cart));
  }

  getCart(): IShoppingCartModel {
    return JSON.parse(localStorage.getItem('cart'));
  }
}
