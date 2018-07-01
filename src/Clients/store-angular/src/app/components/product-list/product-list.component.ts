import { Component, OnInit, Output } from '@angular/core';
import { IProductModel } from '../../models/productModel';
import { ProductService } from '../../services/product.service';

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.css'],
})
export class ProductListComponent implements OnInit {
  @Output() products: IProductModel[];

  constructor(private _productService: ProductService) {}

  ngOnInit() {
    this._productService
      .getFeatured()
      .subscribe(products => (this.products = products));
  }
}
