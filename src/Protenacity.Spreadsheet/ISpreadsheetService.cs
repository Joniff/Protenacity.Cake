namespace Protenacity.Spreadsheet;

public interface ISpreadsheetService
{
    IEnumerable<string[]> Read(Stream stream, char separator = ',');
    IEnumerable<string[]> Read(FileInfo path, char separator = ',');

    IEnumerable<string[]> ReadCsv(string text, char separator = ',');
    IEnumerable<string[]> ReadCsv(Stream stream, char separator = ',');
    IEnumerable<string[]> ReadCsv(FileInfo path, char separator = ',');

    IEnumerable<string[]> ReadExcel(Stream stream);
    IEnumerable<string[]> ReadExcel(FileInfo path);
}
