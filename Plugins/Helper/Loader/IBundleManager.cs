using System;
using System.Collections.Generic;
using Helper.Config;

namespace Helper.Loader
{
    public interface IBundleManager
    {
        IBundleLoader Get(string id);
        IBundleLoader GetByContentName(string assetsBundleContentName);
        IDictionary<string, IBundleLoader> GetAllBundleLoader();
        List<IBundleLoader> Loads(List<VersionBundle> items, Action loadComplete, Action loadError);
        IBundleLoader Load(VersionBundle item, Action loadComplete, Action loadError);
        void UnloadUnusedAssets();
        void Unloads(List<string> ids, bool unloadAllLoadedObjects);
        void Unload(string id, bool unloadAllLoadedObjects);
    }
}
