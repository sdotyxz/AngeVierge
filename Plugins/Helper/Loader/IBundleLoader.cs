using UnityEngine;

namespace Helper.Loader
{
    public interface IBundleLoader
    {
        AssetBundle bundle { get; }
        bool isLoadComplete { get; }

        void Unload(bool unloadAllLoadedObjects);
        void Load(System.Action loadComplete, System.Action loadError);
        float GetProgress();
        int GetSize();
        int GetBytesDownloaded();
        byte[] GetBytes();
        string GetText();
        UnityEngine.Object GetAsset(string name, System.Type type);
        GameObject GetPrefab(string name);
        GameObject GetGameObject(string name);
        void GetPrefabAsync(string name, System.Action<GameObject> onComplete);
        byte[] GetBytes(string name);
        string GetText(string name);
        System.Type GetClass(string name, string assetName);
    }
}