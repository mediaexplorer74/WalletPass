// Decompiled with JetBrains decompiler
// Type: WalletPass.ToolStackPNGWriterLib.PNGWriter
// Assembly: Wallet Pass, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1E8AC314-47AB-4931-BC36-563B31C55EF5
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Wallet Pass.dll

using System;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WalletPass.ToolStackCRCLib;

namespace WalletPass.ToolStackPNGWriterLib
{
  public class PNGWriter
  {
    private const int MaxBlockSize = 65535;
    private const double DefaultDensityX = 75.0;
    private const double DefaultDensityY = 75.0;
    private static Stream _stream;
    private static WriteableBitmap _image;
    private static bool IsWritingGamma = true;
    private static double Gamma = 2.2000000476837158;
    private int[] WBByteOrder = new int[4]{ 2, 1, 0, 3 };
    private bool WBBODetectionRun;

    public void DetectWBByteOrder()
    {
      if (this.WBBODetectionRun)
        return;
      WriteableBitmap writeableBitmap = new WriteableBitmap(3, 1);
      Rectangle rectangle1 = new Rectangle();
      ((FrameworkElement) rectangle1).Width = 1.0;
      ((FrameworkElement) rectangle1).Height = 1.0;
      ((Shape) rectangle1).Fill = (Brush) new SolidColorBrush(Colors.Red);
      Rectangle rectangle2 = new Rectangle();
      ((FrameworkElement) rectangle2).Width = 1.0;
      ((FrameworkElement) rectangle2).Height = 1.0;
      ((Shape) rectangle2).Fill = (Brush) new SolidColorBrush(Colors.Green);
      Rectangle rectangle3 = new Rectangle();
      ((FrameworkElement) rectangle3).Width = 1.0;
      ((FrameworkElement) rectangle3).Height = 1.0;
      ((Shape) rectangle3).Fill = (Brush) new SolidColorBrush(Colors.Blue);
      writeableBitmap.Render((UIElement) rectangle1, (Transform) new TranslateTransform()
      {
        X = 0.0,
        Y = 0.0
      });
      writeableBitmap.Render((UIElement) rectangle2, (Transform) new TranslateTransform()
      {
        X = 1.0,
        Y = 0.0
      });
      writeableBitmap.Render((UIElement) rectangle3, (Transform) new TranslateTransform()
      {
        X = 2.0,
        Y = 0.0
      });
      writeableBitmap.Invalidate();
      byte[] bytes1 = BitConverter.GetBytes(writeableBitmap.Pixels[0]);
      byte[] bytes2 = BitConverter.GetBytes(writeableBitmap.Pixels[1]);
      byte[] bytes3 = BitConverter.GetBytes(writeableBitmap.Pixels[2]);
      int index = 4;
      int num1 = 4;
      int num2 = 4;
      int num3 = 4;
      if ((int) bytes1[0] == (int) bytes2[0] && (int) bytes3[0] == (int) bytes2[0])
        index = 0;
      if ((int) bytes1[1] == (int) bytes2[1] && (int) bytes3[1] == (int) bytes2[1])
        index = 1;
      if ((int) bytes1[2] == (int) bytes2[2] && (int) bytes3[2] == (int) bytes2[2])
        index = 2;
      if ((int) bytes1[3] == (int) bytes2[3] && (int) bytes3[3] == (int) bytes2[3])
        index = 3;
      if (index != 4)
      {
        bytes1[index] = (byte) 0;
        bytes2[index] = (byte) 0;
        bytes3[index] = (byte) 0;
        if (bytes1[0] == byte.MaxValue)
          num1 = 0;
        else if (bytes1[1] == byte.MaxValue)
          num1 = 1;
        else if (bytes1[2] == byte.MaxValue)
          num1 = 2;
        else if (bytes1[3] == byte.MaxValue)
          num1 = 3;
        if (bytes2[0] == (byte) 128)
          num2 = 0;
        else if (bytes2[1] == (byte) 128)
          num2 = 1;
        else if (bytes2[2] == (byte) 128)
          num2 = 2;
        else if (bytes2[3] == (byte) 128)
          num2 = 3;
        if (bytes3[0] == byte.MaxValue)
          num3 = 0;
        else if (bytes3[1] == byte.MaxValue)
          num3 = 1;
        else if (bytes3[2] == byte.MaxValue)
          num3 = 2;
        else if (bytes3[3] == byte.MaxValue)
          num3 = 3;
      }
      if (num1 == 4 || num2 == 4 || num3 == 4 || index == 4)
      {
        this.WBByteOrder = new int[4]{ 2, 1, 0, 3 };
      }
      else
      {
        this.WBBODetectionRun = true;
        this.WBByteOrder = new int[4]
        {
          num1,
          num2,
          num3,
          index
        };
      }
    }

    public void WritePNG(WriteableBitmap image, Stream stream) => this.WritePNG(image, stream, -1);

    public void WritePNG(WriteableBitmap image, Stream stream, int compression)
    {
      PNGWriter._image = image;
      PNGWriter._stream = stream;
      stream.Write(new byte[8]
      {
        (byte) 137,
        (byte) 80,
        (byte) 78,
        (byte) 71,
        (byte) 13,
        (byte) 10,
        (byte) 26,
        (byte) 10
      }, 0, 8);
      this.WriteHeaderChunk(new PngHeader()
      {
        Width = ((BitmapSource) image).PixelWidth,
        Height = ((BitmapSource) image).PixelHeight,
        ColorType = (byte) 6,
        BitDepth = (byte) 8,
        FilterMethod = (byte) 0,
        CompressionMethod = (byte) 0,
        InterlaceMethod = (byte) 0
      });
      this.WritePhysicsChunk();
      this.WriteGammaChunk();
      switch (compression)
      {
        case -1:
          this.WriteDataChunksUncompressed();
          break;
        case 0:
          this.WriteDataChunksUncompressed();
          break;
        default:
          this.WriteDataChunks(compression);
          break;
      }
      this.WriteEndChunk();
      stream.Flush();
    }

    private void WritePhysicsChunk()
    {
      int num1 = (int) Math.Round(2952.7559025);
      int num2 = (int) Math.Round(2952.7559025);
      byte[] data = new byte[9];
      this.WriteInteger(data, 0, num1);
      this.WriteInteger(data, 4, num2);
      data[8] = (byte) 1;
      this.WriteChunk("pHYs", data);
    }

    private void WriteGammaChunk()
    {
      if (!PNGWriter.IsWritingGamma)
        return;
      int num = (int) (PNGWriter.Gamma * 100000.0);
      byte[] data = new byte[4];
      byte[] bytes = BitConverter.GetBytes(num);
      data[0] = bytes[3];
      data[1] = bytes[2];
      data[2] = bytes[1];
      data[3] = bytes[0];
      this.WriteChunk("gAMA", data);
    }

    private void WriteDataChunks(int compression) => this.WriteDataChunksUncompressed();

    private void WriteDataChunksUncompressed()
    {
      int num1 = ((BitmapSource) PNGWriter._image).PixelWidth * ((BitmapSource) PNGWriter._image).PixelHeight * 4 + ((BitmapSource) PNGWriter._image).PixelHeight;
      int num2;
      int num3;
      if (num1 % (int) ushort.MaxValue == 0)
      {
        num2 = num1 / (int) ushort.MaxValue;
        num3 = (int) ushort.MaxValue;
      }
      else
      {
        num2 = num1 / (int) ushort.MaxValue + 1;
        num3 = num1 - (int) ushort.MaxValue * (num2 - 1);
      }
      int length = 11 + (num2 - 1) * 65540 + num3;
      byte[] data = new byte[length];
      data[0] = (byte) 120;
      data[1] = (byte) 218;
      Adler32 adler32 = new Adler32();
      adler32.resetAdler();
      int num4 = 0;
      int num5 = 0;
      int offset = 2;
      int index1 = 0;
      byte[] numArray1 = new byte[4];
      for (int index2 = 0; index2 < ((BitmapSource) PNGWriter._image).PixelHeight; ++index2)
      {
        if (num4 == 0)
        {
          byte[] numArray2 = new byte[2];
          ++num5;
          int num6 = (int) ushort.MaxValue;
          if (num5 == num2)
          {
            num6 = num3;
            data[offset] = (byte) 1;
          }
          else
            data[offset] = (byte) 0;
          int index3 = offset + 1;
          byte[] bytes1 = BitConverter.GetBytes(num6);
          data[index3] = bytes1[0];
          data[index3 + 1] = bytes1[1];
          int index4 = index3 + 2;
          byte[] bytes2 = BitConverter.GetBytes((ushort) ~num6);
          data[index4] = bytes2[0];
          data[index4 + 1] = bytes2[1];
          offset = index4 + 2;
          num4 = num6;
        }
        data[offset] = (byte) 0;
        adler32.addToAdler(data, 1, (uint) offset);
        ++offset;
        --num4;
        for (int index5 = 0; index5 < ((BitmapSource) PNGWriter._image).PixelWidth; ++index5)
        {
          byte[] bytes3 = BitConverter.GetBytes(PNGWriter._image.Pixels[index1]);
          for (int index6 = 0; index6 < 4; ++index6)
          {
            if (num4 == 0)
            {
              byte[] numArray3 = new byte[2];
              ++num5;
              int num7 = (int) ushort.MaxValue;
              if (num5 == num2)
              {
                num7 = num3;
                data[offset] = (byte) 1;
              }
              else
                data[offset] = (byte) 0;
              int index7 = offset + 1;
              byte[] bytes4 = BitConverter.GetBytes(num7);
              data[index7] = bytes4[0];
              data[index7 + 1] = bytes4[1];
              int index8 = index7 + 2;
              byte[] bytes5 = BitConverter.GetBytes((ushort) ~num7);
              data[index8] = bytes5[0];
              data[index8 + 1] = bytes5[1];
              offset = index8 + 2;
              num4 = num7;
            }
            data[offset] = bytes3[this.WBByteOrder[index6]];
            adler32.addToAdler(data, 1, (uint) offset);
            ++offset;
            --num4;
          }
          ++index1;
        }
      }
      byte[] bytes = BitConverter.GetBytes(adler32.adler());
      data[offset] = bytes[3];
      data[offset + 1] = bytes[2];
      data[offset + 2] = bytes[1];
      data[offset + 3] = bytes[0];
      this.WriteChunk("IDAT", data, 0, length);
    }

    private void WriteEndChunk() => this.WriteChunk("IEND", (byte[]) null);

    private void WriteHeaderChunk(PngHeader header)
    {
      byte[] data = new byte[13];
      this.WriteInteger(data, 0, header.Width);
      this.WriteInteger(data, 4, header.Height);
      data[8] = header.BitDepth;
      data[9] = header.ColorType;
      data[10] = header.CompressionMethod;
      data[11] = header.FilterMethod;
      data[12] = header.InterlaceMethod;
      this.WriteChunk("IHDR", data);
    }

    private void WriteChunk(string type, byte[] data) => this.WriteChunk(type, data, 0, data != null ? data.Length : 0);

    private void WriteChunk(string type, byte[] data, int offset, int length)
    {
      this.WriteInteger(PNGWriter._stream, length);
      byte[] numArray = new byte[4]
      {
        (byte) type[0],
        (byte) type[1],
        (byte) type[2],
        (byte) type[3]
      };
      PNGWriter._stream.Write(numArray, 0, 4);
      if (data != null)
        PNGWriter._stream.Write(data, offset, length);
      CRC32 crC32 = new CRC32();
      crC32.addToCRC(numArray, 4);
      if (data != null)
        crC32.addToCRC(data, length, (uint) offset);
      this.WriteInteger(PNGWriter._stream, crC32.crc());
    }

    private void WriteInteger(byte[] data, int offset, int value)
    {
      byte[] bytes = BitConverter.GetBytes(value);
      Array.Reverse((Array) bytes);
      Array.Copy((Array) bytes, 0, (Array) data, offset, 4);
    }

    private void WriteInteger(Stream stream, int value)
    {
      byte[] bytes = BitConverter.GetBytes(value);
      Array.Reverse((Array) bytes);
      stream.Write(bytes, 0, 4);
    }

    private void WriteInteger(Stream stream, uint value)
    {
      byte[] bytes = BitConverter.GetBytes(value);
      Array.Reverse((Array) bytes);
      stream.Write(bytes, 0, 4);
    }
  }
}
