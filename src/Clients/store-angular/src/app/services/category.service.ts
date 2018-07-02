import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { ICategoryModel } from '../models/categoryModel';

@Injectable({
  providedIn: 'root',
})
export class CategoryService {
  private _urlBase = './api/categories.json';

  constructor(private _http: HttpClient) {}

  getCategories(): Observable<ICategoryModel[]> {
    return this._http
      .get<ICategoryModel[]>(this._urlBase)
      .pipe(catchError(this.handleError));
  }

  getCategoryByName(categoryName: string): Observable<ICategoryModel> {
    return this._http
      .get<ICategoryModel[]>(this._urlBase)
      .pipe(catchError(this.handleError))
      .pipe(
        map((categories: ICategoryModel[]) =>
          categories.find(c => c.categoryName === categoryName)
        )
      );
  }

  private handleError(err) {
    // in a real world app, we may send the server to some remote logging infrastructure
    // instead of just logging it to the console
    let errorMessage = '';
    if (err.error instanceof Error) {
      // A client-side or network error occurred. Handle it accordingly.
      errorMessage = `An error occurred: ${err.error.message}`;
    } else {
      // The backend returned an unsuccessful response code.
      // The response body may contain clues as to what went wrong,
      errorMessage = `Server returned code: ${err.status}, error message is: ${
        err.message
      }`;
    }
    console.error(errorMessage);
    return throwError(errorMessage);
  }
}
