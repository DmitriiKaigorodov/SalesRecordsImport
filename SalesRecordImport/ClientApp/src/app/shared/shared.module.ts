import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { LoaderComponent } from './loader/loader.component';
import { YearPickerComponent } from './year-picker/year-picker.component';

@NgModule({
  imports: [
    CommonModule,
    BrowserModule,
    FormsModule
  ],
  exports: [LoaderComponent, YearPickerComponent ],
  declarations: [ LoaderComponent, YearPickerComponent ],
  providers: []
})
export class SharedModule { }
