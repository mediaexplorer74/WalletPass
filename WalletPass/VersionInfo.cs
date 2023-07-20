// WalletPass.VersionInfo

using System;
using System.Runtime.Serialization;
using Windows.ApplicationModel;

namespace WalletPass
{
  [DataContract]
  public class VersionInfo
  {
    [DataMember]
    public string version { get; set; }

    public VersionInfo() => this.version = "0.0.0.0";

    public VersionInfo(bool showTutorial)
    {
      PackageVersion version = Package.Current.Id.Version;
      this.version = string.Format("{0}.{1}.{2}.{3}", 
          (object) version.Major, (object) version.Minor,
          (object) version.Build, (object) version.Revision);
    }

        public int mayorVersion()
        {
            return (int)Convert.ToInt16(this.version.Substring(0, 1));
        }

        public int minorVersion()
        {
            return (int)Convert.ToInt16(this.version.Substring(2, 1));
        }
    }
}
