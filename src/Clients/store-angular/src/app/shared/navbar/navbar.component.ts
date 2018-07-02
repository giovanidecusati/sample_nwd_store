import { Component, OnInit } from '@angular/core';
import { CategoryService } from '../../services/category.service';
import { ICategoryModel } from '../../models/categoryModel';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css'],
})
export class NavbarComponent implements OnInit {
  categories: ICategoryModel[];
  constructor(private _categoryService: CategoryService) {}

  ngOnInit() {
    this._categoryService
      .getCategories()
      .subscribe(categories => (this.categories = categories));
  }
}
