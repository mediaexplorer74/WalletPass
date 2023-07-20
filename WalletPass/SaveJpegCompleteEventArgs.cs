// WalletPass.SaveJpegCompleteEventArgs

using System;

namespace WalletPass
{
  public class SaveJpegCompleteEventArgs
  {
    public bool success { get; set; }

    public Exception exception { get; set; }

    public string imageFilename { get; set; }

    public SaveJpegCompleteEventArgs(bool success, string filename)
    {
      this.success = success;
      this.imageFilename = filename;
    }
  }
}
