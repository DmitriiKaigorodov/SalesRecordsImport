import { HttpClient } from '@angular/common/http';
import { Injectable, Inject } from '@angular/core';


@Injectable()
export class CsvFilesService {

  private _baseUrl: string;
  private _importCsvFileEndpoint = "api/records/import"

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) { 

    this._baseUrl = baseUrl;
  }

  uploadCsvFile(fileData: FormData) {
    return this.http.post(this._baseUrl + this._importCsvFileEndpoint, fileData);
  }
}
