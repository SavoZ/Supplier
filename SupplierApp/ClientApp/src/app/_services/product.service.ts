import { Injectable } from '@angular/core';
import { ProductPostModel } from '../_models/product/productPostModel';
import { HttpClient } from '@angular/common/http';
import { API_ENDPOINT } from '../_constants/url.constants';
import { DropdownModel } from '../_models/dropdownModel';
import { Observable } from 'rxjs';
import { ProductViewModel } from '../_models/product/productViewModel';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  constructor(private http: HttpClient) { }

  public save(model: ProductPostModel) {
    this.http.post(API_ENDPOINT + 'Product/SaveProduct', model).toPromise();
  }

  public getAll(): Observable<Array<ProductViewModel>> {
    return this.http.get<Array<ProductViewModel>>(API_ENDPOINT + 'Product/GetProducts');
  }

  public getDropdowns(): Observable<Array<DropdownModel>> {
    return this.http.get<Array<DropdownModel>>(API_ENDPOINT + 'Product/GetDropdownProducts');
  }
}
