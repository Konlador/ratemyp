using System.Collections.Generic;

namespace RateMyP
    {
    public class SQLQueryContext
        {
        private readonly string m_table;

        public SQLQueryContext(string table)
            {
            m_table = table;
            }

        public IList<string> SelectedProperties { get; set; }

        public string Filter { get; set; }

        public string GetQuery()
            {
            var query = $"SELECT {FormatSelectString(SelectedProperties)} FROM [{m_table}]";

            if (!string.IsNullOrEmpty(Filter))
                query += $" WHERE {Filter}";

            return query;
            }

        private static string FormatSelectString(IList<string> selectedProperties)
            {
            if (null == selectedProperties || selectedProperties.Count <= 0)
                return "*";

            return "[" + string.Join("],[", selectedProperties) + "]";
            }
        }
    }
