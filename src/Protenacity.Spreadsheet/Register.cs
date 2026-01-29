using Microsoft.Extensions.DependencyInjection;

namespace Protenacity.Spreadsheet;

public static class Register
{
    public static IServiceCollection AddSpreadsheet(this IServiceCollection services)
    {
        services.AddTransient<ISpreadsheetService, SpreadsheetService>();
        return services;
    }
}
