import { EstateType } from "../enums/estate-type";

export class EstateUpdateDto {
  id: number;
  estateBrokerId: number;
  type: EstateType;
  price: number;
  size: number;
  settlementId: number;
  floor?: number;
}
