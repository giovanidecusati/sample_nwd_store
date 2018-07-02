import { IShoppingCartItemModel } from './shoppingCartItemModel';
import { IProductModel } from './productModel';

export interface IShoppingCartModel {
  itens: IShoppingCartItemModel[];
  total: number;

  addItem(product: IProductModel): void;
  updateItem(product: IProductModel, newQuantity: number): void;
}
