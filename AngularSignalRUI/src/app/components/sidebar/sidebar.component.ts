import { Component, OnInit } from "@angular/core";

declare const $: any;
declare interface RouteInfo {
  path: string;
  title: string;
  icon: string;
  class: string;
}
export const ROUTES: RouteInfo[] = [
  { path: "/categories", title: "Categories", icon: "dashboard", class: "" },
  { path: "/products", title: "Products", icon: "bubble_chart", class: "" },
  { path: "/about", title: "About", icon: "book", class: "" },
  { path: "/menu", title: "Menu", icon: "content_paste", class: "" },
  {
    path: "/socialmedia",
    title: "socialmedia",
    icon: "library_books",
    class: "",
  },
  { path: "/messages", title: "Messages", icon: "message", class: "" },
  {
    path: "/rezervations",
    title: "Rezervations",
    icon: "notifications",
    class: "",
  },
];

@Component({
  selector: "app-sidebar",
  templateUrl: "./sidebar.component.html",
  styleUrls: ["./sidebar.component.css"],
})
export class SidebarComponent implements OnInit {
  menuItems: any[];

  constructor() {}

  ngOnInit() {
    this.menuItems = ROUTES.filter((menuItem) => menuItem);
  }
  isMobileMenu() {
    if ($(window).width() > 991) {
      return false;
    }
    return true;
  }
}
