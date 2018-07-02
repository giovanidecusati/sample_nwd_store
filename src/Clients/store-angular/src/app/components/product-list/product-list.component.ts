import { Component, OnInit, Input, OnChanges } from '@angular/core';
import { IProductModel } from '../../models/productModel';
import { ProductService } from '../../services/product.service';
import { SearchService } from '../../services/search.service';

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.css'],
})
export class ProductListComponent implements OnInit, OnChanges {
  @Input() filter: string;

  products: IProductModel[];

  constructor(
    private _productService: ProductService,
    private _searchService: SearchService
  ) {}

  ngOnInit() {
    if (!this.filter)
      this._productService
        .getFeatured()
        .subscribe(products => (this.products = products));
  }

  ngOnChanges(): void {
    this._searchService
      .performSearch(this.filter)
      .subscribe(products => (this.products = products));
  }
}
