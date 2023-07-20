// Decompiled with JetBrains decompiler
// Type: Coding4Fun.Toolkit.Controls.Common.PhoneHelper
// Assembly: Coding4Fun.Toolkit.Controls, Version=2.0.9.0, Culture=neutral, PublicKeyToken=c5fd7b72b1a17ce4
// MVID: FE9E68FC-2303-41B7-81A5-6B9678322A1F
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Coding4Fun.Toolkit.Controls.dll

using System;
using System.Xml;

namespace Coding4Fun.Toolkit.Controls.Common
{
  public class PhoneHelper
  {
    private const string AppManifestName = "WMAppManifest.xml";
    private const string AppNodeName = "App";

    public static string GetAppAttribute(string attributeName)
    {
      if (ApplicationSpace.IsDesignMode)
        return "";
      try
      {
        using (XmlReader xmlReader = XmlReader.Create("WMAppManifest.xml", new XmlReaderSettings()
        {
          XmlResolver = (XmlResolver) new XmlXapResolver()
        }))
        {
          xmlReader.ReadToDescendant("App");
          return xmlReader.IsStartElement() ? xmlReader.GetAttribute(attributeName) : throw new FormatException("WMAppManifest.xml is missing App");
        }
      }
      catch (Exception ex)
      {
        return "";
      }
    }
  }
}
