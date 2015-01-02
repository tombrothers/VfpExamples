using System.Configuration;
using System.Data;
using VfpClient;

namespace BulkInsertExample {
    class Program {
        static void Main(string[] args) {
            // Get the Northwind OrderDetails data.
            var orderDetails = GetDataTable("OrderDetails");

            // Convert the DataTable to an XmlToCursor formatted xml string.
            // ToXmlToCursorFormattedXml is an DataTable extension method in the VfpClient namespace.
            var xml = orderDetails.ToXmlToCursorFormattedXml();

            using (var connection = new VfpConnection(ConfigurationManager.ConnectionStrings["FreeTables"].ConnectionString)) {
                connection.Open();

                // Create cursor using XmlToCursor with the DataTable xml.
                using (var command = connection.CreateCommand()) {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "XmlToCursor";
                    command.Parameters.Add(new VfpParameter("xml", xml));
                    command.Parameters.Add(new VfpParameter("cursor", "curXmlTemp"));
                    command.ExecuteNonQuery();
                }

                // Use the cursor to insert records into the destination table.
                using (var command = connection.CreateCommand()) {
                    command.CommandText = "INSERT INTO 'OrderDetailsArchive' SELECT * FROM curXmlTemp";
                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
        }

        private static DataTable GetDataTable(string tableName) {
            var dataTable = new DataTable(tableName);

            using (var connection = new VfpConnection(ConfigurationManager.ConnectionStrings["Northwind"].ConnectionString)) {
                using (var command = connection.CreateCommand()) {
                    command.CommandText = tableName;
                    command.CommandType = CommandType.TableDirect;

                    var adapter = new VfpDataAdapter(command);

                    adapter.Fill(dataTable);
                }
            }

            return dataTable;
        }
    }
}