import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { CurrencyRate } from '../models/currency-rate';

@Injectable({
  providedIn: 'root'
})
export class CurrencyService {

  private apiUrl = 'http://localhost:5262/currencies';

  constructor(private http: HttpClient) { }

  getCurrencies(): Observable<any> {
    return this.http.get<any>(this.apiUrl);
  }

  fetchCurrencies(): Observable<any> {
    return this.http.post(`${this.apiUrl}/fetch`, {});
  }

  getCurrenciesByDate(date: string): Observable<CurrencyRate[]> {
    return this.http.get<CurrencyRate[]>(
      `${this.apiUrl}/date/${date}`
    );
  }

  getCurrenciesByYear(year: number): Observable<CurrencyRate[]> {
    return this.http.get<CurrencyRate[]>(
      `${this.apiUrl}/year/${year}`
    );
  }
  getCurrenciesByMonth(
    year: number,
    month: number
  ): Observable<any> {
    return this.http.get(
      `${this.apiUrl}/month/${year}/${month}`
    );
  }
  getCurrenciesByQuarter(
    year: number,
    quarter: number
  ): Observable<any> {
    return this.http.get(
      `${this.apiUrl}/quarter/${year}/${quarter}`
    );
  }
}