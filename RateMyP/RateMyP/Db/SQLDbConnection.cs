using RateMyP.Exceptions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using RateMyP.Db;
using CsvHelper;
using RateMyP.Entities;
using static RateMyP.Constants;

namespace RateMyP
    {
    public interface ISQLDbConnection : IDisposable
        {
        SqlConnection Connection { get; }
        string SchemaName { get; }

        /// <summary>Executes given query.</summary>
        /// <param name="query">Query string.</param>
        IDataReader ExecuteQuery(string query);

        /// <summary>Executes given non query.</summary>
        /// <param name="query">Query string.</param>
        /// <param name="obj">Temporary parameter to pass object info instead of putting into a string.</param>
        int ExecuteNonQuery(string query, object obj);

        /// <summary>Executes query with given query context.</summary>
        /// <param name="sqlQueryContext">Query context to build a query string.</param>
        IDataReader Query(SQLQueryContext sqlQueryContext);

        /// <summary>Executes query with given filter.</summary>
        /// <param name="table">Table to query.</param>
        /// <param name="selectedProperties">Properties to select.</param>
        IDataReader QueryAll(string table, params string[] selectedProperties);

        /// <summary>Executes query with given filter.</summary>
        /// <param name="table">Table to query.</param>
        /// <param name="filter">Filter string.</param>
        /// <param name="selectedProperties">Properties to select.</param>
        IDataReader QueryByFilter(string table, string filter, params string[] selectedProperties);
        }

    // for now this class does not connect to a database, but only reads from local files
    public class SQLDbConnection : ISQLDbConnection
        {
        public SqlConnection Connection { get; }
        public string SchemaName { get; }

        private bool m_isDisposed = false;
        private readonly string m_ratingsFileName;
        private readonly string m_studentsFileName;
        private readonly string m_teachersFileName;

        public SQLDbConnection()
            {
            var assemblyDirectory = Path.GetDirectoryName(GetType().Assembly.Location);
            var databasePath = Path.Combine(assemblyDirectory, "database");
            m_ratingsFileName = Path.Combine(databasePath, "ratings.csv");
            m_studentsFileName = Path.Combine(databasePath, "students.csv");
            m_teachersFileName = Path.Combine(databasePath, "teachers.csv");

            if (!Directory.Exists (Path.GetDirectoryName (m_ratingsFileName)))
                Directory.CreateDirectory (Path.GetDirectoryName (m_ratingsFileName));
            if (!Directory.Exists(Path.GetDirectoryName(m_studentsFileName)))
                Directory.CreateDirectory(Path.GetDirectoryName(m_studentsFileName));
            if (!Directory.Exists(Path.GetDirectoryName(m_teachersFileName)))
                Directory.CreateDirectory(Path.GetDirectoryName(m_teachersFileName));

            if (!File.Exists(m_ratingsFileName))
                CreateFileWithHeader<Rating>(m_ratingsFileName);
            if (!File.Exists(m_studentsFileName))
                CreateFileWithHeader<Student>(m_ratingsFileName);
            if (!File.Exists(m_teachersFileName))
                CreateFileWithHeader<Teacher>(m_ratingsFileName);
            }

        public void Clear ()
            {
            if (File.Exists (m_ratingsFileName))
                {
                File.Delete(m_ratingsFileName);
                CreateFileWithHeader<Rating>(m_ratingsFileName);
                }

            if (File.Exists(m_studentsFileName))
                {
                File.Delete(m_studentsFileName);
                CreateFileWithHeader<Student>(m_studentsFileName);
                }

            if (File.Exists (m_teachersFileName))
                {
                File.Delete(m_teachersFileName);
                CreateFileWithHeader<Teacher>(m_teachersFileName);
                }
            }

        private static void CreateFileWithHeader<T>(string fileName)
            {
            using (var writer = new StreamWriter(File.Create(fileName)))
            using (var csv = new CsvWriter(writer))
                {
                csv.WriteHeader(typeof(T));
                }
            }

        public void Dispose()
            {
            if (m_isDisposed) return;
            Connection.Close();
            Connection.Dispose();
            m_isDisposed = true;
            GC.SuppressFinalize(this);
            }

        public IDataReader ExecuteQuery(string query)
            {
            string fileName;
            if (query.Equals($"SELECT * FROM [{TABLE_RATINGS}]"))
                fileName = m_ratingsFileName;
            else if (query.Equals($"SELECT * FROM [{TABLE_STUDENTS}]"))
                fileName = m_studentsFileName;
            else if (query.Equals($"SELECT * FROM [{TABLE_TEACHERS}]"))
                fileName = m_teachersFileName;
            else
                return null;

            var reader = new StreamReader(fileName);
            var csvReader = new CsvReader(reader);
            csvReader.Configuration.HasHeaderRecord = true;
            return new CsvDataReader(csvReader);
            }

        public int ExecuteNonQuery(string query, object obj)
            {
            if (query.Equals($"INSERT INTO [{TABLE_RATINGS}]"))
                AddElementToFile((Rating)obj, m_ratingsFileName);
            else if (query.Equals($"INSERT INTO [{TABLE_STUDENTS}]"))
                AddElementToFile((Student)obj, m_studentsFileName);
            else if (query.Equals($"INSERT INTO [{TABLE_TEACHERS}]"))
                AddElementToFile((Teacher)obj, m_teachersFileName);
            else
                return 0;

            return 1;
            }

        private static void AddElementToFile<T>(T element, string fileName)
            {
            List<T> elements;
            using (var reader = new StreamReader(fileName))
            using (var csvReader = new CsvReader(reader))
                {
                elements = csvReader.GetRecords<T>().ToList();
                }

            using (var writer = new StreamWriter(fileName))
            using (var csvWriter = new CsvWriter(writer))
                {
                elements.Add(element);
                csvWriter.WriteRecords(elements);
                }
            }


        public IDataReader Query(SQLQueryContext sqlQueryContext)
            {
            ArgumentChecker.ThrowIfNull(sqlQueryContext, "sqlQueryContext");
            return ExecuteQuery(sqlQueryContext.GetQuery());
            }

        public IDataReader QueryAll(string table, params string[] selectedProperties)
            {
            var queryContext = new SQLQueryContext(table)
                {
                SelectedProperties = selectedProperties.ToList()
                };
            return Query(queryContext);
            }

        public IDataReader QueryByFilter(string table, string filter, params string[] selectedProperties)
            {
            var queryContext = new SQLQueryContext(table)
                {
                SelectedProperties = selectedProperties,
                Filter = filter
                };
            return Query(queryContext);
            }
        }
    }
