using System;
using System.Configuration;
using System.Data.OleDb;
using VfpClient;

namespace VfpReaderExample {
    class Program {
        static void Main(string[] args) {
            Console.ForegroundColor = ConsoleColor.White;
            NumericAsIntTest();
            Console.WriteLine("");

            BoolTest();
            Console.WriteLine("");

            TrimEndTest();
            Console.WriteLine("");

            BinaryVarCharTest();

            Console.ReadKey();
        }

        private static void BinaryVarCharTest() {
            using (var connection = CreateConnection()) {
                connection.Open();

                using (var command = connection.CreateCommand()) {
                    command.CommandText = "select Name from BinaryVarchar";
                    Console.WriteLine(command.CommandText);
                    var oleDbReader = command.ExecuteReader();
                    oleDbReader.Read();

                    var result = oleDbReader.GetValue(0);
                    Console.WriteLine("OleDb Result Type is:  " + result.GetType().FullName);

                    try {
                        var value = oleDbReader.GetString(0);
                        Console.WriteLine("Value from OleDbDataReader:  '" + value + "'");
                    }
                    catch (InvalidCastException ex) {
                        DisplayException(ex);
                    }

                    var vfpDataReader = new VfpDataReader(oleDbReader);
                    Console.WriteLine("Value from VfpDataReader:  '" + vfpDataReader.GetString(0) + "'");
                }

                connection.Close();
            }
        }

        private static void TrimEndTest() {
            using (var connection = CreateConnection()) {
                connection.Open();

                using (var command = connection.CreateCommand()) {
                    command.CommandText = "select 'Test' + space(10) Column1 from SingleColumnSingleRow";
                    Console.WriteLine(command.CommandText);
                    var oleDbReader = command.ExecuteReader();
                    oleDbReader.Read();

                    Console.WriteLine("Value from OleDbDataReader:  '" + oleDbReader.GetString(0) + "'");

                    var vfpDataReader = new VfpDataReader(oleDbReader);
                    Console.WriteLine("Value from VfpDataReader:  '" + vfpDataReader.GetString(0) + "'");
                }

                connection.Close();
            }
        }

        private static void BoolTest() {
            using (var connection = CreateConnection()) {
                connection.Open();

                using (var command = connection.CreateCommand()) {
                    command.CommandText = "select 1 Column1 from SingleColumnSingleRow";
                    Console.WriteLine(command.CommandText);
                    var oleDbReader = command.ExecuteReader();
                    oleDbReader.Read();

                    var result = oleDbReader.GetValue(0);
                    Console.WriteLine("Result Type is:  " + result.GetType().FullName);

                    try {
                        Console.WriteLine("Value from OleDbDataReader:  " + oleDbReader.GetBoolean(0));
                    }
                    catch (InvalidCastException ex) {
                        DisplayException(ex);
                    }

                    var vfpDataReader = new VfpDataReader(oleDbReader);
                    Console.WriteLine("Value from VfpDataReader:  " + vfpDataReader.GetBoolean(0));
                }

                connection.Close();
            }
        }

        private static void NumericAsIntTest() {
            using (var connection = CreateConnection()) {
                connection.Open();

                using (var command = connection.CreateCommand()) {
                    command.CommandText = "select cast(1 as n(10)) Column1 from SingleColumnSingleRow";
                    Console.WriteLine(command.CommandText);
                    var oleDbReader = command.ExecuteReader();
                    oleDbReader.Read();

                    // Result type is Decimal.
                    var result = oleDbReader.GetValue(0);
                    Console.WriteLine("OleDb Result Type is:  " + result.GetType().FullName);

                    try {
                        // The OleDbDataReader will not let me retrieve the value as an integer.
                        var value = oleDbReader.GetInt32(0);
                        Console.WriteLine("Value from OleDbDataReader:  " + value);
                    }
                    catch (InvalidCastException ex) {
                        DisplayException(ex);
                    }

                    var vfpDataReader = new VfpDataReader(oleDbReader);
                    // Result type is Decimal.
                    var result2 = vfpDataReader.GetValue(0);
                    Console.WriteLine("VfpClient Result Type is:  " + result2.GetType().FullName);

                    var value2 = vfpDataReader.GetInt32(0);
                    Console.WriteLine("Value from VfpDataReader:  " + value2);
                }

                connection.Close();
            }
        }

        private static void DisplayException(Exception ex) {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(ex.Message);
            Console.ForegroundColor = ConsoleColor.White;
        }

        private static OleDbConnection CreateConnection() {
            var connectionString = "provider=vfpoledb;data source=" + ConfigurationManager.ConnectionStrings["FreeTables"].ConnectionString;

            return new OleDbConnection(connectionString);
        }
    }
}