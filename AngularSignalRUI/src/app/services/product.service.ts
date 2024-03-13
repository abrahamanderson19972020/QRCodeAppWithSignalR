import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { CreateProduct } from "app/models/product/createProduct.model";
import { GeneralProductResult } from "app/models/product/generalProductResult.model";
import { Product } from "app/models/product/product.model";
import { ProductWithCategoryName } from "app/models/product/productWithCategoryName.model";
import { BehaviorSubject, Observable } from "rxjs";

@Injectable({
  providedIn: "root",
})
export class ProductService {
  baseUrl: string = "https://localhost:7193/api/Product/";
  private userActionButtonText: BehaviorSubject<string> =
    new BehaviorSubject<string>("");
  private userActionButtonText$ = this.userActionButtonText.asObservable();

  constructor(private httpClient: HttpClient) {}

  getUserActionText(): Observable<string> {
    return this.userActionButtonText$;
  }

  setUserActionText(value: string) {
    this.userActionButtonText.next(value);
  }

  getAllCategories(): Observable<ProductWithCategoryName[]> {
    return this.httpClient.get<ProductWithCategoryName[]>(
      this.baseUrl + "getproductswithcategoryname"
    );
  }

  addProduct(product: CreateProduct): Observable<GeneralProductResult> {
    return this.httpClient.post<GeneralProductResult>(
      this.baseUrl + "addnewproduct",
      product
    );
  }

  updateProduct(product: Product): Observable<GeneralProductResult> {
    return this.httpClient.put<GeneralProductResult>(
      this.baseUrl + "update",
      product
    );
  }
  deleteProduct(id: number): Observable<GeneralProductResult> {
    return this.httpClient.delete<GeneralProductResult>(
      this.baseUrl + "delete/" + id
    );
  }
}
