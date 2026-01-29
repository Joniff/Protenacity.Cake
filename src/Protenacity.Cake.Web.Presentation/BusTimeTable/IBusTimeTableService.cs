namespace Protenacity.Cake.Web.Presentation.BusTimeTable;

public interface IBusTimeTableService
{
    IDictionary<string, string> ListServicesByStop(Uri url, string stop);
    IEnumerable<DayOfWeek> ListDaysByServiceStop(Uri url, string service, string stop);
    IDictionary<DateTime, string> ListTimeTableByServiceStopDay(Uri url, string service, string stop, DayOfWeek day);
}
