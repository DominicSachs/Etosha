import { Component, Input } from '@angular/core';
import { Stock } from '../stock.model';

@Component({
  selector: 'stocks-ticker',
  templateUrl: './stocks-ticker.component.html',
  styleUrls: ['./stocks-ticker.component.scss']
})
export class StocksTickerComponent {
    @Input()
    stocks: Array<Stock>;
}
