// Decompiled with JetBrains decompiler
// Type: WalletPass.IO_Ant
// Assembly: Wallet Pass, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1E8AC314-47AB-4931-BC36-563B31C55EF5
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Wallet Pass.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Runtime.Serialization;

namespace WalletPass
{
  internal class IO_Ant
  {
    public List<ClasePass> LoadDataPasses() => (List<ClasePass>) this.LoadValue("passes.xml", typeof (List<ClasePass>)) ?? new List<ClasePass>();

    private object LoadValue(string path, Type objectType)
    {
      IsolatedStorageFile storeForApplication = IsolatedStorageFile.GetUserStoreForApplication();
      if (!storeForApplication.FileExists(path))
        return (object) null;
      IsolatedStorageFileStream storageFileStream = (IsolatedStorageFileStream) null;
      try
      {
        storageFileStream = storeForApplication.OpenFile(path, FileMode.Open);
        return new DataContractSerializer(objectType).ReadObject((Stream) storageFileStream);
      }
      catch
      {
        return (object) null;
      }
      finally
      {
        if (storageFileStream != null)
        {
          storageFileStream.Close();
          storageFileStream.Dispose();
        }
      }
    }
  }
}
