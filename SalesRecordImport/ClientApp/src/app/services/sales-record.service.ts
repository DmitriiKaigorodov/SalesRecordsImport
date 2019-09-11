import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable, Inject } from '@angular/core';
import { SalesRecordsOptions } from '../models/SalesRecordsOptions';
import { SalesRecord } from '../models/SalesRecord';
import { objectToQueryString } from '../shared/utils';
import { PagedResult } from '../models/PagedResult';
import { map } from 'rxjs/operators';

@Injectable()
export class SalesRecordService {

  private _baseUrl: string;
  private _getSalesRecordsEndpoint = "api/records";

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {

    this._baseUrl = baseUrl;
  }

  getSalesRecords(salesRecordOptions: SalesRecordsOptions) {
    var url = this._baseUrl + this._getSalesRecordsEndpoint
    var queryString = objectToQueryString(salesRecordOptions);
    return this.http.get<PagedResult>(url + queryString).pipe(map(response => {

      response.result = response.result.map(record => {

        var genericRecord = record as any;
        record.orderDate = new Date(genericRecord.orderDate);
        record.shipDate = new Date(genericRecord.shipDate);
        return record
      });

      return response;
    }));
  }

  deleteSalesRecord(recordId: number) {
    var url = this._baseUrl + this._getSalesRecordsEndpoint + "/" + recordId;
    return this.http.delete(url);
  }

  updateSalesRecord(record: SalesRecord) {
    let headers = new HttpHeaders();
    headers = headers.set('Content-Type', 'application/json; charset=utf-8');
    var url = this._baseUrl + this._getSalesRecordsEndpoint;
    return this.http.put(url, JSON.stringify(record), {
      headers: headers
    });
  }

}
