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
  products: ProductWithCategoryName[] = [];
  categories: CategoryResult[] = [];
  isAddProductClicked: boolean = false;
  productAddForm: FormGroup | undefined;
  userActionText$: Observable<string>;
  currentProduct: ProductWithCategoryName | undefined;

  constructor(
    private productService: ProductService,
    private categoryService: CategoryService,
    private formBuilder: FormBuilder,
    private snackBar: MatSnackBar
  ) {}

  ngOnInit() {
    this.userActionText$ = this.productService.getUserActionText();
    this.createProductAddForm();
    this.getCategories();
  }
  getCategories() {
    this.categoryService.getAllCategories().subscribe((res) => {
      console.log(res);
      this.categories = res;
    });
  }
  getProducts() {
    this.productService.getAllCategories().subscribe((res) => {
      console.log(res);
      this.products = res;
    });
  }
  addCategorySimulator() {
    this.isAddProductClicked = true;
    this.productService.setUserActionText("ADD");
  }

  addNewCategory() {
    this.isAddProductClicked = false;
    console.log(this.productAddForm?.value);
    this.userActionText$.subscribe((res) => {
      if (res === "ADD") {
        this.productService
          .addProduct(this.productAddForm?.value)
          .subscribe((res) => {
            console.log("res");
            if (res) {
              console.log(res);
              this.snackBar.open(res.message, "Add Product", {
                duration: 3000,
              });
              this.getCategories();
            }
          });
      } else if (res === "UPDATE") {
        this.currentProduct.categoryName =
          this.productAddForm?.get("categoryName")?.value;
        console.log("Updated Category: " + this.currentProduct);
        this.productService
          .updateProduct(this.currentProduct)
          .subscribe((res) => {
            console.log("res");
            if (res) {
              console.log(res);
              this.snackBar.open(res.message, "Update Category", {
                duration: 3000,
              });
              this.getCategories();
            }
          });
      }
    });
  }

  updateCategoryInfo(product: Product) {
    this.isAddCategoryClicked = true;
    this.productService.setUserActionText("UPDATE");
    this.currentProduct = product;
  }
  deleteCategory(product: Product) {
    this.productService.deleteProduct(product.productID).subscribe(
      (res) => {
        console.log(res);
        this.snackBar.open(res.message, "Delete Category", {
          duration: 3000,
        });
        this.getCategories();
        this.productService.setUserActionText("");
      },
      (err) => {
        console.log(err);
      }
    );
  }

  cancelNewProduct() {
    this.isAddProductClicked = false;
  }
  createProductAddForm() {
    this.productAddForm = this.formBuilder.group({
      productName: ["", Validators.required],
      description: ["", Validators.required],
      price: [0, Validators.required],
      imageUrl: [""],
      productStatus: [true, Validators.required],
      categoryID: [0, Validators.required],
    });
  }
}
