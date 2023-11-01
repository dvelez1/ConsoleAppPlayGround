// See https://aka.ms/new-console-template for more information
using ConsoleAppPlayGround.Models;
using ConsoleAppPlayGround.service;

System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

var pathToExcel = @"C:\Users\Dennis\Documents\temp_location\customers.xlsx";
var sheetName = "customer";
//var jsonCustomer =  await ReadExcelFile.ReadExcelFileAndConvertToJson(pathToExcel, sheetName);
//var json = await ReadExcelFile.ConvertExcelToJson();

//if (ReadExcelFile.ValidateJsonCustmerSchema(jsonCustomer, typeof(List<customer>)).GetAwaiter().GetResult())
//    Console.WriteLine($"Valid Schema Validation. Please, see excel conversion to JSON. {jsonCustomer}");
//else
//    Console.WriteLine("Invalid Json Schema");



//ParallelExampleNotAsync.RunParallelExample(600, 800);

DatatableExample.ProofOfConcept();

Console.ReadLine();