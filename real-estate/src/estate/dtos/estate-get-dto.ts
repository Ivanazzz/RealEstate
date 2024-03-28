import { EstateType } from "../enums/estate-type";

export class EstateGetDto {
  id: number;
  estateBrokerUsername: string;
  estateType: EstateType;
  price: number;
  size: number;
  settlementName: string;
  floor?: number;
}
