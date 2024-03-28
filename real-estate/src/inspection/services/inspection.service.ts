import { Injectable } from "@angular/core";
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { InspectionAddDto } from "../dtos/inspection-add-dto";
import { InspectionGetDto } from "../dtos/inspection-get-dto";
import { Status } from "../enums/status";
import { InspectionFilterDto } from "../dtos/inspection-filter-dto";

@Injectable({
  providedIn: "root",
})
export class InspectionService {
  private baseUrl = "http://localhost:13131/api/Inspections";

  constructor(private http: HttpClient) {}

  add(inspectionDto: InspectionAddDto): Observable<void> {
    return this.http.post<void>(`${this.baseUrl}/Add`, inspectionDto);
  }

  getAll(): Observable<InspectionGetDto[]> {
    return this.http.get<InspectionGetDto[]>(`${this.baseUrl}/All`);
  }

  changeStatus(inspectionId: number, status: Status): Observable<void> {
    const params = new HttpParams()
      .set("inspectionId", inspectionId.toString())
      .set("status", status.toString());

    return this.http.post<void>(`${this.baseUrl}/ChangeStatus`, null, {
      params,
    });
  }

  getFiltered(
    inspectionDto: InspectionFilterDto
  ): Observable<InspectionGetDto[]> {
    return this.http.get<InspectionGetDto[]>(
      `${this.baseUrl}/Filtered?${this.composeQueryString(inspectionDto)}`
    );
  }

  composeQueryString(inspectionDto: InspectionFilterDto): string {
    return Object.entries(inspectionDto)
      .filter(([_, value]) => value !== null)
      .map(([key, value]) => `${key}=${value}`)
      .join("&");
  }
}
