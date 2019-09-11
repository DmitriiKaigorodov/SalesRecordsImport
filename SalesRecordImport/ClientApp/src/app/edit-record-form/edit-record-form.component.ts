import { Component, Input, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { SalesRecord } from '../models/SalesRecord';
import { SalesRecordService } from '../services/sales-record.service';

@Component({
  selector: 'edit-record-form',
  templateUrl: './edit-record-form.component.html'
})
export class EditRecordFormComponent implements OnInit {

  @Input() salesRecord: SalesRecord;
  editableSalesRecord: SalesRecord = new SalesRecord();

  constructor(private modalService: NgbModal, private salesRecordService: SalesRecordService) {
  }

  ngOnInit(): void {
    this.editableSalesRecord = Object.assign(this.editableSalesRecord, this.salesRecord);
  }

  open(content) {
    this.modalService.open(content, { ariaLabelledBy: 'modal-basic-title' }).result.then((result) => {

    }, (reason) => {
      this.editableSalesRecord = Object.assign(this.editableSalesRecord, this.salesRecord);
    });
  }

  submitForm() {

    this.salesRecordService.updateSalesRecord(this.editableSalesRecord).subscribe(r => {
      this.salesRecord = Object.assign(this.salesRecord, this.editableSalesRecord);
      this.modalService.dismissAll();
    });
  }

}
