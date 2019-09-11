import { Component, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'country-filter',
  templateUrl: './country-filter.component.html',
  styleUrls: ['./country-filter.component.css']
})
export class CountryFilterComponent {

  @Output() filterChanged = new EventEmitter();
  country: string;

  apply() {
    this.filterChanged.emit(this.country);
  }
}
