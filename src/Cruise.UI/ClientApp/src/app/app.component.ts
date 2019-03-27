import { Component, OnInit } from '@angular/core';
import { DatePipe } from '@angular/common';
import { BookingService } from "./Service/Bookings.service";
import { ISalesUnitSalesData } from "./Service/ISalesUnitSalesData";
import { IBookingData } from "./Service/IBookingData";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  providers: [BookingService, DatePipe ]
})
export class AppComponent implements OnInit{

  startDate;
  endDate;
  searchTerm: string;
  salesUnitId: number;
  salesUnitName: string;
  salesUnits: ISalesUnitSalesData[];
  bookingsData: IBookingData[];

  constructor(private _bookingService: BookingService, private datePipe: DatePipe ) {
    this.startDate = new Date('2016-01-01');
    this.endDate = new Date('2016-03-31');
  }

  ngOnInit() {
    this.startDate = this.datePipe.transform(this.startDate, 'yyyy-MM-dd');
    this.endDate = this.datePipe.transform(this.endDate, 'yyyy-MM-dd');
    this.getSalesUnits();
  }

  getSalesUnits() {
    this.validateAndUpdatesDatesToDefault(this.startDate, this.endDate);
    this.bookingsData = null;
    this._bookingService.getSalesUnits(this.startDate, this.endDate).subscribe(salesUnitsData => this.salesUnits = salesUnitsData);
    
  }

  getBookingDetails(salesUnitId: number, salesUnitName: string) {
    this.salesUnitId = salesUnitId;
    this.salesUnitName = salesUnitName;
    this.validateAndUpdatesDatesToDefault(this.startDate, this.endDate);
    this._bookingService.getBookingDetails(salesUnitId, this.startDate, this.endDate).subscribe(bookingData => this.bookingsData = bookingData);
  }

  getBookingDetailsBySearch(searchTerm: string) {
    this.validateAndUpdatesDatesToDefault(this.startDate, this.endDate);
    this._bookingService.getBookingDetailsBySearch(this.salesUnitId, searchTerm, this.startDate, this.endDate).subscribe(bookingData => this.bookingsData = bookingData);
  }

  validateAndUpdatesDatesToDefault(startDate: Date, endDate: Date) {
    if (startDate)
      this.startDate = startDate;
    else
      this.startDate = this.datePipe.transform(new Date('2016-01-01'), 'yyyy-MM-dd');

    if (endDate)
      this.endDate = endDate;
    else
      this.endDate = this.datePipe.transform(new Date('2016-03-31'), 'yyyy-MM-dd');
  }
}
