import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { CategoryCreate } from "app/models/category/categoryCreate.model";
import { CategoryResult } from "app/models/category/categoryResult.model";
import { GeneralCategoryResult } from "app/models/category/generalCategoryResult.model";
import { BehaviorSubject, Observable } from "rxjs";

@Injectable({
  providedIn: "root",
})
export class CategoryService {
  baseUrl: string = "https://localhost:7193/api/Category";
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

  getAllCategories(): Observable<CategoryResult[]> {
    return this.httpClient.get<CategoryResult[]>(
      this.baseUrl + "/getallcategories"
    );
  }

  addCategory(category: CategoryCreate): Observable<GeneralCategoryResult> {
    return this.httpClient.post<GeneralCategoryResult>(
      this.baseUrl + "/addnewcategory",
      category
    );
  }

  updateCategory(category: CategoryResult): Observable<GeneralCategoryResult> {
    return this.httpClient.put<GeneralCategoryResult>(
      this.baseUrl + "/update",
      category
    );
  }

  deleteCategory(id: number): Observable<GeneralCategoryResult> {
    return this.httpClient.delete<GeneralCategoryResult>(
      this.baseUrl + "/delete/" + id
    );
  }
}
