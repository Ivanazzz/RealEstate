import { Status } from "../enums/status";

export class InspectionFilterDto {
  inspectionStatus: Status;
  dateFrom: Date;
  dateTo: Date;
}
