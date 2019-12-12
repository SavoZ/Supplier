import { Component, OnInit } from '@angular/core';
import { ProductPostModel } from 'src/app/_models/product/productPostModel';
import { ProductService } from 'src/app/_services/product.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-product-edit',
  templateUrl: './product-edit.component.html',
  styleUrls: ['./product-edit.component.css']
})
export class ProductEditComponent implements OnInit {
  public model = new ProductPostModel();
  public limitValue: number = null;
  public title = 'New product';
  constructor(private service: ProductService, private router: Router) { }

  ngOnInit() {
  }

  public addLimit() {
    this.model.limits.push(this.limitValue);
    this.limitValue = null;
  }

  public deleteLimit(index) {
    this.model.limits.splice(index, 1);
  }

  public submit() {
    this.service.save(this.model);
    this.router.navigate(['product']);

  }

}
