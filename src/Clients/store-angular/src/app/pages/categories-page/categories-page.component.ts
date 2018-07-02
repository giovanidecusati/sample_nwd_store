import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ProductService } from '../../services/product.service';
import { CategoryService } from '../../services/category.service';
import { IProductModel } from '../../models/productModel';
import { ICategoryModel } from '../../models/categoryModel';

@Component({
  selector: 'app-categories-page',
  templateUrl: './categories-page.component.html',
  styleUrls: ['./categories-page.component.css'],
})
export class CategoriesPageComponent implements OnInit {
  category: ICategoryModel;
  products: IProductModel[];

  constructor(
    private _route: ActivatedRoute,
    private _categoryService: CategoryService,
    private _productService: ProductService
  ) {}

  ngOnInit() {
    const categoryName = this._route.snapshot.paramMap.get('name');

    this._categoryService
      .getCategoryByName(categoryName)
      .subscribe(category => (this.category = category));

    this._productService
      .getProductByCategoryName(categoryName)
      .subscribe(products => (this.products = products));
  }
}
