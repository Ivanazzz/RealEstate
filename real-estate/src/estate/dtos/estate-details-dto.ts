import { EstateType } from "../enums/estate-type";

export class EstateDetailsDto {
  id: number;
  estateBrokerId: number;
  estateBrokerUsername: string;
  type: EstateType;
  price: number;
  size: number;
  settlementId: number;
  settlementName: string;
  floor?: number;
}
