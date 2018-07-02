import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-searchbar',
  templateUrl: './searchbar.component.html',
  styleUrls: ['./searchbar.component.css'],
})
export class SearchBarComponent implements OnInit {
  filterBy: string = ';';
  constructor(private _router: Router, private _route: ActivatedRoute) {}

  ngOnInit() {
    this._route.queryParamMap.subscribe(
      params => (this.filterBy = params.get('filterBy'))
    );
  }

  performFilter(filterBy: string): void {
    this._router.navigate(['search'], {
      queryParams: { filterBy: filterBy },
    });
  }
}
