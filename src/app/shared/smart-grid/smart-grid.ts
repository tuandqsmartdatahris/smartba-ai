import { Component, Inject, Input,  PLATFORM_ID } from '@angular/core';
import {  CommonModule,   isPlatformBrowser} from '@angular/common';
import { AgGridAngular } from 'ag-grid-angular';
import { ColDef,  GridReadyEvent} from 'ag-grid-community';

@Component({
  selector: 'app-smart-grid',
  standalone: true,
  imports: [AgGridAngular, CommonModule],
  templateUrl: './smart-grid.html',
  styleUrls: ['./smart-grid.scss']
})
export class SmartGridComponent {

  @Input() rowData: any[] = [];

  @Input() columnDefs: ColDef[] = [];

  isBrowser = false;

  pagination = true;

  paginationPageSize = 20;

  quickFilter = '';

  constructor(@Inject(PLATFORM_ID) private platformId: Object) {
    this.isBrowser = isPlatformBrowser(this.platformId);
  }
  defaultColDef: ColDef = {
    sortable: true,
    filter: true,
    floatingFilter: true,
    resizable: true,
    headerClass: 'grid-header-center',
    cellClass: 'grid-cell-center'
  };

  gridApi: any;

  onGridReady(params: any) {
    const colIds = params.api.getColumns()?.map((c: any) => c.getId()) || [];
    params.api.autoSizeColumns(colIds);
  }

  search(event: Event) {
    const value =
      (event.target as HTMLInputElement).value;

    this.gridApi?.setGridOption(
      'quickFilterText',
      value
    );
  }

  exportCsv() {
    this.gridApi.exportDataAsCsv();
  }

  newCustomer() {
    // Implementation for adding a new customer
  }

  
  
}