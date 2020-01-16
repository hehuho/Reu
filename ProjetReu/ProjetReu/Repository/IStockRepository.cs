using ProjetReu.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetReu.Repository
{
    public interface IStockRepository
    {
        void AddStock(Stock stock);
        List<Stock> getListStock();
        void UpdateStock(Stock stock);
    }
}
