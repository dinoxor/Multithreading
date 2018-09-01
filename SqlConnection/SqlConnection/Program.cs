using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SqlConnection
{
    class Program
    {
        static void Main(string[] args)
        {
            Debug.WriteLine("Hello world");


            var connectionString = "Data Source=DESKTOP-TDB8N4J;Initial Catalog=AdventureWorks2014;Integrated Security=True";

            var query = @"SELECT TOP 10 [AddressID]
                          ,[AddressLine1]
                          ,[AddressLine2]
                          ,[City]
                          ,[StateProvinceID]
                          ,[PostalCode]
                          ,[SpatialLocation]
                          ,[rowguid]
                          ,[ModifiedDate]
                        FROM [AdventureWorks2014].[Person].[Address]";




        }
    }
}
