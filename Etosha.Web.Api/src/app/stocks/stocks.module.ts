import { NgModule } from '@angular/core';
import { SharedModule } from '../shared/modules/shared.module';
import { StocksBoardComponent } from './stocks-board.component';
import { StocksTickerComponent } from './ticker/stocks-ticker.component';
import { StocksTickerItemComponent } from './ticker/stocks-ticker-item.component';

@NgModule({
  imports: [
    SharedModule
  ],
  declarations: [StocksBoardComponent, StocksTickerComponent, StocksTickerItemComponent]
})
export class StocksModule { }
