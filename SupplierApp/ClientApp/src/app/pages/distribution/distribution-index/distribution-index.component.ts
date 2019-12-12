import { Component, OnInit } from '@angular/core';
import { DropdownModel } from 'src/app/_models/dropdownModel';
import { DistributionShopsModel, DistributionModel } from 'src/app/_models/distributionPostModel';
import { ProductService } from 'src/app/_services/product.service';
import { ShopService } from 'src/app/_services/shop.service';
import { DistributionService } from 'src/app/_services/distribution.service';

@Component({
  selector: 'app-distribution-index',
  templateUrl: './distribution-index.component.html',
  styleUrls: ['./distribution-index.component.css']
})
export class DistributionIndexComponent implements OnInit {
  public products: DropdownModel[];
  public productId: number;
  public shops: Array<DistributionShopsModel>;
  public model = new DistributionModel();

  constructor(private productService: ProductService,
    private shopService: ShopService,
    private service: DistributionService) { }

  ngOnInit() {
    this.productService.getDropdowns().subscribe(result => {
      this.products = result;
    });
  }

  public onProductChange() {
    this.shopService.getShopProducts(+this.model.productId).then(result => {
      this.shops = result;
    });
  }

  public submit() {
    this.model.shops.push(...this.shops);
    this.service.save(this.model);
    this.model.shops.length = 0;
  }

}
