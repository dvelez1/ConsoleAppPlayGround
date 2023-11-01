using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppPlayGround.service
{
    public class DatatableExample
    {

        // GERALD, Para que lo utilices de ejemplo para lo que estás haciendo. DVG 11-01-2023

        public static void ProofOfConcept()
        {
            DataTable myTable = new DataTable("Example");

            myTable.Columns.Add("Product_id", typeof(int));
            myTable.Columns.Add("Name", typeof(string));
            myTable.Columns.Add("Qualifier_1", typeof(string));
            myTable.Columns.Add("Value_1", typeof(string));
            myTable.Columns.Add("Qualifier_2", typeof(string));
            myTable.Columns.Add("Value_2", typeof(string));
            myTable.Columns.Add("Qualifier_3", typeof(string));
            myTable.Columns.Add("Value_3", typeof(string));
            myTable.Columns.Add("MergerOfQualifiers", typeof(string));

            myTable.Rows.Add(1, "Pedro", "Q101", "V101", "trigger_value", "0", "V301", "Q301", "");
            myTable.Rows.Add(2, "Carlos", "Q102", "V102", "S1", "0", "V302", "Q302", "");
            myTable.Rows.Add(3, "Ramon", "Q103", "V103", "trigger_value", "1", "V303", "Q303", "");

            PrintTable(myTable);

            Console.WriteLine(" ");
            Console.WriteLine("*****************");
            Console.WriteLine(" ");

            foreach (DataRow dr in myTable.Rows)
            {
                dr["MergerOfQualifiers"] = MergeOfQualifiers(dr);
            }

            RemoveTableColumns(ref myTable);

            PrintTable(myTable);
        }

        private static string MergeOfQualifiers(DataRow dr)
        {
            string result = string.Empty;

            for (int i = 1; i <= 3; i++)
            {

                if (string.IsNullOrWhiteSpace(dr["Qualifier_" + i].ToString()))
                    return string.IsNullOrWhiteSpace(result) ? string.Empty : result.TrimEnd(',');


                if (dr["Qualifier_" + i].ToString() == "trigger_value")
                    result = result + (dr["Qualifier_" + i].ToString() + " <--> " + (dr["Value_" + i].ToString() == "1" ? "Yes" + "," : "No" + ","));
                else
                    result = result + (dr["Qualifier_" + i].ToString() + " <--> " + dr["Value_" + i].ToString() + ",");

            }


            return string.IsNullOrWhiteSpace(result) ? string.Empty : result.TrimEnd(',');

        }

        private static void PrintTable(DataTable dt)
        {
            foreach (DataRow dataRow in dt.Rows)
            {
                Console.WriteLine(" ");
                foreach (var item in dataRow.ItemArray)
                {
                    Console.WriteLine(item);
                }
            }
        }

        private static void RemoveTableColumns(ref DataTable myTable)
        {
            myTable.Columns.Remove("Qualifier_1");
            myTable.Columns.Remove("Value_1");
            myTable.Columns.Remove("Qualifier_2");
            myTable.Columns.Remove("Value_2");
            myTable.Columns.Remove("Qualifier_3");
            myTable.Columns.Remove("Value_3");
        }

    }
}
