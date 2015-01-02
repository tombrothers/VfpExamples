using System;
using System.Configuration;
using System.IO;
using VfpClient;
using VfpClient.Utils.DbcCreator;

namespace DataTableDbcCreatorExample {
    internal class Program {
        private static void Main(string[] args) {
            using (var connection = new VfpConnection(ConfigurationManager.ConnectionStrings["Northwind"].ConnectionString)) {
                connection.Open();

                var tables = connection.GetSchema(VfpConnection.SchemaNames.Tables);
                var fields = connection.GetSchema(VfpConnection.SchemaNames.TableFields);

                connection.Close();

                var dbc = GetNewDbcFullPath();
                var dbcCreator = new DataTableDbcCreator(dbc);

                dbcCreator.Add(tables);
                dbcCreator.Add(fields);
            }
        }

        private static string GetNewDbcFullPath() {
            var directory = Path.Combine(Environment.CurrentDirectory, Guid.NewGuid().ToString());

            Directory.CreateDirectory(directory);

            return Path.Combine(directory, "Example.dbc");
        }
    }
}