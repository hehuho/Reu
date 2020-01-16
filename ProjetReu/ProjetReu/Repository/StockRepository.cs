using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjetReu.Models;

namespace ProjetReu.Repository
{
    public class StockRepository : IStockRepository
    {
        private ReuContext _reuContext { get; set; }

        public StockRepository(ReuContext reuContext)
        {
            _reuContext = reuContext;
        }

        public void AddStock(Stock stock)
        {
            _reuContext.Add(stock);
            _reuContext.SaveChanges();
        }

        public List<Stock> getListStock()
        {
            return _reuContext.Stocks.ToList();
        }

        public void UpdateStock(Stock stock)
        {
            _reuContext.Update(stock);
            _reuContext.SaveChanges();
        }
    }
}
