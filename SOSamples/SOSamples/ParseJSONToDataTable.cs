using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOSamples
{
    class ParseJSONToDataTable
    {
        public class Parent
        {
            public GetDataJSON getDataJSON { get; set; }

        }

        public class GetDataJSON
        {
            public string message { get; set; }

            public List<Tables> tables { get; set; }
        }

        public class Tables
        {
            public List<Rows> rows { get; set; }
        }

        public class Rows
        {
            public List<Cols> Cols { get; set; }
        }

        public class Cols
        {
            public string colName { get; set; }
            public string colValue { get; set; }
        }

        void Parse()
        {
            string json = @"{
    ""getDataJSON"": {
        ""message"": """",
        ""tables"": [
            {
                ""rows"": [
                    {
                        ""cols"": [
                            {
                                ""colName"": ""columnName1"",
                               
                            },
                            {
                                ""colName"": ""columnName2"",
                                ""colValue"": ""columnValue""
                            }
                        ]
                    },
                    {
                        ""cols"": [
                            {
                                ""colName"": ""columnName1"",
                                ""colValue"": ""columnValue""
                            },
                            {
                                ""colName"": ""columnName2"",
                                ""colValue"": ""columnValue""
                            }
                        ]
                    }
                ]
            }
        ]
    }
}";

            //{ "message":null,"tables":[{"rows":[{"Cols":[{"colName":"1","colValue":"1"},{"colName":"2","colValue":"2"}]}]}]}

            var entireJSON = JsonConvert.DeserializeObject<Parent>(json);

            DataSet dataset = new DataSet();

            Parallel.ForEach(entireJSON.getDataJSON.tables, x => { dataset.Tables.Add(PopulateTable(x)); });
        }

        public static DataTable PopulateTable(Tables table)
        {
            DataTable returnTable = new DataTable();

            // Traverse JSON to get all column names
            var columnNames = table.rows.SelectMany(x => x.Cols.Select(y => y.colName)).Distinct();

            // Populate table with these columns
            foreach (var columnName in columnNames)
            {
                returnTable.Columns.Add(columnName);
            }

            // Travsers JSON again for values
            table.rows.ForEach(x =>
            {
                var newRow = returnTable.NewRow();

                x.Cols.ForEach(y =>
                {
                    newRow[y.colName] = y.colValue;
                });

                returnTable.Rows.Add(newRow);
            });

            return returnTable;
        }
    }
}
