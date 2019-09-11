import { Component, Output, EventEmitter } from '@angular/core';
import { TotalProfitFilter } from "./TotalProfitFilter";

@Component({
  selector: 'total-profit-filter',
  templateUrl: './total-profit-filter.component.html',
  styleUrls: ['./total-profit-filter.component.css']
})
export class TotalProfitFilterComponent {

  @Output() filterChanged = new EventEmitter();
  filter: TotalProfitFilter = new TotalProfitFilter();

  apply() {
    this.filterChanged.emit(this.filter);
  }
}
