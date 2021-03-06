import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { HubConnection, HubConnectionBuilder, LogLevel } from '@aspnet/signalr';
import { environment } from '../../environments/environment';
import { Stock } from './stock.model';

@Component({
  selector: 'app-stocks-board',
  templateUrl: './stocks-board.component.html',
  styleUrls: ['./stocks-board.component.scss']
})
export class StocksBoardComponent implements OnInit {
  private hubConnection: HubConnection;
  displayedColumns = ['symbol', 'price', 'dayHigh', 'dayLow', 'change', 'percentChange'];
  stocks: Stock[];
  dataSource: MatTableDataSource<Stock>;

  ngOnInit() {
    this.hubConnection = new HubConnectionBuilder()
        .withUrl(environment.webSocketEndpoint)
        .configureLogging(LogLevel.Information)
        .build();

    this.hubConnection
      .start()
      .then(() => {
        this.stocks = [];
        this.hubConnection.invoke('GetMarketState').then((state: string) => {
          if (state === 'Open') {
            this.streamStocks();
          } else {
            this.hubConnection.invoke('OpenMarket');
          }
        });
      });

    this.hubConnection.on('marketOpened', () => {
        this.streamStocks();
      });
  }

  private streamStocks(): void {
    this.hubConnection.stream('StreamStocks').subscribe({
      next: (stock: any) => {
        this.addOrUpdate(stock);
      },
      error: () => { },
      complete: () => { }
    });
  }

  private addOrUpdate(stock: Stock): void {
    const index = this.stocks.findIndex(s => s.symbol === stock.symbol);

    if (index === -1) {
      this.stocks.push(stock);
    } else {
      stock.hasChanged = this.stocks[index].lastChange !== stock.lastChange;
      this.stocks[index] = stock;
    }

    this.stocks.sort((a: Stock, b: Stock) => a.symbol.localeCompare(b.symbol));
    this.dataSource = new MatTableDataSource(this.stocks);
  }
}
