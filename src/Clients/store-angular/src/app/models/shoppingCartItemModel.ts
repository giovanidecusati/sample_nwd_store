import { IProductModel } from './productModel';

export interface IShoppingCartItemModel {
  product: IProductModel;
  quantity: number;
  total: number;

  changeQuantity(newQuantity: number);
}
