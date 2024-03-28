import { Component } from "@angular/core";
import { UserRegistrationDto } from "../../dtos/user-registration-dto";
import { UserService } from "../../services/user.service";
import { catchError, throwError } from "rxjs";
import { ToastrService } from "ngx-toastr";
import { Router } from "@angular/router";
import { Role } from "../../enums/role";

@Component({
  selector: "app-registration",
  templateUrl: `./registration.component.html`,
  styleUrl: `./registration.component.css`,
})
export class RegistrationComponent {
  userDto: UserRegistrationDto = new UserRegistrationDto();
  role = Role;

  constructor(
    private userService: UserService,
    private toastr: ToastrService,
    private router: Router
  ) {}

  onSubmit() {
    this.userService
      .register(this.userDto)
      .pipe(
        catchError((err) => {
          return throwError(() => err);
        })
      )
      .subscribe(() => {
        this.toastr.success("Успешна регистрация");
        this.router.navigate(["/login"]);
      });
  }

  isFormValid(): boolean {
    return (
      this.userDto.username != null &&
      this.userDto.password != null &&
      this.userDto.role == this.userDto.role
    );
  }
}
