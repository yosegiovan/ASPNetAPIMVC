using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Repositories.Interfaces
{
    interface ISupplierRepository
    {
        IEnumerable<Supplier> Get();
        Task< IEnumerable<Supplier>> Get(int SupplierID);
        int Create(Supplier s);
        int Update(int SupplierID, string SupplierName);
        int Delete(int SupplierID);
    }
}
