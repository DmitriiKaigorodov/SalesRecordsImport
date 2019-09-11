import { Component, Input, Output, OnInit, EventEmitter } from '@angular/core';

@Component({
  selector: 'year-picker',
  templateUrl: './year-picker.component.html',
  styleUrls: ['./year-picker.component.css']
})
export class YearPickerComponent implements OnInit {

  @Input() pastYearsCount: number = 10;
  @Input() selectedYear: number;
  @Output() selectedYearChanged = new EventEmitter();

  displayYears : number[] = [];
  currentYear: number;


  constructor() {

    this.currentYear = new Date().getFullYear();
    var latestYear = this.currentYear - this.pastYearsCount;

    for (let year = latestYear; year <= this.currentYear; year++) {
      this.displayYears.push(year);
    }
  }
  ngOnInit(): void {
  }
}
