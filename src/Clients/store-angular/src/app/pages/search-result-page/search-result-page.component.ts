import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-search-result-page',
  templateUrl: './search-result-page.component.html',
  styleUrls: ['./search-result-page.component.css'],
})
export class SearchResultPageComponent implements OnInit {
  filterBy: string;

  constructor(private route: ActivatedRoute) {
    route.queryParamMap.subscribe(
      params => (this.filterBy = params.get("filterBy"))
    );
  }

  ngOnInit() {}
}
