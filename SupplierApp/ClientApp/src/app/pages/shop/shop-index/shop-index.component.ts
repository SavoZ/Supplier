import { Component, OnInit } from '@angular/core';
import { ShopService } from 'src/app/_services/shop.service';
import { ShopViewModel } from 'src/app/_models/shop/shopViewModel';

@Component({
  selector: 'app-shop-index',
  templateUrl: './shop-index.component.html',
  styleUrls: ['./shop-index.component.css']
})
export class ShopIndexComponent implements OnInit {
  public shops: ShopViewModel[];

  constructor(private service: ShopService) { }

  ngOnInit() {
    this.getShops();
  }

  public getShops() {
    this.service.getAll().subscribe(result =>
      this.shops = result);
  }

  public delete(shop: ShopViewModel, index: number) {
    this.service.delete(shop.id);
    this.shops.splice(index, 1);
  }

}
