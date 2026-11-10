
import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { CreateCustomerDto, CustomerResponseDto } from './../../../types/projetcs/customers/customers';

@Injectable({
  providedIn: 'root'
})
export class CustomerCreateService {
  private http = inject(HttpClient);
  private apiUrl = 'http://localhost:5294/api/Customers'; // Cấu hình proxy hoặc URL gốc nếu cần

  public createCustomer(dto: CreateCustomerDto): Observable<CustomerResponseDto> {
    return this.http.post<CustomerResponseDto>(this.apiUrl, dto);
  }
}