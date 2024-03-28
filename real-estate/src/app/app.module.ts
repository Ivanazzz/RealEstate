import { APP_INITIALIZER, NgModule } from "@angular/core";
import { AppComponent } from "./app.component";
import { BrowserModule } from "@angular/platform-browser";
import { AppRoutingModule } from "./app-routing.module";
import { HTTP_INTERCEPTORS, HttpClientModule } from "@angular/common/http";
import { FormsModule } from "@angular/forms";
import { NgbModule } from "@ng-bootstrap/ng-bootstrap";
import { CommonModule } from "@angular/common";
import { ToastrModule } from "ngx-toastr";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { AuthInterceptor } from "./interceptors/auth.interceptor";
import { ErrorInterceptor } from "./interceptors/error.interceptor";
import { UserService } from "../user/services/user.service";
import { NavComponent } from "./root/nav/nav.component";
import { LoginComponent } from "../user/components/login/login.component";
import { RegistrationComponent } from "../user/components/registration/registration.component";
import { AllEstatesComponent } from "../estate/components/all-estates/all-estates.component";
import { EstateService } from "../estate/services/estate.service";
import { SettlementService } from "../settlement/services/settlement.service";
import { AddEstateComponent } from "../estate/components/add-estate/add-estate.component";
import { DetailsEstateComponent } from "../estate/components/details-estate/details-estate.component";
import { InspectionService } from "../inspection/services/inspection.service";
import { AllInspectionsComponent } from "../inspection/components/all-inspections/all-inspections.component";

export function appInitializer(userService: UserService) {
  return () => userService.initializeUser();
}

@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    LoginComponent,
    RegistrationComponent,
    AllEstatesComponent,
    AddEstateComponent,
    DetailsEstateComponent,
    AllInspectionsComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    NgbModule,
    CommonModule,
    ToastrModule.forRoot(),
    BrowserAnimationsModule,
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true,
    },
    {
      provide: APP_INITIALIZER,
      useFactory: appInitializer,
      multi: true,
      deps: [UserService],
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: ErrorInterceptor,
      multi: true,
    },
    UserService,
    EstateService,
    SettlementService,
    InspectionService,
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
