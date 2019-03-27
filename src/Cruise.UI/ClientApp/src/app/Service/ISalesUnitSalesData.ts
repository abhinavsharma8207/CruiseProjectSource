export interface ISalesUnitSalesData {
  salesUnit: ISalesUnit;
  bookingTotal: number;
}

export interface ISalesUnit {
  id: number;
  name: string;
  country: number;
  currency: string;
  bookingDetailsUrl: string;
}
