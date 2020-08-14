using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace SSMS2.Infrastructure
{
    public class DbConnection
    {
        public JArray Connection(SqlCommand command) {

            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = "Server = DESKTOP-9I7GF5A;Initial Catalog = StudentDB; Integrated Security = True;";

            connection.Open();

            DataTable dataTable = new DataTable();

            using (SqlDataAdapter adapter = new SqlDataAdapter())
            {
                adapter.SelectCommand = command;
                adapter.SelectCommand.Connection = connection;
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                adapter.Fill(dataTable);
            }

            connection.Close();

            return JArray.FromObject(dataTable);
        }
    }
}
