import { Injectable } from "@angular/core";
import { Http, Response } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import { ISalesUnitSalesData } from "./ISalesUnitSalesData";
import { IBookingData } from "./IBookingData";

@Injectable()
export class BookingService {

  baseUrl: string = "http://localhost:57191/api/cruise/";

  constructor(private http: Http) { }

  //service to get salesUnits list
  getSalesUnits(startDate: Date, endDate: Date): Observable<ISalesUnitSalesData[]> {

    return this.http.get(this.baseUrl + "GetSalesUnitSalesData?startDate=" + startDate + "&endDate=" + endDate)
      .map((response: Response) => {
        var result = <ISalesUnitSalesData[]>response.json();

        return result;
      });
  }

  //service to get BookingDetails of a given salesUnitId
  getBookingDetails(salesUnitId: number, startDate: Date, endDate: Date): Observable<IBookingData[]> {

    return this.http.get(this.baseUrl +
        "GetBookings?startDate=" + startDate +
        "&endDate=" + endDate +
      "&salesUnitId=" + salesUnitId
    ).map((response: Response) => {
        var result = <IBookingData[]>(response.json());

        return result;
      });
  }

  //service to get BookingDetails of a given salesUnitId and search text
  getBookingDetailsBySearch(salesUnitId: number, searchTerm: string, startDate: Date, endDate: Date): Observable<IBookingData[]> {

    return this.http.get(this.baseUrl +
      "SearchBookings?startDate=" + startDate +
      "&endDate=" + endDate +
      "&salesUnitId=" + salesUnitId +
      "&searchTerm=" + searchTerm
    ).map((response: Response) => {
        var result = <IBookingData[]>(response.json());

        return result;
      });
  }
  
}
