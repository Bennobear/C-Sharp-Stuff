//Read from Excel to Range, excel C# to range C# to array, cell to array

using System;
using Excel = Microsoft.Office.Interop.Excel;

class StartUp
{
    static void Main()
    {
        string filePath = @"C:\Sample.xlsx";

        int rowsCount = 5;
        int colsCount = 6;

        Excel.Application excel = new Excel.Application();
        excel.Visible = false; 
        excel.EnableAnimations = false;

        Excel.Workbook wkb = Open(excel, filePath);
        Excel.Worksheet wk = (Excel.Worksheet)excel.Worksheets.get_Item(1);

        Excel.Range startCell = wk.Cells[1, 1];
        Excel.Range endCell = wk.Cells[rowsCount, colsCount];
        Excel.Range currentRange = wk.get_Range(startCell, endCell).Cells;
        currentRange.Interior.Color = Excel.XlRgbColor.rgbWhite;

        object[,]  matrixRead = (object[,])currentRange.Value;
        bool[,] matrixResult = new bool[rowsCount+1,colsCount+1];

        for (int rows = 1; rows <= rowsCount; rows++)
        {
            for (int cols = 1; cols < colsCount; cols++)
            {
                if (matrixRead[rows,cols].ToString()==matrixRead[rows,cols+1].ToString())
                {
                    matrixResult[rows, cols] = true;
                    matrixResult[rows, cols + 1] = true;
                }
            }
        }

        for (int rows = 1; rows <= rowsCount; rows++)
        {
            for (int cols = 1; cols <= colsCount; cols++)
            {
                if (matrixResult[rows, cols])
                {
                    currentRange.Cells[rows, cols].interior.color = 
                                                Excel.XlRgbColor.rgbRed;
                }                
            }
        }

        excel.EnableAnimations = true;
        wkb.Close(true);
        excel.Quit();
        Console.WriteLine("Finished!");
    }

    private static Excel.Workbook Open(Excel.Application excelInstance, 
            string fileName, bool readOnly = false, 
            bool editable = true, bool updateLinks = true)
    {
        Excel.Workbook book = excelInstance.Workbooks.Open(
            fileName, updateLinks, readOnly,
            Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
            Type.Missing, editable, Type.Missing, Type.Missing, Type.Missing,
            Type.Missing, Type.Missing);
        return book;
    }
}
