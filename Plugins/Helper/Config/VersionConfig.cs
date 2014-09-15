namespace Helper.Config
{
    public class VersionConfig
    {
        public string version;
        public string language;
        public string os;
        public VersionNotice notice;
        public VersionBundle[] bundles;

        public int versionValue { get { return int.Parse(version.Replace(".", "")); } }
    }

    public class VersionNotice
    {
        public string title;
        public string content;
        public string url;
    }

    public class VersionBundle
    {
        public string id;
        public string url;
        public string version;
        public bool isEncrypted = false;

        public int versionValue { get { return int.Parse(version.Replace(".", "")); } }
    }
}