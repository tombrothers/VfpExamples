using System;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using VfpClient;

namespace ArrayXmlToCursorExample {
    internal class Program {
        private static void Main(string[] args) {
            // Simulating getting a list of Order Ids from another source.
            var orderIds = Enumerable.Range(10000, 1000).ToArray();
            
            //QueryByLargeNumberOfIdsWithoutXmlToCursor(orderIds);
            QueryByLargeNumberOfIdsWithXmlToCursor(orderIds);
        }

        private static void QueryByLargeNumberOfIdsWithXmlToCursor(int[] orderIds) {
            Console.WriteLine("Running:  {0}", MethodBase.GetCurrentMethod().Name);

            // ToXmlToCursorFormattedXml is an Array extension method in the VfpClient namespace.
            var xml = orderIds.ToXmlToCursorFormattedXml();

            using (var connection = new VfpConnection(ConfigurationManager.ConnectionStrings["Northwind"].ConnectionString)) {
                using (var command = connection.CreateCommand()) {

                    command.CommandText = @"
    select o.OrderId, c.CustomerId, c.CompanyName
        from Orders o
        inner join Customers c on upper(allt(o.CustomerId)) == upper(allt(c.CustomerId))
        where o.OrderId in (select Id from (iif(XmlToCursor(@OrderIdsXml, 'curTempIdList') > 0, 'curTempIdList', '')))";

                    command.Parameters.AddWithValue("@OrderIdsXml", xml);

                    var dataAdapter = new VfpDataAdapter(command);
                    var dataTable = new DataTable("Table1");

                    dataAdapter.Fill(dataTable);
                }
            }
        }

        private static void QueryByLargeNumberOfIdsWithoutXmlToCursor(int[] orderIds) {
            Console.WriteLine("Running:  {0}", MethodBase.GetCurrentMethod().Name);

            using (var connection = new VfpConnection(ConfigurationManager.ConnectionStrings["Northwind"].ConnectionString)) {
                using (var command = connection.CreateCommand()) {
                    command.CommandText = @"
    select o.OrderId, c.CustomerId, c.CompanyName
        from Orders o
        inner join Customers c on upper(allt(o.CustomerId)) == upper(allt(c.CustomerId))
        where o.OrderId in (" + string.Join(",", orderIds) + ")";

                    var dataAdapter = new VfpDataAdapter(command);
                    var dataTable = new DataTable("Table1");

                    dataAdapter.Fill(dataTable);
                }
            }
        }

        private static void DisplayException(Exception ex) {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(ex.Message);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}