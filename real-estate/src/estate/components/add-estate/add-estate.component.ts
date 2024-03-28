import { Component } from "@angular/core";
import { EstateAddDto } from "../../dtos/estate-add-dto";
import { EstateService } from "../../services/estate.service";
import { SettlementService } from "../../../settlement/services/settlement.service";
import { SettlementGetDto } from "../../../settlement/dtos/settlement-get-dto";
import { catchError, throwError } from "rxjs";
import { EstateType } from "../../enums/estate-type";

@Component({
  selector: "app-add-estate",
  templateUrl: "./add-estate.component.html",
  styleUrl: "./add-estate.component.css",
})
export class AddEstateComponent {
  estateDto: EstateAddDto = new EstateAddDto();
  settlements: SettlementGetDto[] = [];

  type: EstateType;

  constructor(
    private estateService: EstateService,
    private settlementService: SettlementService
  ) {}

  ngOnInit() {
    this.getSettlements();
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

  add() {
    this.estateService
      .add(this.estateDto)
      .pipe(
        catchError((err) => {
          return throwError(() => err);
        })
      )
      .subscribe((res) => {});
  }
}
