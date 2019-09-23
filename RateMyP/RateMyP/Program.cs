using System;
using System.Windows.Forms;
using static RateMyP.Constants;

namespace RateMyP.Forms
    {
    static class Program
        {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
            {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
            }

        private static void PrepareDb()
            {
            using (var dbConnection = SQLDbConnection.CreateToDb())
                {
                dbConnection.ExecuteCommand(
                    $"drop table if exists [{TABLE_TEACHERS}];" +
                    $"drop table if exists [{TABLE_STUDENTS}];" +
                    $"drop table if exists [{TABLE_TEACHER_ACTIVITIES}];" +
                    $"drop table if exists [{TABLE_COURSES}];" +
                    $"drop table if exists [{TABLE_COMMENTS}];" +
                    $"drop table if exists [{TABLE_COMMENT_LIKES}];" +
                    $"drop table if exists [{TABLE_RATINGS}]");

                dbConnection.ExecuteScript("InitializeRateMyPCatalog.sql");
                }
            }
        }
    }
