using System.Data;

namespace VfpReaderSchemaExample.Providers.Interfaces {
    public interface ISchemaProvider {
        DataSet GetSchema(string tableName);
    }
}