import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SmartGridComponent } from '../../../shared/smart-grid/smart-grid';
import { RouterLink } from '@angular/router'; // 1. Import RouterLink từ @angular/router

@Component({
  selector: 'app-customers-list',
  standalone: true,
  imports: [
    CommonModule,
    SmartGridComponent,
    RouterLink // 2. Thêm vào đây để Angular hiểu và kích hoạt điều hướng
  ],
  templateUrl: './customers-list.html',
  styleUrl: './customers-list.scss'
})
export class CustomersListComponent {

  customers = [

    {
      code: 'CUS001',
      name: 'SMARTDATAHRIS Company',
      email: 'abc@gmail.com',
      status: 'Active'
    },

    {
      code: 'CUS002',
      name: 'Microsoft VN',
      email: 'microsoft@gmail.com',
      status: 'Active'
    },

    {
      code: 'CUS003',
      name: 'FPT Software',
      email: 'fpt@gmail.com',
      status: 'Inactive'
    }

  ];

  columns = [
    {
          headerName: 'Action',
          width: 180,
          cellClass: 'text-center',
          headerClass: 'header-center',

          cellRenderer: () => {

            return `
              <div class="action-buttons">

                  <button class="icon-btn edit-btn">
                      <i class="bi bi-pencil-square"></i>
                  </button>

                  <button class="icon-btn delete-btn">
                      <i class="bi bi-trash"></i>
                  </button>

              </div>
            `;
          }
        },

  {
    field:'code',
    headerName:'Code',
    cellRenderer: (params: any) => {

    return `
      <span class="code-link">
        ${params.value}
      </span>
    `;
  }
  },
  {
    field:'name',
    headerName:'Customer Name'
    
  },
  {
    field:'email',
    headerName:'Email'
  },
  {
    field:'status',
    headerName:'Status',

    cellRenderer: (params: any) => {

        if (params.value === 'Active') {
          return `
            <span class="status-active">
              Active
            </span>
          `;
        }

         return `
          <span class="status-inactive">
            Inactive
          </span>
        `;
    }
  }
  ];

}