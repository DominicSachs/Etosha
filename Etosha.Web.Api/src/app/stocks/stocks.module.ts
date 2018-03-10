import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { StocksBoardComponent } from './stocks-board.component';
import { StocksTickerComponent } from './ticker/stocks-ticker.component';
import { StocksTickerItemComponent } from './ticker/stocks-ticker-item.component';

@NgModule({
  imports: [
    CommonModule
  ],
  declarations: [StocksBoardComponent, StocksTickerComponent, StocksTickerItemComponent]
})
export class StocksModule { }
