using System.Configuration;
using System.Data;
using System.Data.OleDb;
using VfpClient;

namespace NamedParameterExample {
    internal class Program {
        private static void Main(string[] args) {
            VfpClientVersion();
            //OleDbVersion();
        }

        private static void VfpClientVersion() {
            using (var connection = new VfpConnection(ConfigurationManager.ConnectionStrings["Northwind"].ConnectionString)) {
                using (var command = connection.CreateCommand()) {

                    command.CommandText = @"
SELECT CAST('Supplier' as v(254)) Type, CompanyName Name 
    FROM Suppliers WHERE UPPER(ALLTRIM(CompanyName)) like @Name OR SupplierId = @Id
UNION SELECT CAST('Shipper' as v(254)), CompanyName 
        FROM Shippers WHERE UPPER(ALLTRIM(CompanyName)) like @Name OR ShipperId = @Id
UNION SELECT CAST('Customer' as v(254)), CompanyName 
        FROM Customers WHERE UPPER(ALLTRIM(CompanyName)) like @Name
UNION SELECT CAST('Product' as v(254)), ProductName 
        FROM Products WHERE UPPER(ALLTRIM(ProductName)) like @Name OR ProductId = @Id
UNION SELECT CAST('Category' as v(254)), CategoryName 
        FROM Categories WHERE UPPER(ALLTRIM(CategoryName)) like @Name OR CategoryId = @Id
UNION SELECT CAST('Employee' as v(254)), ALLTRIM(FirstName) + ' ' + ALLTRIM(LastName) 
        FROM Employees WHERE UPPER(ALLTRIM(FirstName)) like @Name OR UPPER(ALLTRIM(LastName)) like @Name OR EmployeeId = @Id
Order by 2
";

                    command.Parameters.AddWithValue("@Name", "%AB%");
                    command.Parameters.AddWithValue("@Id", 1);

                    var dataAdapter = new VfpDataAdapter(command);
                    var dataTable = new DataTable();

                    dataAdapter.Fill(dataTable);
                }
            }
        }

        private static void OleDbVersion() {
            using (var connection = new OleDbConnection("provider=vfpoledb;data source=" + ConfigurationManager.ConnectionStrings["Northwind"].ConnectionString)) {
                using (var command = connection.CreateCommand()) {

                    command.CommandText = @"
SELECT CAST('Supplier' as v(254)) Type, CompanyName Name 
    FROM Suppliers WHERE UPPER(ALLTRIM(CompanyName)) like ? OR SupplierId = ?
UNION SELECT CAST('Shipper' as v(254)), CompanyName 
        FROM Shippers WHERE UPPER(ALLTRIM(CompanyName)) like ? OR ShipperId = ?
UNION SELECT CAST('Customer' as v(254)), CompanyName 
        FROM Customers WHERE UPPER(ALLTRIM(CompanyName)) like ?
UNION SELECT CAST('Product' as v(254)), ProductName 
        FROM Products WHERE UPPER(ALLTRIM(ProductName)) like ? OR ProductId = ?
UNION SELECT CAST('Category' as v(254)), CategoryName 
        FROM Categories WHERE UPPER(ALLTRIM(CategoryName)) like ? OR CategoryId = ?
UNION SELECT CAST('Employee' as v(254)), ALLTRIM(FirstName) + ' ' + ALLTRIM(LastName) 
        FROM Employees WHERE UPPER(ALLTRIM(FirstName)) like ? OR UPPER(ALLTRIM(LastName)) like ? OR EmployeeId = ?
Order by 2";

                    command.Parameters.AddWithValue("@Name1", "%AB%");
                    command.Parameters.AddWithValue("@Id1", 1);
                    command.Parameters.AddWithValue("@Name2", "%AB%");
                    command.Parameters.AddWithValue("@Id2", 1);
                    command.Parameters.AddWithValue("@Name3", "%AB%");
                    command.Parameters.AddWithValue("@Name4", "%AB%");
                    command.Parameters.AddWithValue("@Id3", 1);
                    command.Parameters.AddWithValue("@Name5", "%AB%");
                    command.Parameters.AddWithValue("@Id4", 1);
                    command.Parameters.AddWithValue("@Name6", "%AB%");
                    command.Parameters.AddWithValue("@Name7", "%AB%");
                    command.Parameters.AddWithValue("@Id5", 1);

                    var dataAdapter = new OleDbDataAdapter(command);
                    var dataTable = new DataTable();

                    dataAdapter.Fill(dataTable);
                }
            }
        }        
    }
}