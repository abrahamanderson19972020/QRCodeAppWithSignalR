import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { CategoryCreate } from "app/models/category/categoryCreate.model";
import { CategoryResult } from "app/models/category/categoryResult.model";
import { Observable } from "rxjs";

@Injectable({
  providedIn: "root",
})
export class CategoryService {
  baseUrl: string = "https://localhost:7193/api/Category";
  constructor(private httpClient: HttpClient) {}

  getAllCategories(): Observable<CategoryResult[]> {
    return this.httpClient.get<CategoryResult[]>(
      this.baseUrl + "/getallcategories"
    );
  }

  addCategory(category: CategoryCreate): Observable<string> {
    return this.httpClient.post<string>(
      this.baseUrl + "/addnewcategory",
      category
    );
  }
}
