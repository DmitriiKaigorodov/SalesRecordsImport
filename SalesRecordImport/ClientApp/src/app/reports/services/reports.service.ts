import { HttpClient } from '@angular/common/http';
import { Injectable, Inject } from '@angular/core';
import { OrdersCountFilter } from '../orders-count/filter/OrdersCountFilter';
import { objectToQueryString } from '../../shared/utils';

@Injectable()
export class ReportsService {

  private _baseUrl: string;
  private _reportsEndpoint = "api/reports";

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {

    this._baseUrl = baseUrl;
  }

  getOrdersCountReport(ordersCount: OrdersCountFilter) {
    var url = this._baseUrl + this._reportsEndpoint + "/ordersCount";
    var queryString = objectToQueryString(ordersCount);
    return this.http.get<any>(url + queryString);
  }

  getTotalProfitReport(ordersCount: OrdersCountFilter) {
    var url = this._baseUrl + this._reportsEndpoint + "/totalProfit";
    var queryString = objectToQueryString(ordersCount);
    return this.http.get<any>(url + queryString);
  }

}
