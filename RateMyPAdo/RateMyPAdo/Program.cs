using System;
using System.Data;
using System.Data.SqlClient;

namespace RateMyPAdo
    {
    public class Program
        {
        private static string s_connectionString =
            "Data Source=ratemyp.database.windows.net;Initial Catalog=RateMyP;User Id=koldunai; Password=abrikosas79?;MultipleActiveResultSets=true;";

        public static void Main(string[] args)
            {
            Select();
            SelectWithSelectCommand();
            Insert();
            Update();
            Delete();
            Console.ReadLine();
            }

        private static void PrintDataSet(DataSet dataSet)
            {
            foreach (DataColumn column in dataSet.Tables[0].Columns)
                Console.Write(column.ColumnName + " ");
            Console.WriteLine();
            foreach (DataRow row in dataSet.Tables[0].Rows)
                {
                foreach (var item in row.ItemArray)
                    Console.Write(item + " ");
                Console.WriteLine();
                }
            }

        public static void Select()
            {
            var dataSet = new DataSet();

            using (var connection = new SqlConnection { ConnectionString = s_connectionString })
            using (var dataAdapter = new SqlDataAdapter("SELECT Top 10 TeacherId, OverallMark, LevelOfDifficulty FROM Ratings", connection))
                {
                dataAdapter.Fill(dataSet, "Rating");
                }

            PrintDataSet(dataSet);
            Console.WriteLine();
            }

        public static void SelectWithSelectCommand()
            {
            var dataSet = new DataSet();
            using (var connection = new SqlConnection { ConnectionString = s_connectionString })
            using (var dataAdapter = new SqlDataAdapter())
                {
                var command = new SqlCommand("SELECT Top 10 FirstName, LastName, Rank FROM Teachers WHERE FirstName = @FN", connection);

                command.Parameters.AddWithValue("@FN", "Antanas");
                dataAdapter.SelectCommand = command;
                dataAdapter.Fill(dataSet, "Teacher");
                }

            PrintDataSet(dataSet);
            Console.WriteLine();
            }

        public static void Insert()
            {
            using (var connection = new SqlConnection { ConnectionString = s_connectionString })
            using (var dataAdapter = new SqlDataAdapter("SELECT Type, Text FROM Tags", connection))
                {
                connection.Open();

                var dataSet = new DataSet();
                dataAdapter.Fill(dataSet, "Tag");
                PrintDataSet(dataSet); // before
                Console.WriteLine();

                var insert = new SqlCommand
                    {
                    Connection = connection,
                    CommandType = CommandType.Text,
                    CommandText = "INSERT INTO Tags (Id, Text, Type) VALUES (@ID,@TEXT,@TYPE)"
                    };

                insert.Parameters.AddWithValue("@ID", Guid.NewGuid());
                insert.Parameters.AddWithValue("@TEXT", "Tipo naujas tagas");
                insert.Parameters.AddWithValue("@TYPE", 0);
                insert.ExecuteNonQuery();
                PrintDataSet(dataSet); // after
                Console.WriteLine();
                }
            }

        public static void Update()
            {
            using (var connection = new SqlConnection { ConnectionString = s_connectionString })
            using (var dataAdapter = new SqlDataAdapter("SELECT Type, Text FROM Tags", connection))
                {
                connection.Open();

                var update = new SqlCommand
                    {
                    Connection = connection,
                    CommandType = CommandType.Text,
                    CommandText = "UPDATE Tags SET Text = @Text WHERE Type = @Type"
                    };

                update.Parameters.AddWithValue("@TEXT", "Atnaujintas tekstas");
                update.Parameters.AddWithValue("@TYPE", 0);
                update.ExecuteNonQuery();

                var dataSet = new DataSet();
                dataAdapter.Fill(dataSet, "Tag");
                PrintDataSet(dataSet);
                Console.WriteLine();
                }
            }

        public static void Delete()
            {
            using (var connection = new SqlConnection { ConnectionString = s_connectionString })
            using (var dataAdapter = new SqlDataAdapter("SELECT Type, Text FROM Tags", connection))
                {
                connection.Open();

                var update = new SqlCommand
                    {
                    Connection = connection,
                    CommandType = CommandType.Text,
                    CommandText = "DELETE FROM Tags WHERE Type = @Type"
                    };

                update.Parameters.AddWithValue("@TYPE", 0);
                update.ExecuteNonQuery();

                var dataSet = new DataSet();
                dataAdapter.Fill(dataSet, "Tag");
                PrintDataSet(dataSet);
                Console.WriteLine();
                }
            }
        }
    }