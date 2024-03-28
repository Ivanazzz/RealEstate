import { EstateType } from "../enums/estate-type";

export class EstateAddDto {
  type: EstateType;
  price: number;
  size: number;
  settlementId: number;
  floor?: number;
}
