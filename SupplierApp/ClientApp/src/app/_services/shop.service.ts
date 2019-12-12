import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { API_ENDPOINT } from '../_constants/url.constants';
import { ShopPostModel } from '../_models/shop/shopPostModel';
import { DistributionShopsModel } from '../_models/distributionPostModel';
import { ShopViewModel } from '../_models/shop/shopViewModel';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ShopService {

  constructor(private http: HttpClient) { }

  public getAll(): Observable<Array<ShopViewModel>> {
    return this.http.get<Array<ShopViewModel>>(API_ENDPOINT + 'Shop/GetShops');

  }

  public save(model: ShopPostModel) {
    this.http.post(API_ENDPOINT + 'Shop/SaveShop', model).toPromise();
  }

  public getShopProducts(productId: number) {
    const params = new HttpParams().append('productId', productId.toString());

    return this.http.get<Array<DistributionShopsModel>>(API_ENDPOINT + 'Shop/GetShopsByProduct', { params: params }).toPromise();
  }

  public delete(shopId: number) {
    const params = new HttpParams().append('shopId', shopId.toString());
    this.http.delete(API_ENDPOINT + 'Shop/DeleteShop', {params: params}).toPromise();
  }
}
