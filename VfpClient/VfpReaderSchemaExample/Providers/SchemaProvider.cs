using System.Configuration;
using System.Data;
using System.Data.OleDb;
using VfpClient;
using VfpReaderSchemaExample.Providers.Interfaces;

namespace VfpReaderSchemaExample.Providers {
    public class SchemaProvider : ISchemaProvider {
        public const string OleDbSchemaTableName = "OleDbSchema";
        public const string VfpSchemaTableName = "VfpSchema";

        public DataSet GetSchema(string tableName) {
            var schema = new DataSet();

            using (var connection = GetConnection()) {
                connection.Open();

                using (var command = connection.CreateCommand()) {
                    command.CommandText = "Select * from " + tableName;

                    using (var reader = command.ExecuteReader()) {
                        var oleDbSchema = reader.GetSchemaTable();

                        oleDbSchema.TableName = OleDbSchemaTableName;

                        schema.Tables.Add(oleDbSchema.DefaultView.ToTable());

                        var vfpDataReader = new VfpDataReader(reader);
                        var vfpSchema = vfpDataReader.GetSchemaTable();

                        vfpSchema.TableName = VfpSchemaTableName;

                        schema.Tables.Add(vfpSchema.DefaultView.ToTable());
                    }
                }

                connection.Close();
            }

            return schema;
        }

        private static OleDbConnection GetConnection() {
            var connectionString = "provider=vfpoledb;data source=" + ConfigurationManager.ConnectionStrings["Northwind"].ConnectionString;

            return new OleDbConnection(connectionString);
        }
    }
}