import { Component, OnInit, Input } from '@angular/core';
import { IProductModel } from '../../models/productModel';
import { ProductService } from '../../services/product.service';

@Component({
  selector: 'app-home-page',
  templateUrl: './home-page.component.html',
  styleUrls: ['./home-page.component.css'],
})
export class HomePageComponent implements OnInit {
  featured: IProductModel[];
  constructor(private _productService: ProductService) {}

  ngOnInit() {
    this._productService
      .getFeatured()
      .subscribe(products => (this.featured = products));
  }
}
