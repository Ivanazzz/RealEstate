import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { EstateGetDto } from "../dtos/estate-get-dto";
import { EstateFilterDto } from "../dtos/estate-filter-dto";
import { EstateAddDto } from "../dtos/estate-add-dto";
import { EstateDetailsDto } from "../dtos/estate-details-dto";
import { EstateUpdateDto } from "../dtos/estate-update-dto";

@Injectable({
  providedIn: "root",
})
export class EstateService {
  private baseUrl = "http://localhost:13131/api/Estates";

  constructor(private http: HttpClient) {}

  getAll(): Observable<EstateGetDto[]> {
    return this.http.get<EstateGetDto[]>(`${this.baseUrl}/All`);
  }

  getFiltered(estateDto: EstateFilterDto): Observable<EstateGetDto[]> {
    return this.http.get<EstateGetDto[]>(
      `${this.baseUrl}/Filtered?${this.composeQueryString(estateDto)}`
    );
  }

  add(estateDto: EstateAddDto): Observable<void> {
    return this.http.post<void>(`${this.baseUrl}/Add`, estateDto);
  }

  getById(id: number): Observable<EstateDetailsDto> {
    return this.http.get<EstateDetailsDto>(`${this.baseUrl}/ById/${id}`);
  }

  update(estateDto: EstateUpdateDto): Observable<void> {
    return this.http.post<void>(`${this.baseUrl}/Update`, estateDto);
  }

  composeQueryString(estateDto: EstateFilterDto): string {
    return Object.entries(estateDto)
      .filter(([_, value]) => value !== null)
      .map(([key, value]) => `${key}=${value}`)
      .join("&");
  }
}
