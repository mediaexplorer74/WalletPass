// System.Windows.Media.Imaging.WriteableBitmapExtensionsXna

using Microsoft.Xna.Framework.Media;
using System.IO;
using System.Xml.Linq;
using Windows.UI.Xaml.Media.Imaging;

namespace System.Windows.Media.Imaging
{
  /// <summary>
  /// Collection of draw spline extension methods for the Silverlight WriteableBitmap class.
  /// </summary>
  public static class WriteableBitmapExtensionsXna
  {
    /// <summary>
    /// Saves the WriteableBitmap encoded as JPEG to the Media library using the best quality of 100.
    /// </summary>
    /// <param name="bitmap">The WriteableBitmap to save.</param>
    /// <param name="name">The name of the destination file.</param>
    /// <param name="saveToCameraRoll">If true the bitmap will be saved to the camera roll, otherwise it will be written to the default saved album.</param>
    public static Picture SaveToMediaLibrary(
      this WriteableBitmap bitmap,
      string name,
      bool saveToCameraRoll = false)
    {
      return bitmap.SaveToMediaLibrary(name, 100, saveToCameraRoll);
    }

    /// <summary>
    /// Saves the WriteableBitmap encoded as JPEG to the Media library.
    /// </summary>
    /// <param name="bitmap">The WriteableBitmap to save.</param>
    /// <param name="name">The name of the destination file.</param>
    /// <param name="quality">The quality for JPEG encoding has to be in the range 0-100,
    /// where 100 is the best quality with the largest size.</param>
    /// <param name="saveToCameraRoll">If true the bitmap will be saved to the camera roll, otherwise it will be written to the default saved album.</param>
    public static Picture SaveToMediaLibrary(
      this WriteableBitmap bitmap,
      string name,
      int quality,
      bool saveToCameraRoll = false)
    {
      using (MemoryStream memoryStream = new MemoryStream())
      {
        Extensions.SaveJpeg(bitmap, (Stream) memoryStream,
            ((BitmapSource) bitmap).PixelWidth, ((BitmapSource) bitmap).PixelHeight, 
            0, quality);
        memoryStream.Seek(0L, SeekOrigin.Begin);
        MediaLibrary mediaLibrary = new MediaLibrary();
        return saveToCameraRoll ? mediaLibrary.SavePictureToCameraRoll(name, 
            (Stream) memoryStream) : mediaLibrary.SavePicture(name, (Stream) memoryStream);
      }
    }
  }
}
