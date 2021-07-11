namespace ExcelOperationConsoleApp
{
    #region using

    using ClosedXML.Excel;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

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
            public int Age { get; set; }
            public int NumberOfMale { get; set; }
            public int NumberOfFemale { get; set; }
        }

        void Run(string filePath)
        {
            using (var workbook = new XLWorkbook(filePath))
            {
                IXLWorksheet worksheet = null;
                workbook.Worksheets.TryGetWorksheet("第１表", out worksheet);
                var result = this.GetAllDataFromWorksheet(worksheet);
                Console.WriteLine("Age,Male,Female");
                foreach (var item in result)
                {
                    Console.WriteLine($"{item.Age},{item.NumberOfMale},{item.NumberOfFemale}");
                }
            }
        }

        List<PopulationByAge> GetAllDataFromWorksheet(IXLWorksheet worksheet)
        {
            var result = new List<PopulationByAge>();
            //Headerの位置は定義する必要がある・・・
            var headerCells = worksheet.Range("A1:T19").Cells();

            //Headerの列を特定する。
            var ageColumn = GetSpecificHeaderColumn(headerCells, "Age");
            var maleColumn = GetSpecificHeaderColumn(headerCells, "Male");
            var femaleColumn = GetSpecificHeaderColumn(headerCells, "Female");

            //表のBody部の設定をする。
            var lastRowNumber = worksheet.LastRowUsed().Cell(ageColumn.ColumnNumber).Address.RowNumber;
            var lastColNumber = worksheet.LastColumnUsed().Cell(ageColumn.RowNumber).Address.ColumnNumber;
            var bodyCells = worksheet.Range($"A1:{ToColumnName(lastColNumber)}{lastRowNumber}").Cells();

            //Headerの列毎に、情報を取得する。
            var ageColumns = bodyCells.Where(i => i.Address.ColumnNumber == ageColumn.ColumnNumber).Select(i => i);
            var maleColumns = bodyCells.Where(i => i.Address.ColumnNumber == maleColumn.ColumnNumber).Select(i => i);
            var femaleColumns = bodyCells.Where(i => i.Address.ColumnNumber == femaleColumn.ColumnNumber).Select(i => i);

            //必要な情報だけをPOCOに設定していく。
            //主キーは、年齢列とする。
            foreach (var age in ageColumns)
            {
                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex("^[0-9]");
                if (regex.IsMatch(age.Value.ToString().Trim()) == false) continue;

                var excelRow = age.Address.RowNumber;
                result.Add(new PopulationByAge()
                {
                    Age = int.Parse(age.Value.ToString().Trim().Split(" ").First()),
                    NumberOfFemale = maleColumns.Where(i => i.Address.RowNumber == excelRow)
                                         .Select(i => int.Parse(i.Value.ToString().Trim().Replace(",", string.Empty)))
                                         .FirstOrDefault(),
                    NumberOfMale = femaleColumns.Where(i => i.Address.RowNumber == excelRow)
                                              .Select(i => int.Parse(i.Value.ToString().Trim().Replace(",", string.Empty)))
                                              .FirstOrDefault()
                });
            }
            return result;
        }

        IXLAddress GetSpecificHeaderColumn(IXLCells cells, string headerString)
        {
            return cells.Where(i => string.Equals(i.Value.ToString().Trim(), headerString, StringComparison.OrdinalIgnoreCase))
                        .Select(i => i.Address).FirstOrDefault();
        }

        string ToColumnName(int source)
        {
            if (source < 1) return string.Empty;
            return ToColumnName((source - 1) / 26) + (char)('A' + ((source - 1) % 26));
        }
    }
}
