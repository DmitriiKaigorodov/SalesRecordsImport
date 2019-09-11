import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { SharedModule }  from '../shared/shared.module';
import { ReportsTableComponent } from './reports-table/reports-table.component';
import { OrdersCountComponent } from './orders-count/orders-count.component';
import { OrdersCountFilterComponent } from './orders-count/filter/orders-count-filter.component';
import { TotalProfitComponent } from './total-profit/total-profit.component';
import { TotalProfitFilterComponent } from './total-profit/filter/total-profit-filter.component';
import { ReportsService } from './services/reports.service';

@NgModule({
  imports: [
    CommonModule,
    BrowserModule,
    FormsModule,
    SharedModule
  ],
  exports: [ReportsTableComponent],
  declarations: [ReportsTableComponent, OrdersCountComponent, TotalProfitComponent, OrdersCountFilterComponent, TotalProfitFilterComponent],
  providers: [ReportsService]
})
export class ReportsModule { }
