using NUnit.Framework;
using static RateMyP.Constants;

namespace RateMyP.Tests
    {
    public class RateMyPFixture
        {
        private static readonly object s_databaseLock = new object();

        [OneTimeSetUp]
        public void OneTimeSetUp()
            {
            PrepareDb();
            }

        protected static void PrepareDb()
            {
            lock (s_databaseLock)
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
    }
