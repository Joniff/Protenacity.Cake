using Protenacity.Cake.Web.Core.Property;
using Umbraco.Cms.Core.Models;

namespace Protenacity.Cake.Web.Presentation.Editor.ResponsiveImage;

public interface IResponsiveImageViewModel : IResponsiveImageBase
{
    IEnumerable<Tuple<int?, string>> Urls { get; }
}
