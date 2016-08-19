using Hermes;
using OperatorSQL.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox.Examples
{
    public class OperatorSQLExample
    {
        readonly string SELECT_ALL_COMPANIES = @"
            SELECT TOP 1000 [Id]
              ,[Name]
              ,[NIP]
              ,[Phonenumber]
              ,[Email]
              ,[PasswordHash]
              ,[BranchId]
              ,[Adress_Id]
          FROM [PromosDB].[dbo].[CompanySet]
";
        readonly string _con = @"Data Source=RANDZIU-KOMP;Initial Catalog=PromosDB;Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        readonly string COLUMNS_MAIN = @"dc.[Id]
              ,dc.[Name]
              ,[NIP]
              ,[Phonenumber]
              ,[Email]
              ,[PasswordHash]
              ,[BranchId]
              ,[Adress_Id]";
        readonly string COLUMNS_ADRESS = @"da.Country";
        readonly string COLUMNS_BRANCH = @"db.Description";

        readonly string FROM_A = @"[PromosDB].[dbo].[CompanySet] dc";

        readonly string JOIN_ADRESS = 
            @"
              join dbo.AdressSet da on da.Id = dc.Adress_Id
              ,join dbo.BranchSet db on db.Id = dc.BranchId
";
        readonly string WHERE_NAME = @"name = @name";
        readonly string WHERE_ADRESS = @"Adress_Id = @adress";


        public string Select(string name, long adress)
        {
            var quary = new Query();
            quary.Select().Columns(COLUMNS_MAIN).From(FROM_A).Join(JOIN_ADRESS);
            if (name != null)
                quary.Columns(COLUMNS_MAIN);
            return quary.Build();
        }

        public void Run()
        {
            string g;
            using (new SpeedTimer())
            {
                g = Select("Maltex", 1);

            }
            using (new SpeedTimer())
            {
                var quary = new Query();
                quary.Select().Columns(COLUMNS_MAIN+ COLUMNS_MAIN).From(FROM_A).Join(JOIN_ADRESS);
                //if (name != null)
                    quary.Columns(COLUMNS_MAIN);
                g = quary.Build();

            }

            //quary.AddColumns()
            var conn = new SqlConnection(_con);
            var q = new object[] { "ads", 44, "asd" }.AsQueryable();
            var asd = q.Where(x => x.ToString() == "asd").ToString() ;
            using (var con = new SqlChannel(_con))
            {
                var reader = con.Send(SELECT_ALL_COMPANIES);
                //var query = new SqlCommand(SELECT_ALL_COMPANIES, con);
                //var reader = query.ExecuteReader();
                var s = reader["Id"];
                while (reader.Read())
                {
                    Console.WriteLine("Start");
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        if (!reader.IsDBNull(i))
                        {
                            
                            var ctype = reader.GetFieldType(i);
                            Console.WriteLine(reader.GetValue(i).ToString());
                        }

                    }
                    Console.WriteLine("End");
                }
            }
                //con.Open();
           
        }
    }
}
