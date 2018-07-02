import { IProductModel } from '../models/productModel';
import { IShoppingCartItemModel } from '../models/shoppingCartItemModel';

export class CartItem implements IShoppingCartItemModel {
  product: IProductModel;
  quantity: number;
  total: number;

  constructor(product: IProductModel) {
    this.product = product;
    this.quantity = 1;
    this.total = product.productPrice;
  }

  changeQuantity(newQuantity: number) {
    this.quantity = newQuantity;
    this.total = this.product.productPrice * this.quantity;
  }
}
