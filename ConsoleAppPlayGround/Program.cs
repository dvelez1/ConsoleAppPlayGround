// See https://aka.ms/new-console-template for more information
using ConsoleAppPlayGround.Models;
using ConsoleAppPlayGround.service;

var pathToExcel = @"C:\Users\Dennis\Documents\temp_location\customers.xlsx";
var sheetName = "customer";
var jsonCustomer =  ReadExcelFile.ReadExcelFileAndConvertToJson(pathToExcel, sheetName);

if (ReadExcelFile.ValidateJsonCustmerSchema(jsonCustomer))
    Console.WriteLine($"Valid Schema Validation. Please, see excel conversion to JSON. {jsonCustomer}");
else
    Console.WriteLine("Invalid Json Schema");


Console.ReadLine();