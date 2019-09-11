import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { CsvImportComponent } from './csv-import/csv-import.component';
import { HomeComponent } from './home/home.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { SalesRecordsGridComponent } from './sales-records-grid/sales-records-grid.component';
import { CsvFilesService } from './services/csv-files.service';
import { SalesRecordService } from './services/sales-record.service';
import { SharedModule } from './shared/shared.module';
import { NgxPaginationModule } from 'ngx-pagination';
import { CountryFilterComponent } from './country-filter/country-filter.component';
import { ReportsModule } from './reports/reports.module';
import { ReportsTableComponent } from './reports/reports-table/reports-table.component';
import { NgbModule, NgbDateAdapter, NgbDateNativeAdapter } from '@ng-bootstrap/ng-bootstrap'
import { EditRecordFormComponent } from './edit-record-form/edit-record-form.component';


@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    CsvImportComponent,
    HomeComponent,
    SalesRecordsGridComponent,
    CountryFilterComponent,
    EditRecordFormComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    SharedModule,
    NgxPaginationModule,
    ReportsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'reports', component: ReportsTableComponent, pathMatch: 'full' },
    ]),
    NgbModule,
    ReactiveFormsModule

  ],
  providers: [CsvFilesService, SalesRecordService, { provide: NgbDateAdapter, useClass: NgbDateNativeAdapter }],
  bootstrap: [AppComponent]
})
export class AppModule { }
