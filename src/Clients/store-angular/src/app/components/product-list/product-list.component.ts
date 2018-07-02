import { Component, OnInit, Input, OnChanges } from '@angular/core';
import { IProductModel } from '../../models/productModel';

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.css'],
})
export class ProductListComponent implements OnInit {
  @Input() products: IProductModel[];

  constructor() {}

  ngOnInit() {}
}
