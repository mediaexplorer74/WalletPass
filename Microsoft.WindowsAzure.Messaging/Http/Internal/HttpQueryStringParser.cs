// Decompiled with JetBrains decompiler
// Type: Microsoft.WindowsAzure.Messaging.Http.Internal.HttpQueryStringParser
// Assembly: Microsoft.WindowsAzure.Messaging, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B9918A97-7C70-4F7D-AF4D-7D1C4EF615F3
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.WindowsAzure.Messaging.dll

using System;
using System.Collections.Generic;

namespace Microsoft.WindowsAzure.Messaging.Http.Internal
{
  internal class HttpQueryStringParser
  {
    internal static Dictionary<string, string> Parse(string queryString)
    {
      Dictionary<string, string> dictionary = new Dictionary<string, string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
      string str1 = queryString;
      char[] chArray = new char[1]{ '&' };
      foreach (string str2 in str1.Split(chArray))
      {
        int length = str2.IndexOf('=');
        if (length >= 0)
        {
          string key = str2.Substring(0, length);
          string str3 = str2.Substring(length + 1);
          dictionary.Add(key, str3);
        }
        else
          break;
      }
      return dictionary;
    }
  }
}
