using Microsoft.Extensions.DependencyInjection;
using Protenacity.Cake.Web.Core.Cryptography;
using Protenacity.Cake.Web.Core.Property;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;

namespace Protenacity.Cake.Web.Core.Boot;

public class RegisterComposer : IComposer
{
    public void Compose(IUmbracoBuilder builder)
    {
        builder.Services.AddTransient<ICryptographyService, CryptographyService>();
        //builder.PropertyValueConverters().Append<ActionStyleAlignmentsValueConverter>();
    }
}
