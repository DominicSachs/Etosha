using Etosha.Server.Common.Models;
using Etosha.Web.Api.Infrastructure.SampleData;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Threading.Channels;
using System.Threading.Tasks;
using Etosha.Web.Api.Extensions;

namespace Etosha.Web.Api.Hubs
{
  public class StockTickerHub : Hub
  {
    private readonly StockTicker _stockTicker;

    public StockTickerHub(StockTicker stockTicker)
    {
      _stockTicker = stockTicker;
    }

    public IEnumerable<Stock> GetAllStocks()
    {
      return _stockTicker.GetAllStocks();
    }

    public ChannelReader<Stock> StreamStocks()
    {
      return _stockTicker.StreamStocks().AsChannelReader();
    }

    public string GetMarketState()
    {
      return _stockTicker.MarketState.ToString();
    }

    public async Task OpenMarket()
    {
      await _stockTicker.OpenMarket();
    }

    public async Task CloseMarket()
    {
      await _stockTicker.CloseMarket();
    }

    public async Task Reset()
    {
      await _stockTicker.Reset();
    }
  }
}
