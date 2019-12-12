import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { SupplierOrderModel } from '../_models/supplier/supplierOrderModel';
import { API_ENDPOINT } from '../_constants/url.constants';
import { Observable } from 'rxjs';
import { OrderModel } from '../_models/supplier/orderModel';

@Injectable({
  providedIn: 'root'
})
export class SupplierService {

  constructor(private http: HttpClient) { }

  public order(model: SupplierOrderModel): Observable<Array<OrderModel>> {
    let params = new HttpParams();
    params = params.append('productId', model.productId.toString());
    if (model.quantity != null) {
      params = params.append('quantity', model.quantity.toString());
    }
    return this.http.get<Array<OrderModel>>(API_ENDPOINT + 'Supplier/CalculateOrder', { params: params });
  }
}
