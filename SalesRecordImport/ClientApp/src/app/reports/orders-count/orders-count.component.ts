import { Component, Input, Output, OnInit, EventEmitter } from '@angular/core';
import { OrdersCountFilter } from './filter/OrdersCountFilter';
import { ReportsService } from '../services/reports.service';

@Component({
  selector: 'orders-count',
  templateUrl: './orders-count.component.html',
  styleUrls: ['./orders-count.component.css']
})
export class OrdersCountComponent {

  ordersCount?: number;
  filter: OrdersCountFilter = new OrdersCountFilter();

  constructor(private reportsService: ReportsService) {
    this.filter.year = new Date().getFullYear();
  }

  onFilterChanged(filter: OrdersCountFilter) {
    this.getReport(filter);
  }

  getReport(filter: OrdersCountFilter) {

    this.reportsService.getOrdersCountReport(filter).subscribe(r => {
      this.ordersCount = r.ordersCount;
      this.filter = filter;
    });
  }
}
