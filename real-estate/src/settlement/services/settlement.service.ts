import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { SettlementGetDto } from "../dtos/settlement-get-dto";

@Injectable({
  providedIn: "root",
})
export class SettlementService {
  private baseUrl = "http://localhost:13131/api/Settlements";

  constructor(private http: HttpClient) {}

  getAll(): Observable<SettlementGetDto[]> {
    return this.http.get<SettlementGetDto[]>(`${this.baseUrl}`);
  }
}
