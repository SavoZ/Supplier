import { Component, OnInit } from '@angular/core';
import { ShopService } from 'src/app/_services/shop.service';
import { ProductService } from 'src/app/_services/product.service';
import { DropdownModel } from 'src/app/_models/dropdownModel';
import { ShopPostModel } from 'src/app/_models/shop/shopPostModel';

@Component({
  selector: 'app-shop-edit',
  templateUrl: './shop-edit.component.html',
  styleUrls: ['./shop-edit.component.css']
})
export class ShopEditComponent implements OnInit {
  public products: DropdownModel[];
  public model = new ShopPostModel();
  public title = 'New shop';

  constructor(private productService: ProductService, private shopService: ShopService) { }

  ngOnInit() {
    this.productService.getDropdowns().subscribe(result => {
      this.products = result;
    });
  }

  public submit() {
    this.shopService.save(this.model);
  }

}
