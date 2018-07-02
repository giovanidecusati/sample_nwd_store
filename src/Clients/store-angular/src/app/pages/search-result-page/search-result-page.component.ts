import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { SearchService } from '../../services/search.service';
import { IProductModel } from '../../models/productModel';

@Component({
  selector: 'app-search-result-page',
  templateUrl: './search-result-page.component.html',
  styleUrls: ['./search-result-page.component.css'],
})
export class SearchResultPageComponent implements OnInit {
  products: IProductModel[];
  constructor(
    private _route: ActivatedRoute,
    private _searchService: SearchService
  ) {}

  ngOnInit() {
    this._route.queryParamMap.subscribe(params =>
      this._searchService
        .performSearch(params.get('filterBy'))
        .subscribe(products => (this.products = products))
    );
  }
}
