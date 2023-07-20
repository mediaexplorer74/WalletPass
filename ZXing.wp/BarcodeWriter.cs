// ZXing.BarcodeWriter

//using System.Windows.Media.Imaging;
using Windows.UI.Xaml.Media.Imaging;
using ZXing.Rendering;

namespace ZXing
{
  /// <summary>
  /// A smart class to encode some content to a barcode image
  /// </summary>
  public class BarcodeWriter : BarcodeWriterGeneric<WriteableBitmap>, IBarcodeWriter
  {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:ZXing.BarcodeWriter" /> class.
        /// </summary>
        public BarcodeWriter()
        {
            this.Renderer = (IBarcodeRenderer<WriteableBitmap>)
                new WriteableBitmapRenderer();
        }
    }
}
