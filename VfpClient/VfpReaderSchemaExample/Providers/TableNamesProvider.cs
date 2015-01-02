using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using VfpClient;
using VfpReaderSchemaExample.Providers.Interfaces;

namespace VfpReaderSchemaExample.Providers {
    public class TableNamesProvider : ITableNamesProvider {
        public IEnumerable<string> GetTableNames() {
            using (var connection = new VfpConnection(ConfigurationManager.ConnectionStrings["Northwind"].ConnectionString)) {
                connection.Open();

                var tableNames = connection.GetSchema(VfpConnection.SchemaNames.Tables)
                                           .AsEnumerable()
                                           .Select(x => x.Field<string>(VfpConnection.SchemaColumnNames.Table.TableName))
                                           .OrderBy(x => x)
                                           .ToArray();

                connection.Close();

                return tableNames;
            }
        }
    }
}