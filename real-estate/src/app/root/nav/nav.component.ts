import { Component } from "@angular/core";
import { UserService } from "../../../user/services/user.service";

@Component({
  selector: "app-nav",
  templateUrl: "./nav.component.html",
  styleUrl: "./nav.component.css",
})
export class NavComponent {
  constructor(public userService: UserService) {}

  logout(): void {
    this.userService.logout();
  }
}
