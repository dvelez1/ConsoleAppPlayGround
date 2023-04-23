using ConsoleAppPlayGround.Models;
using ExcelDataReader;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Schema.Generation;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.OleDb;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppPlayGround.service
{
    public class ReadExcelFile
    {
        public static async Task<string> ReadExcelFileAndConvertToJson(string pathToExcel, string sheetName)
        {
            //var pathToExcel = @"C:\Users\Dennis\Documents\temp_location\customers.xlsx";
            //var sheetName = "customer";

            //This connection string works if you have Office 2007+ installed and your 
            //data is saved in a .xlsx file
            var connectionString = string.Format(@"
            Provider=Microsoft.ACE.OLEDB.12.0;
            Data Source={0};
            Extended Properties=""Excel 12.0 Xml;HDR=YES""
        ", pathToExcel);

            //Creating and opening a data connection to the Excel sheet 
            using (var conn = new OleDbConnection(connectionString))
            {
                conn.Open();

                var cmd = conn.CreateCommand();
                cmd.CommandText = string.Format(
                    @"SELECT * FROM [{0}$]",
                    sheetName
                    );

                using (var rdr = cmd.ExecuteReader())
                {
                    //LINQ query - when executed will create anonymous objects for each row
                    var query =
                                          (from DbDataRecord row in rdr
                                           select row).Select(x =>
                                           {
                                               //dynamic item = new ExpandoObject();
                                               Dictionary<string, object> item = new Dictionary<string, object>();
                                               for (int i = 0; i < x.FieldCount; i++)
                                                   item.Add(rdr.GetName(i), x[i]);
                                               return item;
                                           });

                    //Generates JSON from the LINQ query
                    var json = JsonConvert.SerializeObject(query); //, Formatting.Indented

                    return json;
                }
            }


        }

        //https://stackoverflow.com/questions/57378535/read-exel-files-dynamically-not-depending-on-rows-and-write-json-c-sharp
        public static async Task<string> ConvertExcelToJson()
        {
            var inFilePath = @"C:\Users\Dennis\Documents\temp_location\customers.xlsx";
            var json = string.Empty;

            //var pathToExcel = @"C:\Users\Dennis\Documents\temp_location\customers.xlsx";
            //var sheetName = "customer";


            using (var inFile = System.IO.File.Open(inFilePath, FileMode.Open, FileAccess.Read))

            using (var reader = ExcelReaderFactory.CreateReader(inFile, new ExcelReaderConfiguration()
            { FallbackEncoding = Encoding.GetEncoding(1252) }))
            {
                var ds = reader.AsDataSet(new ExcelDataSetConfiguration()
                {
                    ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                    {
                        UseHeaderRow = true
                    }
                });
                var table = ds.Tables[0];
                json = JsonConvert.SerializeObject(table, Formatting.Indented);
            }

            return json;
        }

        //https://www.newtonsoft.com/jsonschema
        //https://www.newtonsoft.com/jsonschema/help/html/CustomJsonValidator.htm
        public static async Task<bool> ValidateJsonCustmerSchema(string json , Type objectType)
        {
            JSchemaGenerator generator = new JSchemaGenerator();
            //JSchema schema = generator.Generate(typeof(List<customer>));
            JSchema schema = generator.Generate(objectType);
            JArray jsonArray = JArray.Parse(json);
            //JObject person = JObject.Parse(jsonArray[0].ToString());
            IList<string> messages;
            return jsonArray.IsValid(schema, out messages);
        }

    }
}


