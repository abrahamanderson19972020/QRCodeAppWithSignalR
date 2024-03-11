import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { CategoryResult } from "./../models/category/categoryResult.model";
import { Component, ElementRef, OnInit, ViewChild } from "@angular/core";
import { CategoryService } from "app/services/category.service";
import * as Chartist from "chartist";
import { BehaviorSubject, Observable } from "rxjs";
import { CategoryCreate } from "app/models/category/categoryCreate.model";
import { MatSnackBar } from "@angular/material/snack-bar";

@Component({
  selector: "app-dashboard",
  templateUrl: "./dashboard.component.html",
  styleUrls: ["./dashboard.component.css"],
})
export class DashboardComponent implements OnInit {
  categories: CategoryResult[] = [];
  isAddCategoryClicked: boolean = false;
  categoryAddForm: FormGroup | undefined;
  userActionText$: Observable<string>;
  currentCategory: CategoryResult | undefined;

  constructor(
    private categoryService: CategoryService,
    private formBuilder: FormBuilder,
    private snackBar: MatSnackBar
  ) {}

  ngOnInit() {
    this.userActionText$ = this.categoryService.getUserActionText();
    this.createCategoryAddForm();
    this.getCategories();
  }
  getCategories() {
    this.categoryService.getAllCategories().subscribe((res) => {
      console.log(res);
      this.categories = res;
    });
  }
  addCategorySimulator() {
    this.isAddCategoryClicked = true;
    this.categoryService.setUserActionText("ADD");
  }

  addNewCategory() {
    this.isAddCategoryClicked = false;
    console.log(this.categoryAddForm?.value);
    this.userActionText$.subscribe((res) => {
      if (res === "ADD") {
        this.categoryService
          .addCategory(this.categoryAddForm?.value)
          .subscribe((res) => {
            console.log("res");
            if (res) {
              console.log(res);
              this.snackBar.open(res.message, "Add Category", {
                duration: 3000,
              });
              this.getCategories();
            }
          });
      } else if (res === "UPDATE") {
        this.currentCategory.categoryName =
          this.categoryAddForm?.get("categoryName")?.value;
        console.log("Updated Category: " + this.currentCategory);
        this.categoryService
          .updateCategory(this.currentCategory)
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

  updateCategoryInfo(category: CategoryResult) {
    this.isAddCategoryClicked = true;
    this.categoryService.setUserActionText("UPDATE");
    this.currentCategory = category;
  }
  deleteCategory(category: CategoryResult) {
    this.categoryService.deleteCategory(category.categoryID).subscribe(
      (res) => {
        console.log(res);
        this.snackBar.open(res.message, "Delete Category", {
          duration: 3000,
        });
        this.getCategories();
        this.categoryService.setUserActionText("");
      },
      (err) => {
        console.log(err);
      }
    );
  }

  cancelNewCategory() {
    this.isAddCategoryClicked = false;
  }
  createCategoryAddForm() {
    this.categoryAddForm = this.formBuilder.group({
      categoryName: ["", Validators.required],
      status: [true],
    });
  }
}
