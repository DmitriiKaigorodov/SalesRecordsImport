import { Component } from '@angular/core';
import { TotalProfitFilter } from './filter/TotalProfitFilter';
import { ReportsService } from '../services/reports.service';

@Component({
  selector: 'total-profit',
  templateUrl: './total-profit.component.html',
  styleUrls: ['./total-profit.component.css']
})
export class TotalProfitComponent {

  totalProfit?: number;
  filter: TotalProfitFilter = new TotalProfitFilter();
  selectedCountry: string;
  selectedYear: number

  constructor(private reportsService: ReportsService) {
    this.filter.year = new Date().getFullYear();
  }

  onFilterChanged(filter: TotalProfitFilter) {
    this.getReport(filter);
  }

  getReport(filter: TotalProfitFilter) {

    this.reportsService.getTotalProfitReport(filter).subscribe(r => {
      this.totalProfit = r.profit;
      this.selectedCountry = filter.country;
      this.selectedYear = filter.year;
    });
  }
}
