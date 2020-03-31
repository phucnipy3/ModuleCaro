using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
namespace ClassLibraryServer
{
    public class ExcelFile
    {
        string path;
        _Application excel = new Application();
        Workbook wb;
        Worksheet ws;
        public ExcelFile(string path, int sheetNumber)
        {
            this.path = path;
            wb = excel.Workbooks.Open(path);
            ws = wb.Worksheets[sheetNumber];
        }

        public string ReadCell(int i, int j)
        {
            string cellValue = "";
            if (ws.Cells[i + 1, j + 1].Value2 != null)
                cellValue = ws.Cells[i + 1, j + 1].Value2.ToString();
            return cellValue;
        }

        public void WriteToCell(int i, int j, string value)
        {
            ws.Cells[i + 1, j + 1].Value2 = value;
        }

        public void Save()
        {
            wb.Save();
        }

        public void SaveAs(string path)
        {
            wb.SaveAs(path);
        }

        public static ExcelFile CreateNewFile(string pathName)
        {
            _Application excel = new Application();
            Workbook wb = excel.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);
            wb.SaveAs(pathName);
            wb.Close();
            return new ExcelFile(pathName, 1);
        }

        public void Close()
        {
            wb.Close();
        }

        public void AddNewSheet()
        {
            wb.Worksheets.Add(After: wb.Worksheets[wb.Worksheets.Count]);
        }

        public void SelectSheet(int sheetNumber)
        {
            ws = wb.Worksheets[sheetNumber];
        }

        public void DeleteSheet(int sheetNumber)
        {
            wb.Worksheets[sheetNumber].Delete();
        }

        public string[,] ReadRange(int startX, int endX, int startY, int endY)
        {
            Range range = (Range)ws.Range[ws.Cells[startX+1, endX+1], ws.Cells[startY+1, endY+1]];
            object[,] holder = range.Value2;
            int rowMax = startY - startX + 1;
            int colMax = endY - endX + 1;
            string[,] returnString = new string[rowMax, colMax];
            for(int i = 0;i<rowMax; i++)
            {
                for(int j = 0;j<colMax; j++)
                {
                    returnString[i, j] = holder[i + 1, j + 1].ToString();
                }
            }
            return returnString;
        }

        public void WriteRange(int startX, int endX, int startY, int endY, string[,] values)
        {
            Range range = (Range)ws.Range[ws.Cells[startX+1, endX+1], ws.Cells[startY+1, endY+1]];
            range.Value2 = values;
        }
    }
}
