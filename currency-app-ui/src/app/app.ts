import { Component, signal } from '@angular/core';
import { CurrencyService } from './services/currency.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';


@Component({
  selector: 'app-root',
  imports: [CommonModule, FormsModule],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {

  protected readonly title = signal('currency-app-ui');

  currencies: any[] = [];

  selectedDate = '';
  selectedYear: number | null = null;
  selectedMonth: number | null = null;
  selectedQuarter: number | null = null;

  constructor(private currencyService: CurrencyService)
  {
  }

  loadCurrencies()
{
  this.currencyService.getCurrencies().subscribe({
    next: data =>
    {
      this.currencies = data[0].rates;
    },

    error: err =>
    {
      console.error(err);
      alert('Failed to load currencies');
    }
  });
}

  fetchCurrencies()
{
  this.currencyService.fetchCurrencies().subscribe({
    next: result =>
    {
      console.log(result);
      alert('Currencies downloaded successfully');
    },

    error: err =>
    {
      console.error(err);
      alert('Download failed');
    }
  });
}
  searchByDate()
{
  if (!this.selectedDate)
  {
    alert('Select date');
    return;
  }

  this.currencyService
    .getCurrenciesByDate(this.selectedDate)
    .subscribe({
      next: data =>
      {
        this.currencies = data;
      },

      error: err =>
      {
        console.error(err);
        alert('Server error');
      }
    });
}
  searchByYear()
{
  if (!this.selectedYear)
  {
    alert('Enter year');
    return;
  }

  this.currencyService
    .getCurrenciesByYear(this.selectedYear)
    .subscribe({
      next: data =>
      {
        this.currencies = data;
      },

      error: err =>
      {
        console.error(err);
        alert('Server error');
      }
    });
  }
  searchByMonth()
{
  if (!this.selectedYear || !this.selectedMonth)
  {
    alert('Enter year and month');
    return;
  }

  this.currencyService
    .getCurrenciesByMonth(
      this.selectedYear,
      this.selectedMonth)
    .subscribe({
      next: data =>
      {
        this.currencies = data;
      },

      error: err =>
      {
        console.error(err);
        alert('Server error');
      }
    });
  }
  searchByQuarter()
{
  if (!this.selectedYear || !this.selectedQuarter)
  {
    alert('Enter year and quarter');
    return;
  }

  this.currencyService
    .getCurrenciesByQuarter(
      this.selectedYear,
      this.selectedQuarter)
    .subscribe({
      next: data =>
      {
        this.currencies = data;
      },

      error: err =>
      {
        console.error(err);
        alert('Server error');
      }
    });
}
  sortByRate()
{
  this.currencies.sort((a: any, b: any) =>
  {
    const rateA = a.rate ?? a.mid;
    const rateB = b.rate ?? b.mid;

    return rateA - rateB;
  });
}
  
  
}