import { Component, OnInit } from '@angular/core';
import { SalesRecord } from '../models/SalesRecord';
import { SalesRecordService } from '../services/sales-record.service';
import { SalesRecordsOptions } from '../models/SalesRecordsOptions';
import { PaginateConfig } from '../models/PaginateConfig';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit {

  records: SalesRecord[];
  totalRecordsCount: number;
  salesRecordsOptions: SalesRecordsOptions = new SalesRecordsOptions();
  paginateConfig = new PaginateConfig();

  constructor(private salesRecordService: SalesRecordService) {

  }

  ngOnInit(): void {
    this.retrieveSalesRecords();
    this.paginateConfig.currentPage = 1;
    this.paginateConfig.itemsPerPage = this.salesRecordsOptions.size;
  }

  retrieveSalesRecords() {
    this.salesRecordService.getSalesRecords(this.salesRecordsOptions).subscribe(response => {
      this.records = response.result;
      this.totalRecordsCount = response.totalCount;
      this.paginateConfig.totalItems = this.totalRecordsCount;
      this.paginateConfig.itemsPerPage = this.salesRecordsOptions.size;
      console.log(this.paginateConfig);
    });
  }
  onPageChanged(page: number) {
    this.salesRecordsOptions.page = page;
    this.retrieveSalesRecords();
    this.paginateConfig.currentPage = page;
  }

  onFilterChanged(country: string) {
    this.salesRecordsOptions.country = country;
    this.retrieveSalesRecords();
  }

  orderClicked(orderInfo: any) {
    console.log(orderInfo);
    this.salesRecordsOptions.orderColumn = orderInfo.column;
    this.salesRecordsOptions.orderAscending = orderInfo.ascOrder;
    this.retrieveSalesRecords();
  }

  onCsvUploaded(event: any) {
    this.salesRecordsOptions.page = 1;
    this.salesRecordsOptions.country = "";
    this.salesRecordsOptions.orderColumn = ""
    this.retrieveSalesRecords();
  }

  onPageSizeChanged(event: any) {
    this.retrieveSalesRecords();
  }
}
