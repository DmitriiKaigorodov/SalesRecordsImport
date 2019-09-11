import { Component, Output, EventEmitter } from '@angular/core';
import { OrdersCountFilter } from "./OrdersCountFilter";

@Component({
  selector: 'orders-count-filter',
  templateUrl: './orders-count-filter.component.html',
  styleUrls: ['./orders-count-filter.component.css']
})
export class OrdersCountFilterComponent {

  @Output() filterChanged = new EventEmitter();
  filter: OrdersCountFilter = new OrdersCountFilter();

  apply() {
    this.filterChanged.emit(this.filter);
  }
}
