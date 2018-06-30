import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IProductModel } from '../models/productModel';

const text = `Lorem ipsum dolor sit amet, in nec enim est, nunc ligula viverra fames nulla fermentum ut, aliquam sem cum erat at mus vel, tristique commodo dolor id lobortis. Eu nonummy augue ut orci, lectus non porta sed sed et sodales, metus sit eu dui. Nunc velit amet eget neque pede massa, eget quam et quasi proin orci, luctus lacinia, mattis non sed. Ut dolor sit arcu in nec. Cras feugiat lobortis lacus, sed dictum, quisque purus, dui et mi vehicula lorem praesent mi, nisl ante mollis nulla sapien per. Dolor etiam in ornare, et nonummy sollicitudin. Eget id, feugiat quis eros eu morbi at lacus. Elit vitae curabitur amet varius, vel facere pharetra nunc mattis sodales, nisl dictum aliquam vitae erat, duis at nunc iaculis blandit orci ridiculus. Nulla mauris cras varius maecenas tincidunt, mauris vivamus malesuada pellentesque non. Mollis a quis, pulvinar malesuada elit in ante arcu, hendrerit luctus sed metus dui risus. Mi ullamcorper amet nonummy lectus, venenatis lobortis arcu ullam, est urna ipsum facilisi.`;

@Injectable({
  providedIn: 'root',
})
export class ProductService {
  private _urlBase = 'producs';
  constructor(private _http: HttpClient) {}

  getFeatured(): IProductModel[] {
    const products = new Array();
    for (let index = 0; index < 25; index++) {
      const element = {
        productId: index,
        productName: `Product ${index}`,
        productUrl: `product-${index}`,
        productFrendlyName: text,
        price: 1.99 * index,
        imageUrl: `https://picsum.photos/200/300/?image=${index}`,
        rating: 5,
      };

      products.push(element);
    }

    return products;
  }

  getProductById(id: number): Observable<IProductModel[]> {
    return this._http.get<IProductModel[]>(this._urlBase);
  }
}
