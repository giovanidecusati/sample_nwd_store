import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-categories-page',
  templateUrl: './categories-page.component.html',
  styleUrls: ['./categories-page.component.css'],
})
export class CategoriesPageComponent implements OnInit {
  currentCategory: string = '';

  constructor(private _route: ActivatedRoute, private _router: Router) {}

  ngOnInit() {
    this.currentCategory = this._route.snapshot.paramMap.get('name');
  }

  back(): void {
    this._router.navigate(['/products']);
  }
}
