import { Component, OnInit, Input } from '@angular/core';
import { IProductModel } from '../../models/productModel';

@Component({
  selector: 'app-product-list-item',
  templateUrl: './product-list-item.component.html',
  styleUrls: ['./product-list-item.component.css'],
})
export class ProductListItemComponent implements OnInit {
  
  @Input() product: IProductModel;

  constructor() {}

  ngOnInit() {}
}
