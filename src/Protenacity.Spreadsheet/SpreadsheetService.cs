using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.Excel.EPPlus;
using System.Globalization;

namespace Protenacity.Spreadsheet;

internal class SpreadsheetService : ISpreadsheetService
{
    public IEnumerable<string[]> ReadCsv(string text, char separator = ',')
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return Enumerable.Empty<string[]>();
        }

        using (TextReader textReader = new StringReader(text))
        {
            return ReadCsv(textReader, separator);
        }
    }

    public IEnumerable<string[]> ReadExcel(FileInfo path)
    {
        using (var stream = path.Open(FileMode.Open))
        {
            return ReadExcel(stream);
        }
    }

    public IEnumerable<string[]> ReadExcel(Stream stream)
    {
        var data = new List<string[]>();

        using (var reader = new CsvReader(new ExcelParser(stream, null, new CsvConfiguration(CultureInfo.InvariantCulture))))
        {
            while (reader.Read())
            {
                var columnData = new List<string>();
                for (var column = 0; column != reader.ColumnCount; column++)
                {
                    columnData.Add(reader.GetField<string>(column) ?? string.Empty);
                }
                data.Add(columnData.ToArray());
            }
        }
        return data;
    }


    private bool? IsExcel(Stream stream)
    {
        var buffer = new byte[1024];
        var length = stream.Read(buffer, 0, buffer.Length);
        if (length < 2)
        {
            return null;
        }
        if (length < buffer.Length)
        {
            Array.Resize(ref buffer, length);
        }
        stream.Position = 0;
        var magic = BitConverter.ToUInt32(buffer, 0);
        switch (magic)
        {
            case 0x04034B50: // xlsx
                return true;

            case 0xE011CFD0: // xls
                return null;    // We can't handle this file format

            default:
                if (buffer.All(b => b >= ' ' || b == '\n' || b == '\r' || b == '\t'))
                {
                    return false;   // text file
                }

                return null;        // unknown file type
        }
    }

    private bool? IsExcel(FileInfo path)
    {
        using (var stream = path.Open(FileMode.Open))
        {
            return IsExcel(stream);
        }
    }

    public IEnumerable<string[]> Read(Stream stream, char separator = ',')
    {
        if (!stream.CanRead)
        {
            //  Can't read stream
            return Enumerable.Empty<string[]>();
        }
        
        switch (IsExcel(stream))
        {
            case true:
                return ReadExcel(stream);

            case false:
                return ReadCsv(stream, separator);

        }

        return Enumerable.Empty<string[]>();
    }


    public IEnumerable<string[]> Read(FileInfo path, char separator = ',')
    {
        using (var stream = path.Open(FileMode.Open))
        {
            return Read(stream, separator);
        }
    }

    private IEnumerable<string[]> ReadCsv(TextReader reader, char separator = ',')
    {
        var data = new List<string[]>();
        var csvReader = new CsvParser(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Delimiter = new string(separator, 1)
        }, false);
        while (csvReader.Read())
        {
            var fields = csvReader.Record;
            if (fields != null)
            {
                data.Add(fields);
            }
        }
        return data;
    }

    public IEnumerable<string[]> ReadCsv(Stream stream, char separator = ',')
    {
        using (TextReader reader = new StreamReader(stream))
        {
            return ReadCsv(reader, separator);
        }
    }

    public IEnumerable<string[]> ReadCsv(FileInfo path, char separator = ',')
    {
        using (var reader = path.OpenText())
        {
            return ReadCsv(reader, separator);
        }
    }
}
