using Protenacity.Cake.Web.Core.Extensions;
using Protenacity.Cake.Web.Core.Property;

namespace Protenacity.Cake.Web.Core.Constitution;

public partial class EditorTimeslot
{
    public EditorDayOfWeek DayTyped => EditorDayOfWeek.ParseByDescription(this.Day);

}
