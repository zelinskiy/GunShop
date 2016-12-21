using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace GunShop.Utils
{
    public class SqlMaker
    {

        private readonly string _connectionString;

        public SqlMaker(string connectionString)
        {
            _connectionString = connectionString;
        }

        public bool TryExecuteQuery(string query, ref string output)
        {
            using (var con = new SqlConnection(_connectionString))
            {
                con.Open();
                try
                {
                    var command = new SqlCommand(query, con);
                    using (command)
                    {
                        var result = command.ExecuteReader();
                        var heading = string.Join(",",Enumerable.Range(0, result.FieldCount)
                            .Select(i => result.GetName(i)))+"\n";

                        var rows = ReadAllRows(result);
                        output = heading + string.Join("",RowsToStrings(rows));
                        return true;
                    }
                }
                catch (SqlException ex)
                {
                    output = ex.Message;
                    return false;
                }
            }
        }


        private static IEnumerable<IDataRecord> ReadAllRows(SqlDataReader reader)
        {
            using (reader)
            {
                while (reader.Read())
                {
                    yield return reader;
                }
            }
        }

        private static IEnumerable<string> RowsToStrings(IEnumerable<IDataRecord> rows)
        {
            foreach (var r in rows)
            {
                var t = new List<string>();
                for (int i = 0; i < r.FieldCount; i++)
                {
                    t.Add(r[i].ToString());
                }
                yield return $"{string.Join(",", t)}\n";
            }
        }


    }
}
