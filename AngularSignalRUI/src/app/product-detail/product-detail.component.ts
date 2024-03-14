import { Component, Input, OnInit } from "@angular/core";
import { Product } from "app/models/product/product.model";

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

  constructor() {}

  ngOnInit() {}
}
