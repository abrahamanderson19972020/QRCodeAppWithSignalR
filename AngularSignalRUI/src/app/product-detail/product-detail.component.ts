import { Component, EventEmitter, Input, OnInit, Output } from "@angular/core";
import { MatSnackBar } from "@angular/material/snack-bar";
import { Product } from "app/models/product/product.model";
import { ProductService } from "app/services/product.service";

declare const google: any;

interface Marker {
  lat: number;
  lng: number;
  label?: string;
  draggable?: boolean;
}
@Component({
  selector: "app-product-detail",
  templateUrl: "./product-detail.component.html",
  styleUrls: ["./product-detail.component.css"],
})
export class ProductDetailComponent implements OnInit {
  @Input() product: Product | undefined;
  @Output() changeProduct: EventEmitter<boolean> = new EventEmitter<boolean>();

  constructor(
    private productService: ProductService,
    private snackBar: MatSnackBar
  ) {}

  ngOnInit() {}

  updateProduct(product) {
    this.productService.updateProduct(product).subscribe((res) => {
      this.snackBar.open(res.message, "Update", {
        duration: 3000,
      });
      this.changeProduct.emit(true);
    });
  }

  deleteProduct(product: Product) {
    this.productService.deleteProduct(product.productID).subscribe((res) => {
      this.snackBar.open(res.message, "Delete", {
        duration: 3000,
      });
      this.changeProduct.emit(true);
    });
  }
}
