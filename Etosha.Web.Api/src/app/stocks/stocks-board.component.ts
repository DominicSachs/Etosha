import { Component, OnInit } from '@angular/core';
import { HubConnection } from '@aspnet/signalr-client';
import { environment } from '../../environments/environment';

@Component({
  selector: 'app-stocks-board',
  templateUrl: './stocks-board.component.html',
  styleUrls: ['./stocks-board.component.scss']
})
export class StocksBoardComponent implements OnInit {
  private hubConnection: HubConnection;

  constructor() { }

  ngOnInit() {
    this.hubConnection = new HubConnection(environment.webSocketEndpoint);

    this.hubConnection
          .start()
          .then(() => console.log('Connection started!'))
          .catch(err => console.log('Error while establishing connection :('));
  }
}
