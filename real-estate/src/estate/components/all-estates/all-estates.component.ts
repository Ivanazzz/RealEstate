import { Component } from "@angular/core";
import { EstateService } from "../../services/estate.service";
import { catchError, throwError } from "rxjs";
import { EstateGetDto } from "../../dtos/estate-get-dto";
import { EstateTypeLocalization } from "../../enums/estate-type";
import { EstateFilterDto } from "../../dtos/estate-filter-dto";
import { SettlementGetDto } from "../../../settlement/dtos/settlement-get-dto";
import { SettlementService } from "../../../settlement/services/settlement.service";
import { UserService } from "../../../user/services/user.service";
import { Role } from "../../../user/enums/role";

@Component({
  selector: "app-all-estates",
  templateUrl: "./all-estates.component.html",
  styleUrl: "./all-estates.component.css",
})
export class AllEstatesComponent {
  estates: EstateGetDto[] = [];
  settlements: SettlementGetDto[] = [];

  estateFilterDto: EstateFilterDto = new EstateFilterDto();
  estateTypeLocalization = EstateTypeLocalization;
  role: Role;

  constructor(
    private estateService: EstateService,
    private settlementService: SettlementService,
    public userService: UserService
  ) {}

  ngOnInit() {
    this.get();
    this.getSettlements();
  }

  get() {
    this.estateService
      .getAll()
      .pipe(
        catchError((err) => {
          return throwError(() => err);
        })
      )
      .subscribe((res) => {
        this.estates = res;
      });
  }

  getFiltered(estateFilterDto: EstateFilterDto) {
    this.estateService
      .getFiltered(estateFilterDto)
      .pipe(
        catchError((err) => {
          return throwError(() => err);
        })
      )
      .subscribe((res) => {
        this.estates = res;
      });
  }

  getSettlements() {
    this.settlementService
      .getAll()
      .pipe(
        catchError((err) => {
          return throwError(() => err);
        })
      )
      .subscribe((res) => {
        this.settlements = res;
      });
  }
}
