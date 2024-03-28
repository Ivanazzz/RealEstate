import { Status } from "../enums/status";

export class InspectionGetDto {
  id: number;
  userId: number;
  username: string;
  inspectionDate: Date;
  estateId: number;
  status: Status;
}
