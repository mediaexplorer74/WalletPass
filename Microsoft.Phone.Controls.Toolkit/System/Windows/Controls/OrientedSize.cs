// Decompiled with JetBrains decompiler
// Type: System.Windows.Controls.OrientedSize
// Assembly: Microsoft.Phone.Controls.Toolkit, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b772ad94eb9ca604
// MVID: BD71C4CB-0489-4E2A-8436-9568C3D40C61
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.dll
// XML documentation location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.xml

namespace System.Windows.Controls
{
  /// <summary>
  /// The OrientedSize structure is used to abstract the growth direction from
  /// the layout algorithms of WrapPanel.  When the growth direction is
  /// oriented horizontally (ex: the next element is arranged on the side of
  /// the previous element), then the Width grows directly with the placement
  /// of elements and Height grows indirectly with the size of the largest
  /// element in the row.  When the orientation is reversed, so is the
  /// directional growth with respect to Width and Height.
  /// </summary>
  /// <QualityBand>Mature</QualityBand>
  internal struct OrientedSize
  {
    /// <summary>The orientation of the structure.</summary>
    private Orientation _orientation;
    /// <summary>
    /// The size dimension that grows directly with layout placement.
    /// </summary>
    private double _direct;
    /// <summary>
    /// The size dimension that grows indirectly with the maximum value of
    /// the layout row or column.
    /// </summary>
    private double _indirect;

    /// <summary>Gets the orientation of the structure.</summary>
    public Orientation Orientation => this._orientation;

    /// <summary>
    /// Gets or sets the size dimension that grows directly with layout
    /// placement.
    /// </summary>
    public double Direct
    {
      get => this._direct;
      set => this._direct = value;
    }

    /// <summary>
    /// Gets or sets the size dimension that grows indirectly with the
    /// maximum value of the layout row or column.
    /// </summary>
    public double Indirect
    {
      get => this._indirect;
      set => this._indirect = value;
    }

    /// <summary>Gets or sets the width of the size.</summary>
    public double Width
    {
      get => this.Orientation == 1 ? this.Direct : this.Indirect;
      set
      {
        if (this.Orientation == 1)
          this.Direct = value;
        else
          this.Indirect = value;
      }
    }

    /// <summary>Gets or sets the height of the size.</summary>
    public double Height
    {
      get => this.Orientation != 1 ? this.Direct : this.Indirect;
      set
      {
        if (this.Orientation != 1)
          this.Direct = value;
        else
          this.Indirect = value;
      }
    }

    /// <summary>Initializes a new OrientedSize structure.</summary>
    /// <param name="orientation">Orientation of the structure.</param>
    public OrientedSize(Orientation orientation)
      : this(orientation, 0.0, 0.0)
    {
    }

    /// <summary>Initializes a new OrientedSize structure.</summary>
    /// <param name="orientation">Orientation of the structure.</param>
    /// <param name="width">Un-oriented width of the structure.</param>
    /// <param name="height">Un-oriented height of the structure.</param>
    public OrientedSize(Orientation orientation, double width, double height)
    {
      this._orientation = orientation;
      this._direct = 0.0;
      this._indirect = 0.0;
      this.Width = width;
      this.Height = height;
    }
  }
}
