import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-searchbar',
  templateUrl: './searchbar.component.html',
  styleUrls: ['./searchbar.component.css'],
})
export class SearchBarComponent implements OnInit {
  constructor(private _router: Router) {}

  ngOnInit() {}

  performFilter(filterBy: string): void {
    this._router.navigate(['search'], {
      queryParams: { filterBy: filterBy },
    });
  }
}
