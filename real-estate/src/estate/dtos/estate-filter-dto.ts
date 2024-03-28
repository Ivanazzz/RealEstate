import { EstateType } from "../enums/estate-type";

export class EstateFilterDto {
  type: EstateType;
  priceMin: number;
  priceMax: number;
  sizeMin: number;
  sizeMax: number;
  settlementId: number;
  floor: number;
}
