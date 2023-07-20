// Decompiled with JetBrains decompiler
// Type: Coding4Fun.Toolkit.Controls.MetroFlowData
// Assembly: Coding4Fun.Toolkit.Controls, Version=2.0.9.0, Culture=neutral, PublicKeyToken=c5fd7b72b1a17ce4
// MVID: FE9E68FC-2303-41B7-81A5-6B9678322A1F
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Coding4Fun.Toolkit.Controls.dll

using System;
using System.ComponentModel;

namespace Coding4Fun.Toolkit.Controls
{
  public class MetroFlowData : INotifyPropertyChanged
  {
    private Uri _imageUri;
    private string _title;

    public Uri ImageUri
    {
      get => this._imageUri;
      set
      {
        this._imageUri = value;
        this.RaisePropertyChanged(nameof (ImageUri));
      }
    }

    public string Title
    {
      get => this._title;
      set
      {
        this._title = value;
        this.RaisePropertyChanged(nameof (Title));
      }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    private void RaisePropertyChanged(string propertyName)
    {
      if (this.PropertyChanged == null)
        return;
      this.PropertyChanged((object) this, new PropertyChangedEventArgs(propertyName));
    }
  }
}
