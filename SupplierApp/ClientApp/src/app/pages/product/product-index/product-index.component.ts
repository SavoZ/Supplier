import { Component, OnInit } from '@angular/core';
import { ProductService } from 'src/app/_services/product.service';
import { ProductViewModel } from 'src/app/_models/product/productViewModel';

@Component({
  selector: 'app-product-index',
  templateUrl: './product-index.component.html',
  styleUrls: ['./product-index.component.css']
})
export class ProductIndexComponent implements OnInit {

  public products: ProductViewModel[];
  constructor(private service: ProductService) { }

  ngOnInit() {
    this.service.getAll().subscribe(result => {
      this.products = result;
    });
  }

}
