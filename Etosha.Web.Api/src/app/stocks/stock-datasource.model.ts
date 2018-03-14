import { DataSource } from '@angular/cdk/table';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/observable/of';
import { Stock } from './stock.model';

export class StocksDataSource extends DataSource<Stock> {
  stocks: Array<Stock>;

  constructor(stocks: Array<Stock>) {
    super();

    this.stocks = stocks;
  }

  disconnect() {}

  connect(): Observable<Stock[]> {
      return Observable.of(this.stocks);
  }
}
