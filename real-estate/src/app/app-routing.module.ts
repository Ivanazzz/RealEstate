import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { LoginComponent } from "../user/components/login/login.component";
import { RegistrationComponent } from "../user/components/registration/registration.component";
import { AllEstatesComponent } from "../estate/components/all-estates/all-estates.component";
import { AddEstateComponent } from "../estate/components/add-estate/add-estate.component";
import { DetailsEstateComponent } from "../estate/components/details-estate/details-estate.component";
import { AllInspectionsComponent } from "../inspection/components/all-inspections/all-inspections.component";

const routes: Routes = [
  {
    path: "login",
    component: LoginComponent,
  },
  {
    path: "registration",
    component: RegistrationComponent,
  },
  {
    path: "",
    component: AllEstatesComponent,
  },
  {
    path: "addEstate",
    component: AddEstateComponent,
  },
  {
    path: "detailsEstate/:id",
    component: DetailsEstateComponent,
  },
  {
    path: "inspections",
    component: AllInspectionsComponent,
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
