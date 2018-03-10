import { Component, Input } from '@angular/core';
import { Stock } from '../stock.model';

@Component({
  selector: 'stocks-ticker-item',
  templateUrl: './stocks-ticker-item.component.html',
  styleUrls: ['./stocks-ticker-item.component.scss']
})
export class StocksTickerItemComponent {
    @Input()
    stock: Stock;
}
