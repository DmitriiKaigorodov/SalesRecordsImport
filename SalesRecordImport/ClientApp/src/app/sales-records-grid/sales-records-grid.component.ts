import { Component, Input, Output, EventEmitter } from '@angular/core';
import { SalesRecord } from '../models/SalesRecord';
import { PaginateConfig } from '../models/PaginateConfig';
import { SalesChannel } from '../models/SalesChannel';
import { OrderPriority } from '../models/OrderPriority';
import { SalesRecordService } from '../services/sales-record.service';

@Component({
  selector: 'sales-records-grid',
  templateUrl: './sales-records-grid.component.html',
  styleUrls: ['./sales-records-grid.component.css']
})
export class SalesRecordsGridComponent {

  @Input() data: SalesRecord[];
  @Input() paginateConfig: PaginateConfig;
  @Output() orderClicked = new EventEmitter();

  SalesChannel = SalesChannel;
  OrderPriority = OrderPriority;

  ascOrder: boolean;

  constructor(private salesRecordService: SalesRecordService) {

  }

  order(column: string) {

    this.ascOrder = !this.ascOrder
    this.orderClicked.emit(
      {
        column: column,
        ascOrder: this.ascOrder
      })
  }

  delete(id: number) {

    this.salesRecordService.deleteSalesRecord(id).subscribe(r => {
      this.data = this.data.filter(record => record.id !== id);
    });
  }
}
