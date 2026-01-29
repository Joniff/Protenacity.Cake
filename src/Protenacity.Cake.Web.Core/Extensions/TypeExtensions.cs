using System.ComponentModel;
using Umbraco.Cms.Infrastructure.ModelsBuilder;

namespace Protenacity.Cake.Web.Core.Extensions;

public static class TypeExtensions
{
    public static string ModelsBuilderAlias(this Type publishedModel, string propertyName) =>
        (publishedModel.GetProperty(propertyName)?.GetCustomAttributes(typeof(ImplementPropertyTypeAttribute), true).FirstOrDefault() as ImplementPropertyTypeAttribute)?.Alias
        ?? throw new ApplicationException("Can\'t get alias for " + publishedModel.Name + "." + propertyName);

    public static string Description(this Type obj, string property) =>
       (obj.GetProperty(property)?.GetCustomAttributes(typeof(DescriptionAttribute), true).FirstOrDefault() as DescriptionAttribute)?.Description ?? property;
}
