import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { MaterialModule } from '../shared/modules/material.module';
import { StocksBoardComponent } from './stocks-board.component';
import { StocksTickerItemComponent } from './ticker/stocks-ticker-item.component';
import { StocksTickerComponent } from './ticker/stocks-ticker.component';

@NgModule({
  imports: [
    CommonModule,
    MaterialModule
  ],
  declarations: [StocksBoardComponent, StocksTickerComponent, StocksTickerItemComponent]
})
export class StocksModule { }
