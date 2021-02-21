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
    public class SupplierRepository : ISupplierRepository
    {
        MyContext myContext = new MyContext();
        DynamicParameters parameters = new DynamicParameters();
        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["myConnection"].ConnectionString);
        public int Create(Supplier s)
        {
            //throw new NotImplementedException();
            var SP_Name = "SP_InsertSupplier";
            parameters.Add("@SupplierName", s.SupplierName);

            var create = connection.Execute(SP_Name, parameters, commandType: CommandType.StoredProcedure);

            return create;
        }

        public int Delete(int SupplierID)
        {
            //throw new NotImplementedException();
            var SP_Name = "SP_DeleteSupplier";
            parameters.Add("@SupplierID", SupplierID);

            var delete = connection.Execute(SP_Name, parameters, commandType: CommandType.StoredProcedure);

            return delete;
        }

        public IEnumerable<Supplier> Get()
        {
            //throw new NotImplementedException();
            //List<Supplier> ie = new List<Supplier>();
            //connection.Open();

            //SqlCommand command = new SqlCommand("EXEC SP_RetrieveSupplierAll", connection);
            //SqlDataReader reader = command.ExecuteReader();
            //while (reader.Read())
            //{
            //    //Console.WriteLine(String.Format("{0}", reader[0]));
            //    ie.Add(new Supplier() { SupplierID = Convert.ToInt32(String.Format("{0}", reader[0])),
            //    SupplierName = String.Format("{0}", reader[1]) } );
            //}

            var SP_Name = "SP_RetrieveSupplierAll";
            var result = connection.Query<Supplier>(SP_Name, commandType: CommandType.StoredProcedure);
            return result;

            //return ie;
        }

        public async Task<IEnumerable<Supplier>> Get(int SupplierID)
        {
            //throw new NotImplementedException();
            var SP_Name = "SP_RetrieveSupplierByID";
            parameters.Add("@SupplierID", SupplierID);
            var getSupplierByID = await connection.QueryAsync<Supplier>(SP_Name, parameters, commandType: CommandType.StoredProcedure);
            return getSupplierByID;
        }

        public int Update(int SupplierID, string SupplierName)
        {
            //throw new NotImplementedException();
            var SP_Name = "SP_UpdateSupplier";
            parameters.Add("@SupplierID", SupplierID);
            parameters.Add("@SupplierName", SupplierName);

            var update = connection.Execute(SP_Name, parameters, commandType: CommandType.StoredProcedure);

            return update;
        }
    }
}