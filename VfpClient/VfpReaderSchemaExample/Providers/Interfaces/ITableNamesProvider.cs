using System.Collections.Generic;

namespace VfpReaderSchemaExample.Providers.Interfaces {
    public interface ITableNamesProvider {
        IEnumerable<string> GetTableNames();
    }
}