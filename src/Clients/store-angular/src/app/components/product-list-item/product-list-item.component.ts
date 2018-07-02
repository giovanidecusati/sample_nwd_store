import { Component, OnInit, Input } from '@angular/core';
import { IProductModel } from '../../models/productModel';
import { ShoppingCartService } from '../../services/shopping-cart.service';

@Component({
  selector: 'app-product-list-item',
  templateUrl: './product-list-item.component.html',
  styleUrls: ['./product-list-item.component.css'],
})
export class ProductListItemComponent implements OnInit {
  @Input() product: IProductModel;

  constructor(private _cartService: ShoppingCartService) {}

  ngOnInit() {}

  addToCard(product: IProductModel) {
    this._cartService.addItem(product);
  }
}
