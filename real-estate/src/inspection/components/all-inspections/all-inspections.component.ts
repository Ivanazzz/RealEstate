import { Component } from "@angular/core";
import { InspectionGetDto } from "../../dtos/inspection-get-dto";
import { InspectionService } from "../../services/inspection.service";
import { catchError, throwError } from "rxjs";
import { Status, StatusLocalization } from "../../enums/status";
import { InspectionFilterDto } from "../../dtos/inspection-filter-dto";

@Component({
  selector: "app-all-inspections",
  templateUrl: "./all-inspection.component.html",
  styleUrl: "./all-inspection.component.css",
})
export class AllInspectionsComponent {
  inspections: InspectionGetDto[] = [];
  inspectionFilterDto: InspectionFilterDto = new InspectionFilterDto();

  statusLocalization = StatusLocalization;

  constructor(private inspectionService: InspectionService) {}

  ngOnInit() {
    this.get();
  }

  get() {
    this.inspectionService
      .getAll()
      .pipe(
        catchError((err) => {
          return throwError(() => err);
        })
      )
      .subscribe((res) => {
        this.inspections = res;
      });
  }

  confirm(inspectionId: number) {
    this.inspectionService
      .changeStatus(inspectionId, Status.Confirmed)
      .pipe(
        catchError((err) => {
          return throwError(() => err);
        })
      )
      .subscribe((res) => {});
  }

  decline(inspectionId: number) {
    this.inspectionService
      .changeStatus(inspectionId, Status.Declined)
      .pipe(
        catchError((err) => {
          return throwError(() => err);
        })
      )
      .subscribe((res) => {});
  }

  getFiltered(inspectionFilterDto: InspectionFilterDto) {
    this.inspectionService
      .getFiltered(inspectionFilterDto)
      .pipe(
        catchError((err) => {
          return throwError(() => err);
        })
      )
      .subscribe((res) => {
        this.inspections = res;
      });
  }
}
