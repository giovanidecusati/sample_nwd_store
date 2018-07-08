import { IShoppingCartModel } from '../models/shoppingCartModel';
import { IShoppingCartItemModel } from '../models/shoppingCartItemModel';
import { IProductModel } from '../models/productModel';
import { CartItem } from './item';

export class Cart implements IShoppingCartModel {
  itens: IShoppingCartItemModel[] = new Array<IShoppingCartItemModel>();
  total: number;
  constructor(shoppingCart: IShoppingCartModel) {
    if (shoppingCart != null) {
      this.itens = shoppingCart.itens;
      this.total = shoppingCart.total;
    }
  }

  addItem(product: IProductModel): void {
    let item = this.itens.find(i => i.product.productId == product.productId);

    if (item) item.quantity += 1;
    else this.itens.push(new CartItem(product));
    this.calc();
  }

  updateItem(product: IProductModel, newQuantity: number): void {
    if (newQuantity == 0) {
      this.itens = this.itens.filter(
        i => i.product.productId != product.productId
      );
    } else {
      let item = this.itens.find(i => i.product.productId == product.productId);
      item.changeQuantity(newQuantity);
    }
    this.calc();
  }

  private calc(): void {
    this.total = 0;
    this.itens.forEach(item => {
      this.total += item.total;
    });
  }
}
