import { Component, EventEmitter, Output } from '@angular/core';
import { CsvFilesService } from '../services/csv-files.service';

@Component({
  selector: 'csv-import',
  templateUrl: './csv-import.component.html',
})
export class CsvImportComponent {

  loading: boolean

  @Output() csvUploaded = new EventEmitter()
  constructor(private csvFilesService: CsvFilesService) {

  }

  onCsvSelected(event: any) {

    if (event.target.files.length > 0) {

      let file = event.target.files[0]
      let fileData = new FormData();
      fileData.append('csvFile', file);

      this.uploadCsvFile(fileData);
    }
  }

  uploadCsvFile(fileData: FormData) {

    this.loading = true;
    this.csvFilesService.uploadCsvFile(fileData).subscribe(r => {
      this.csvUploaded.emit();
      this.loading = false;
    })
  }
}
