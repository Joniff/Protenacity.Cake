using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Persistence.Repositories;
using Umbraco.Cms.Core.Scoping;

namespace Protenacity.Cake.Web.Core.Extensions;

public static class IKeyValueRepositoryExtensions
{
    public static bool TrySetKeyValue(this IKeyValueRepository keyValueRepository, ICoreScopeProvider scopeProvider, string key, string? originalValue, string newValue)
    {
        using (var scope = scopeProvider.CreateCoreScope(System.Data.IsolationLevel.ReadCommitted))
        {
            try
            {
                scope.WriteLock(Constants.Locks.KeyValues);

                IKeyValue? keyValue = keyValueRepository.Get(key);

                if (keyValue == null)
                {
                    if (originalValue != null)
                    {
                        return false;
                    }
                    keyValue = new KeyValue
                    {
                        Identifier = key,
                        CreateDate = DateTime.UtcNow,
                    };
                }
                else if (keyValue.Value != originalValue)
                {
                    return false;
                }

                keyValue.Value = newValue;
                keyValue.UpdateDate = DateTime.UtcNow;
                keyValueRepository.Save(keyValue);
            }
            finally
            {
                scope.Complete();
            }
        }
        return true;
    }
}
