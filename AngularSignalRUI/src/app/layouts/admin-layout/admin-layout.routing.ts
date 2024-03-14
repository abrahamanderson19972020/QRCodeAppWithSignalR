import { Routes } from "@angular/router";

import { CategoryComponent } from "../../category/category.component";
import { UserProfileComponent } from "../../user-profile/user-profile.component";
import { TableListComponent } from "../../table-list/table-list.component";
import { TypographyComponent } from "../../typography/typography.component";
import { ProductComponent } from "../../product/product.component";
import { ProductDetailComponent } from "../../product-detail/product-detail.component";
import { NotificationsComponent } from "../../notifications/notifications.component";
import { UpgradeComponent } from "../../upgrade/upgrade.component";

export const AdminLayoutRoutes: Routes = [
  { path: "categories", component: CategoryComponent },
  { path: "about", component: UserProfileComponent },
  { path: "menu", component: TableListComponent },
  { path: "socialmedia", component: TypographyComponent },
  { path: "products", component: ProductComponent },
  { path: "messages", component: ProductDetailComponent },
  { path: "rezervations", component: NotificationsComponent },
];
