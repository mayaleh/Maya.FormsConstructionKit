using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using Maya.Ext.Rop;

namespace Maya.FormsConstructionKit.Api.Library.Exports.EntityValue
{
    public static class CsvExport
    {
        public static Result<string, Exception> Generate(List<Dictionary<string, object>> rows, bool sanitizeForInjection = true, string delimiter = ";")
        {
            try
            {
                var csvContent = "";
                var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    SanitizeForInjection = sanitizeForInjection,
                    Delimiter = delimiter,
                };
                using (var writer = new StringWriter())
                using (var csv = new CsvWriter(writer, config))
                {
                    var head = rows.First();
                    foreach (var (name, value) in head)
                    {
                        csv.WriteField(name);
                    }
                    csv.NextRecord();

                    rows.ForEach(row => {
                        foreach (var (name, value) in row)
                        {
                            csv.WriteField(value);
                        }
                        csv.NextRecord();
                    });

                    csvContent = writer.ToString();
                }

                return Result<string, Exception>.Succeeded(csvContent);
            }
            catch (Exception e)
            {
                return Result<string, Exception>.Failed(e);
            }
        }
    }
}
