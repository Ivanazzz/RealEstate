import { Component } from "@angular/core";
import { EstateDetailsDto } from "../../dtos/estate-details-dto";
import { SettlementGetDto } from "../../../settlement/dtos/settlement-get-dto";
import { EstateService } from "../../services/estate.service";
import { SettlementService } from "../../../settlement/services/settlement.service";
import { ActivatedRoute } from "@angular/router";
import { catchError, throwError } from "rxjs";
import { EstateUpdateDto } from "../../dtos/estate-update-dto";
import { UserService } from "../../../user/services/user.service";
import { InspectionAddDto } from "../../../inspection/dtos/inspection-add-dto";
import { InspectionService } from "../../../inspection/services/inspection.service";

@Component({
  selector: "app-details-estate",
  templateUrl: "./details-estate.component.html",
  styleUrl: "./details-estate.component.css",
})
export class DetailsEstateComponent {
  estateDto: EstateDetailsDto = new EstateDetailsDto();
  estateUpdateDto: EstateUpdateDto = new EstateUpdateDto();
  settlements: SettlementGetDto[] = [];
  estateId: number;

  inspectionDto: InspectionAddDto = new InspectionAddDto();
  wantToAddInspection = false;

  constructor(
    private route: ActivatedRoute,
    private estateService: EstateService,
    private settlementService: SettlementService,
    public userService: UserService,
    private inspectionService: InspectionService
  ) {}

  ngOnInit(): void {
    this.route.params.subscribe((params) => {
      this.estateId = +params["id"];
      this.fetchEstateDetails();
      this.getSettlements();
    });
  }

  fetchEstateDetails(): void {
    this.estateService.getById(this.estateId).subscribe((data) => {
      this.estateDto = data;
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

  updateEstate() {
    this.estateUpdateDto.id = this.estateId;
    this.estateUpdateDto.estateBrokerId = this.estateDto.estateBrokerId;
    this.estateUpdateDto.type = this.estateDto.type;
    this.estateUpdateDto.price = this.estateDto.price;
    this.estateUpdateDto.size = this.estateDto.size;
    this.estateUpdateDto.settlementId = this.estateDto.settlementId;
    this.estateUpdateDto.floor = this.estateDto.floor;

    this.estateService
      .update(this.estateUpdateDto)
      .pipe(
        catchError((err) => {
          return throwError(() => err);
        })
      )
      .subscribe((res) => {});
  }

  changeState() {
    this.wantToAddInspection = true;
  }

  addInspection() {
    this.inspectionDto.estateId = this.estateDto.id;

    this.inspectionService
      .add(this.inspectionDto)
      .pipe(
        catchError((err) => {
          return throwError(() => err);
        })
      )
      .subscribe((res) => {});
  }
}
