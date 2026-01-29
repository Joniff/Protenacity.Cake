using Protenacity.Cake.Web.Core.Property;
using Umbraco.Cms.Core.Models;

namespace Protenacity.Cake.Web.Presentation.Editor.ResponsiveImage;

public class ResponsiveImageViewModel : ResponsiveImageBase, IResponsiveImageViewModel
{
    public required IEnumerable<Tuple<int?, string>> Urls { get; init; }
}
