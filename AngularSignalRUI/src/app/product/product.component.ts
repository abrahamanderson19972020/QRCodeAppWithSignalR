import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { MatSnackBar } from "@angular/material/snack-bar";
import { CategoryResult } from "app/models/category/categoryResult.model";
import { CreateProduct } from "app/models/product/createProduct.model";
import { Product } from "app/models/product/product.model";
import { ProductWithCategoryName } from "app/models/product/productWithCategoryName.model";
import { CategoryService } from "app/services/category.service";
import { ProductService } from "app/services/product.service";
import { Observable } from "rxjs";

@Component({
  selector: "app-icons",
  templateUrl: "./product.component.html",
  styleUrls: ["./product.component.css"],
})
export class ProductComponent implements OnInit {
  products: Product[] = [];

  constructor(private productService: ProductService) {}

  ngOnInit() {
    this.getProducts();
  }
  getProducts() {
    this.productService.getAllProducst().subscribe((res) => {
      console.log(res);
      this.products = res;
      console.log(this.products);
    });
  }
}
