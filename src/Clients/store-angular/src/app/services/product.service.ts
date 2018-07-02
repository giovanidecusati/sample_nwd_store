import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { map, catchError } from 'rxjs/operators';
import { IProductModel } from '../models/productModel';

@Injectable({
  providedIn: 'root',
})
export class ProductService {
  private _urlBase = './api/products.json';

  constructor(private _http: HttpClient) {}

  getFeatured(): Observable<IProductModel[]> {
    return this._http
      .get<IProductModel[]>(this._urlBase)
      .pipe(catchError(this.handleError));
  }

  getProductById(id: number): Observable<IProductModel> {
    return this.getFeatured().pipe(
      map((products: IProductModel[]) => products.find(p => p.productId === id))
    );
  }

  getProductByCategoryName(categoryName: string): Observable<IProductModel[]> {
    return this.getFeatured().pipe(
      map((products: IProductModel[]) =>
        products.filter (p => p.categoryName === categoryName)
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
