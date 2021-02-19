using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Repositories.Interfaces
{
    interface IItemRepository
    {
        IEnumerable<Item> Get();
        Task<IEnumerable<Item>> Get(int ItemID);
        int Create(Item i);
        int Update(int ItemID, string ItemName, int Quantity, int Price, int SupplierID);
        int Delete(int ItemID);
    }
}
