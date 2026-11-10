import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { CustomerCreateService } from './../../../../services/projects/customers/customer.service';
import { Router } from '@angular/router';
import { HttpClient, HttpHeaders, HttpBackend } from '@angular/common/http';
import { CreateCustomerDto } from './../../../../types/projetcs/customers/customers';

@Component({
  selector: 'app-customer-create',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './customers-create.html',
  styleUrls: ['./customers-create.scss']
})
export class CustomerCreateComponent {
  
  private fb = inject(FormBuilder);
  private httpBackend = inject(HttpBackend);
  private customerCreateService = inject(CustomerCreateService);
  private router = inject(Router);
  private httpWithoutInterceptor = new HttpClient(this.httpBackend);
  // Thêm các biến này vào class CustomerCreateComponent
  errorMessage: string | null = null;
  showErrorModal: boolean = false;

  // Khởi tạo form 1 lần duy nhất chuẩn Angular
  customerForm: FormGroup = this.fb.group({
    masothue: ['', [Validators.required, Validators.maxLength(50)]],
    customerName: ['', [Validators.required, Validators.maxLength(250)]],
    industry: [''],
    contactName: [''],
    contactNumber: [''],
    contactAddress: [''],
    contactEmail: ['', [Validators.email]],
    description: ['']
  });

  isLoading = false;

  // Hàm tra cứu mã số thuế qua API bên ngoài
  lookupTaxCode() {
    const taxValue = this.customerForm.get('masothue')?.value;
    if (!taxValue || String(taxValue).trim() === '') {
      console.log('MST đang trống, không gọi API.');
      return;
    }
    const taxCode = String(taxValue).trim();

    this.isLoading = true;
    const url = `/v1/businesses?tax_code=${taxCode}&per_page=10`;
    
    const headers = new HttpHeaders({
      'Authorization': 'Bearer dlpl_live_CXBMOFuNxzAEwYSygAOD1a8gZkr4EKRfmaL9z3'
    });

    this.httpWithoutInterceptor.get<any>(url, { headers }).subscribe({
      next: (res) => {
        this.isLoading = false;
        const business = res?.data?.[0] || res?.[0] || res;
        if (business) {
          this.customerForm.patchValue({
            customerName: business.name || '',
            industry: business.industries?.[0]?.name || 'Không xác định',
            contactAddress: business.address || ''
          });
        }
      },
      error: (err) => {
        this.isLoading = false;
        console.error('Lỗi chi tiết khi gọi API tra cứu:', err);
      }
    });
  }


  // Hàm xử lý back trang

  onCancel() {
  // Có thể thêm xác nhận ở đây nếu muốn: if (confirm('Bạn có chắc muốn hủy?')) { ... }
  this.router.navigate(['/customers/list']);
}

  // Hàm xử lý lưu dữ liệu duy nhất
  onSave() {
    if (this.customerForm.invalid) {
      this.customerForm.markAllAsTouched();
      console.warn('Form không hợp lệ, vui lòng kiểm tra lại các trường bắt buộc.');
      return;
    }

    const dto: CreateCustomerDto = this.customerForm.value;

    // Gọi API lưu dữ liệu xuống backend .NET Core
    this.customerCreateService.createCustomer(dto).subscribe({
      next: (response) => {
        // Lưu thành công, điều hướng về màn hình danh sách khách hàng
        this.router.navigate(['/customers']); 
      },
      error: (err) => {
        console.error('Lỗi khi lưu khách hàng:', err);

        let friendlyMessage = 'Đã có lỗi xảy ra trong quá trình lưu dữ liệu.';

      // Kiểm tra nếu Backend có trả về message cụ thể (ví dụ: { message: '...' })
      if (err?.error?.message) {
        friendlyMessage = err.error.message;
      }

        // Kiểm tra lỗi Validate tự động của .NET Core (ModelState validation errors)
      else if (err?.error?.errors) {
        const errorKeys = Object.keys(err.error.errors);
        if (errorKeys.length > 0) {
          const firstField = errorKeys[0];
          friendlyMessage = err.error.errors[firstField][0]; // Lấy thông báo lỗi đầu tiên của field
        }
      }      

      // Bắt lỗi trùng khóa SQL Server (Duplicate key / Index violation)
      else if (err?.status === 400) {
        friendlyMessage = 'Dữ liệu gửi lên không hợp lệ hoặc bị trùng lặp mã định danh.';
      } else if (err?.status === 500) {
        friendlyMessage = 'Lỗi hệ thống Server (500). Vui lòng kiểm tra lại log backend.';
      }

        // Lấy chi tiết lỗi từ Backend (ví dụ: err.error.message hoặc err.message)
        this.errorMessage = friendlyMessage; 'Đã có lỗi không xác định xảy ra từ hệ thống.';
        this.showErrorModal = true; // Bật cờ hiển thị popup lỗi

      }
    });
    
  }
    // Hàm đóng popup lỗi
  closeErrorModal() {
    this.showErrorModal = false;
    this.errorMessage = null;
  }
}