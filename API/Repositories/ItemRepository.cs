using API.Contexts;
using API.Models;
using API.Repositories.Interfaces;
using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace API.Repositories
{
    public class ItemRepository : IItemRepository
    {
        DynamicParameters parameters = new DynamicParameters();
        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["myConnection"].ConnectionString);

        public int Create(Item i)
        {
            //throw new NotImplementedException();
            var SP_Name = "SP_InsertItem";
            parameters.Add("@ItemName", i.ItemName);
            parameters.Add("@Quantity", i.Quantity);
            parameters.Add("@Price", i.Price);
            parameters.Add("@SupplierID", i.SupplierID);

            var create = connection.Execute(SP_Name, parameters, commandType: CommandType.StoredProcedure);

            return create;
        }

        public int Delete(int ItemID)
        {
            //throw new NotImplementedException();
            var SP_Name = "SP_DeleteItemByID";
            parameters.Add("@ItemID", ItemID);

            var delete = connection.Execute(SP_Name, parameters, commandType: CommandType.StoredProcedure);

            return delete;
        }

        public IEnumerable<Item> Get()
        {
            //throw new NotImplementedException();
            List<Item> ie = new List<Item>();
            connection.Open();

            SqlCommand command = new SqlCommand("EXEC SP_RetrieveItemAll", connection);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                //Console.WriteLine(String.Format("{0}", reader[0]));
                ie.Add(new Item()
                {
                    ItemID = Convert.ToInt32(String.Format("{0}", reader[0])),
                    ItemName = String.Format("{0}", reader[1]),
                    Quantity = Convert.ToInt32(String.Format("{0}", reader[2])),
                    Price = Convert.ToInt32(String.Format("{0}", reader[3])),
                    SupplierID = Convert.ToInt32(String.Format("{0}", reader[4]))
                });
            }

            //reader.Close();
            //command.Dispose();
            //connection.Close();
            return ie;
        }

        public Task<IEnumerable<Item>> Get(int ItemID)
        {
            //throw new NotImplementedException();
            var SP_Name = "SP_RetrieveItemByID";
            parameters.Add("@ItemID", ItemID);
            var getItemByID = connection.QueryAsync<Item>(SP_Name, parameters, commandType: CommandType.StoredProcedure);
            return getItemByID;
        }

        public int Update(int ItemID, string ItemName, int Quantity, int Price, int SupplierID)
        {
            //throw new NotImplementedException();
            var SP_Name = "SP_UpdateItemAll";
            parameters.Add("@ItemID", ItemID);
            parameters.Add("@ItemName", ItemName);
            parameters.Add("@Quantity", Quantity);
            parameters.Add("@Price", Price);
            parameters.Add("@SupplierID", SupplierID);

            var update = connection.Execute(SP_Name, parameters, commandType: CommandType.StoredProcedure);

            return update;
        }
    }
}