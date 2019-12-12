import { Component, OnInit } from '@angular/core';
import { ProductService } from '../_services/product.service';
import { DropdownModel } from '../_models/dropdownModel';
import { SupplierOrderModel } from '../_models/supplier/supplierOrderModel';
import { SupplierService } from '../_services/supplier.service';
import { OrderModel } from '../_models/supplier/orderModel';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit {
  public products: DropdownModel[];
  public model = new SupplierOrderModel();
  public shops: OrderModel[];

  constructor(private productService: ProductService, private supplierServide: SupplierService) {

  }
  ngOnInit() {
    this.productService.getDropdowns().subscribe(result => {
      this.products = result;
    });
  }

  public submit() {
    this.supplierServide.order(this.model).subscribe(result => {
      this.shops = result;
    });
  }

}
