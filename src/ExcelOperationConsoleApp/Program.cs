namespace ExcelOperationConsoleApp
{
    #region using

    using ClosedXML.Excel;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;

    #endregion

    class Program
    {
        static void Main(string[] args)
        {
            var filePath = @".\source.xlsx";// args[0];
            if (File.Exists(filePath))
            {
                new Program().Run(filePath);
            }
        }

        class PopulationByAge
        {
            public string Age { get; set; }
            public int Male { get; set; }
            public int Female { get; set; }
        }

        class HeaderItem
        {
            public IXLAddress Address { get; set; }
            public string TypeOfValue { get; set; }
        }


        void Run(string filePath)
        {
            using (var workbook = new XLWorkbook(filePath))
            {
                IXLWorksheet worksheet = null;
                workbook.Worksheets.TryGetWorksheet("第１表", out worksheet);

                var headers = new Dictionary<string, HeaderItem>()
                {
                    { "Age",new HeaderItem(){TypeOfValue =nameof(String) }},
                    { "Male", new HeaderItem() { TypeOfValue = nameof(Int32) }},
                    { "Female", new HeaderItem() { TypeOfValue = nameof(Int32) }}
                };
                var result = this.GetAllDataFromWorksheet(worksheet, headers, "Age");
                Console.WriteLine("Age,Male,Female");
                foreach (var item in result)
                {
                    Console.WriteLine($"{item.Age},{item.Male},{item.Female}");
                }
            }
        }

        List<PopulationByAge> GetAllDataFromWorksheet(IXLWorksheet worksheet, Dictionary<string, HeaderItem> headers, string primaryKey)
        {
            var result = new List<PopulationByAge>();
            var headerCells = worksheet.Cells();


            foreach (var header in headers)
            {
                header.Value.Address = GetSpecificHeaderColumn(headerCells, header.Key);
            }

            var lastRowNumber = worksheet.LastRowUsed().Cell(headers[primaryKey].Address.ColumnNumber).Address.RowNumber;
            var lastColNumber = worksheet.LastColumnUsed().Cell(headers[primaryKey].Address.RowNumber).Address.ColumnNumber;
            var bodyCells = worksheet.Range($"A1:{ToColumnName(lastColNumber)}{lastRowNumber}").Cells();

            var primaryRows = bodyCells.Where(i => i.Address.ColumnNumber == headers[primaryKey].Address.ColumnNumber).Select(i => i);

            foreach (var primaryRow in primaryRows)
            {
                Regex regex = new Regex("^[0-9]");
                if (regex.IsMatch(primaryRow.Value.ToString().Trim()) == false) continue;

                var excelRow = primaryRow.Address.RowNumber;
                var newItem = new PopulationByAge();
                foreach (var header in headers)
                {
                    var valueObject = worksheet.Cells().Where(i => i.Address.RowNumber == excelRow &&
                                                             i.Address.ColumnNumber == headers[header.Key].Address.ColumnNumber)
                                         .Select(i => i.Value)
                                         .FirstOrDefault();

                    switch (header.Value.TypeOfValue)
                    {
                        case nameof(String):
                            var valueString = valueObject.ToString().Trim().Split(" ").First();
                            typeof(PopulationByAge).GetProperty(header.Key).SetValue(newItem, valueString);
                            break;
                        case nameof(Int32):
                            var valueInt = int.Parse(valueObject.ToString().Trim());
                            typeof(PopulationByAge).GetProperty(header.Key).SetValue(newItem, valueInt);
                            break;
                        default:
                            break;
                    }

                }
                result.Add(newItem);
            }
            return result;
        }

        IXLAddress GetSpecificHeaderColumn(IXLCells cells, string headerString)
        {
            return cells.Where(i => string.Equals(i.Value.ToString().Trim(), headerString, StringComparison.OrdinalIgnoreCase))
                        .Select(i => i.Address).FirstOrDefault();
        }

        //https://qiita.com/omochi_motimoti/items/b6261e3cacda11d460dd#%E6%95%B0%E5%80%A4--%E3%82%AB%E3%83%A9%E3%83%A0%E6%96%87%E5%AD%97 を参照。
        string ToColumnName(int source)
        {
            var numberOfAlphabet = 26;
            if (source < 1) return string.Empty;
            return ToColumnName((source - 1) / numberOfAlphabet) + (char)('A' + ((source - 1) % numberOfAlphabet));
        }
    }
}
