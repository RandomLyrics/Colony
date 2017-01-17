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
    ,[Adress_Id] AdressNr
FROM [PromosDB].[dbo].[CompanySet]
where BranchId = @BranchNr
";
        readonly string _con = @"Data Source=RANDZIU-KOMP;Initial Catalog=PromosDB;Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        readonly string COLUMNS_MAIN = @"dc.[Id]
              ,dc.[Name]
              ,[NIP]
              ,[Phonenumber]
              ,[Email]
              ,[PasswordHash]
              ,[BranchId] BranchNr
              ,[Adress_Id] AdressNr";
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


        //public void Select(string name, long adress)
        //{
        //    var quary = new Query();
        //   // quary.Select().Columns(COLUMNS_MAIN).From(FROM_A).Join(JOIN_ADRESS).W;
        //    //if (name != null)
        //    //    quary.Columns(COLUMNS_MAIN);
        //    var s = quary.Build();
        //}

        public void Run()
        {

            // var list = 
            var branchnr = 4;
            List<Company> list;
            using (var conn = new SqlChannel(_con))
            {
                var qr = new Query(SELECT_ALL_COMPANIES)
                    .AddParam("@BranchNr", SqlDbType.Int, branchnr);
                list = conn.GetRecords<Company>(qr);
            }

            //var con = new SqlConnection(_con);
            
            //var comnd = new SqlCommand(SELECT_ALL_COMPANIES, con);
            //comnd.pa
            //con.Open();


            //var reader = comnd.ExecuteReader();
            //var listobj = Mapper.FromReader<Company>(reader);

            //con.Close();
            //using (var con = new SqlChannel(_con))
            //{
                
            //}
           
        }
    }
}
