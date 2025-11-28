using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;

namespace Documents_Toshmatov.Classes.Common
{
    public class DBConnect
    {
        public static readonly string Path = @"C:\Users\aooshchepkov\Desktop\Pup.accdb";

        public static OleDbConnection Connection()
        {
            OleDbConnection oleDbConnection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + Path);

            oleDbConnection.Open();
            return oleDbConnection;
        }
>
        public static OleDbDataReader Query(string Query, OleDbConnection Connection)
        {
            return new OleDbCommand(Query, Connection).ExecuteReader();
        }

        public static void CloseConnection(OleDbConnection Connection)
        {
            Connection.Close();
        }
    }
}