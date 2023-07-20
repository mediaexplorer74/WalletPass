//  System.Windows.Media.Imaging.WriteableBitmapExtensions

using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Xaml.Media.Imaging;

namespace System.Windows.Media.Imaging
{
  /// <summary>
  /// Collection of extension methods for the WriteableBitmap class.
  /// </summary>
  /// <summary>
  /// Collection of filter / convolution extension methods for the WriteableBitmap class.
  /// </summary>
  /// <summary>
  /// Collection of transformation extension methods for the WriteableBitmap class.
  /// </summary>
  /// <summary>
  /// Collection of extension methods for the WriteableBitmap class.
  /// </summary>
  /// <summary>
  /// Collection of blit (copy) extension methods for the WriteableBitmap class.
  /// </summary>
  /// <summary>
  /// Collection of interchange extension methods for the WriteableBitmap class.
  /// </summary>
  /// <summary>
  /// Collection of extension methods for the WriteableBitmap class.
  /// </summary>
  /// <summary>
  /// Collection of draw spline extension methods for the WriteableBitmap class.
  /// </summary>
  public static class WriteableBitmapExtensions
  {
    internal const int SizeOfArgb = 4;
    private const float StepFactor = 2f;
    /// <summary>Gaussian blur kernel with the size 5x5</summary>
    public static int[,] KernelGaussianBlur5x5 = new int[5, 5]
    {
      {
        1,
        4,
        7,
        4,
        1
      },
      {
        4,
        16,
        26,
        16,
        4
      },
      {
        7,
        26,
        41,
        26,
        7
      },
      {
        4,
        16,
        26,
        16,
        4
      },
      {
        1,
        4,
        7,
        4,
        1
      }
    };
    /// <summary>Gaussian blur kernel with the size 3x3</summary>
    public static int[,] KernelGaussianBlur3x3 = new int[3, 3]
    {
      {
        16,
        26,
        16
      },
      {
        26,
        41,
        26
      },
      {
        16,
        26,
        16
      }
    };
    /// <summary>Sharpen kernel with the size 3x3</summary>
    public static int[,] KernelSharpen3x3 = new int[3, 3]
    {
      {
        0,
        -2,
        0
      },
      {
        -2,
        11,
        -2
      },
      {
        0,
        -2,
        0
      }
    };

    /// <summary>
    /// Draws a filled rectangle.
    /// x2 has to be greater than x1 and y2 has to be greater than y1.
    /// </summary>
    /// <param name="bmp">The WriteableBitmap.</param>
    /// <param name="x1">The x-coordinate of the bounding rectangle's left side.</param>
    /// <param name="y1">The y-coordinate of the bounding rectangle's top side.</param>
    /// <param name="x2">The x-coordinate of the bounding rectangle's right side.</param>
    /// <param name="y2">The y-coordinate of the bounding rectangle's bottom side.</param>
    /// <param name="color">The color.</param>
    public static void FillRectangle(
      this WriteableBitmap bmp,
      int x1,
      int y1,
      int x2,
      int y2,
      Color color)
    {
      int color1 = WriteableBitmapExtensions.ConvertColor(color);
      bmp.FillRectangle(x1, y1, x2, y2, color1);
    }

    /// <summary>
    /// Draws a filled rectangle.
    /// x2 has to be greater than x1 and y2 has to be greater than y1.
    /// </summary>
    /// <param name="bmp">The WriteableBitmap.</param>
    /// <param name="x1">The x-coordinate of the bounding rectangle's left side.</param>
    /// <param name="y1">The y-coordinate of the bounding rectangle's top side.</param>
    /// <param name="x2">The x-coordinate of the bounding rectangle's right side.</param>
    /// <param name="y2">The y-coordinate of the bounding rectangle's bottom side.</param>
    /// <param name="color">The color.</param>
    public static void FillRectangle(
      this WriteableBitmap bmp,
      int x1,
      int y1,
      int x2,
      int y2,
      int color)
    {
      using (BitmapContext bitmapContext = bmp.GetBitmapContext())
      {
        int width = bitmapContext.Width;
        int height = bitmapContext.Height;
        int[] pixels = bitmapContext.Pixels;
        if (x1 < 0 && x2 < 0 || y1 < 0 && y2 < 0 || x1 >= width && x2 >= width || y1 >= height && y2 >= height)
          return;
        if (x1 < 0)
          x1 = 0;
        if (y1 < 0)
          y1 = 0;
        if (x2 < 0)
          x2 = 0;
        if (y2 < 0)
          y2 = 0;
        if (x1 >= width)
          x1 = width - 1;
        if (y1 >= height)
          y1 = height - 1;
        if (x2 >= width)
          x2 = width - 1;
        if (y2 >= height)
          y2 = height - 1;
        int num1 = y1 * width;
        int num2 = num1 + x1;
        int num3 = num1 + x2;
        for (int index = num2; index <= num3; ++index)
          pixels[index] = color;
        int count = (x2 - x1 + 1) * 4;
        int srcOffset = num2 * 4;
        int num4 = y2 * width + x1;
        for (int index = num2 + width; index <= num4; index += width)
          BitmapContext.BlockCopy(bitmapContext, srcOffset, bitmapContext, index * 4, count);
      }
    }

    /// <summary>
    /// A Fast Bresenham Type Algorithm For Drawing filled ellipses http://homepage.smc.edu/kennedy_john/belipse.pdf
    /// x2 has to be greater than x1 and y2 has to be greater than y1.
    /// </summary>
    /// <param name="bmp">The WriteableBitmap.</param>
    /// <param name="x1">The x-coordinate of the bounding rectangle's left side.</param>
    /// <param name="y1">The y-coordinate of the bounding rectangle's top side.</param>
    /// <param name="x2">The x-coordinate of the bounding rectangle's right side.</param>
    /// <param name="y2">The y-coordinate of the bounding rectangle's bottom side.</param>
    /// <param name="color">The color for the line.</param>
    public static void FillEllipse(
      this WriteableBitmap bmp,
      int x1,
      int y1,
      int x2,
      int y2,
      Color color)
    {
      int color1 = WriteableBitmapExtensions.ConvertColor(color);
      bmp.FillEllipse(x1, y1, x2, y2, color1);
    }

    /// <summary>
    /// A Fast Bresenham Type Algorithm For Drawing filled ellipses http://homepage.smc.edu/kennedy_john/belipse.pdf
    /// x2 has to be greater than x1 and y2 has to be greater than y1.
    /// </summary>
    /// <param name="bmp">The WriteableBitmap.</param>
    /// <param name="x1">The x-coordinate of the bounding rectangle's left side.</param>
    /// <param name="y1">The y-coordinate of the bounding rectangle's top side.</param>
    /// <param name="x2">The x-coordinate of the bounding rectangle's right side.</param>
    /// <param name="y2">The y-coordinate of the bounding rectangle's bottom side.</param>
    /// <param name="color">The color for the line.</param>
    public static void FillEllipse(
      this WriteableBitmap bmp,
      int x1,
      int y1,
      int x2,
      int y2,
      int color)
    {
      int xr = x2 - x1 >> 1;
      int yr = y2 - y1 >> 1;
      int xc = x1 + xr;
      int yc = y1 + yr;
      bmp.FillEllipseCentered(xc, yc, xr, yr, color);
    }

    /// <summary>
    /// A Fast Bresenham Type Algorithm For Drawing filled ellipses http://homepage.smc.edu/kennedy_john/belipse.pdf
    /// Uses a different parameter representation than DrawEllipse().
    /// </summary>
    /// <param name="bmp">The WriteableBitmap.</param>
    /// <param name="xc">The x-coordinate of the ellipses center.</param>
    /// <param name="yc">The y-coordinate of the ellipses center.</param>
    /// <param name="xr">The radius of the ellipse in x-direction.</param>
    /// <param name="yr">The radius of the ellipse in y-direction.</param>
    /// <param name="color">The color for the line.</param>
    public static void FillEllipseCentered(
      this WriteableBitmap bmp,
      int xc,
      int yc,
      int xr,
      int yr,
      Color color)
    {
      int color1 = WriteableBitmapExtensions.ConvertColor(color);
      bmp.FillEllipseCentered(xc, yc, xr, yr, color1);
    }

    /// <summary>
    /// A Fast Bresenham Type Algorithm For Drawing filled ellipses http://homepage.smc.edu/kennedy_john/belipse.pdf
    /// Uses a different parameter representation than DrawEllipse().
    /// </summary>
    /// <param name="bmp">The WriteableBitmap.</param>
    /// <param name="xc">The x-coordinate of the ellipses center.</param>
    /// <param name="yc">The y-coordinate of the ellipses center.</param>
    /// <param name="xr">The radius of the ellipse in x-direction.</param>
    /// <param name="yr">The radius of the ellipse in y-direction.</param>
    /// <param name="color">The color for the line.</param>
    public static void FillEllipseCentered(
      this WriteableBitmap bmp,
      int xc,
      int yc,
      int xr,
      int yr,
      int color)
    {
      using (BitmapContext bitmapContext = bmp.GetBitmapContext())
      {
        int[] pixels = bitmapContext.Pixels;
        int width = bitmapContext.Width;
        int height = bitmapContext.Height;
        if (xr < 1 || yr < 1)
          return;
        int num1 = xr;
        int num2 = 0;
        int num3 = xr * xr << 1;
        int num4 = yr * yr << 1;
        int num5 = yr * yr * (1 - (xr << 1));
        int num6 = xr * xr;
        int num7 = 0;
        int num8 = num4 * xr;
        int num9 = 0;
        while (num8 >= num9)
        {
          int num10 = yc + num2;
          int num11 = yc - num2;
          if (num10 < 0)
            num10 = 0;
          if (num10 >= height)
            num10 = height - 1;
          if (num11 < 0)
            num11 = 0;
          if (num11 >= height)
            num11 = height - 1;
          int num12 = num10 * width;
          int num13 = num11 * width;
          int num14 = xc + num1;
          int num15 = xc - num1;
          if (num14 < 0)
            num14 = 0;
          if (num14 >= width)
            num14 = width - 1;
          if (num15 < 0)
            num15 = 0;
          if (num15 >= width)
            num15 = width - 1;
          for (int index = num15; index <= num14; ++index)
          {
            pixels[index + num12] = color;
            pixels[index + num13] = color;
          }
          ++num2;
          num9 += num3;
          num7 += num6;
          num6 += num3;
          if (num5 + (num7 << 1) > 0)
          {
            --num1;
            num8 -= num4;
            num7 += num5;
            num5 += num4;
          }
        }
        int num16 = 0;
        int num17 = yr;
        int num18 = yc + num17;
        int num19 = yc - num17;
        if (num18 < 0)
          num18 = 0;
        if (num18 >= height)
          num18 = height - 1;
        if (num19 < 0)
          num19 = 0;
        if (num19 >= height)
          num19 = height - 1;
        int num20 = num18 * width;
        int num21 = num19 * width;
        int num22 = yr * yr;
        int num23 = xr * xr * (1 - (yr << 1));
        int num24 = 0;
        int num25 = 0;
        int num26 = num3 * yr;
        while (num25 <= num26)
        {
          int num27 = xc + num16;
          int num28 = xc - num16;
          if (num27 < 0)
            num27 = 0;
          if (num27 >= width)
            num27 = width - 1;
          if (num28 < 0)
            num28 = 0;
          if (num28 >= width)
            num28 = width - 1;
          for (int index = num28; index <= num27; ++index)
          {
            pixels[index + num20] = color;
            pixels[index + num21] = color;
          }
          ++num16;
          num25 += num4;
          num24 += num22;
          num22 += num4;
          if (num23 + (num24 << 1) > 0)
          {
            --num17;
            int num29 = yc + num17;
            int num30 = yc - num17;
            if (num29 < 0)
              num29 = 0;
            if (num29 >= height)
              num29 = height - 1;
            if (num30 < 0)
              num30 = 0;
            if (num30 >= height)
              num30 = height - 1;
            num20 = num29 * width;
            num21 = num30 * width;
            num26 -= num3;
            num24 += num23;
            num23 += num3;
          }
        }
      }
    }

    /// <summary>
    /// Draws a filled polygon. Add the first point also at the end of the array if the line should be closed.
    /// </summary>
    /// <param name="bmp">The WriteableBitmap.</param>
    /// <param name="points">The points of the polygon in x and y pairs, therefore the array is interpreted as (x1, y1, x2, y2, ..., xn, yn).</param>
    /// <param name="color">The color for the line.</param>
    public static void FillPolygon(this WriteableBitmap bmp, int[] points, Color color)
    {
      int color1 = WriteableBitmapExtensions.ConvertColor(color);
      bmp.FillPolygon(points, color1);
    }

    /// <summary>
    /// Draws a filled polygon. Add the first point also at the end of the array if the line should be closed.
    /// </summary>
    /// <param name="bmp">The WriteableBitmap.</param>
    /// <param name="points">The points of the polygon in x and y pairs, therefore the array is interpreted as (x1, y1, x2, y2, ..., xn, yn).</param>
    /// <param name="color">The color for the line.</param>
    public static void FillPolygon(this WriteableBitmap bmp, int[] points, int color)
    {
      using (BitmapContext bitmapContext = bmp.GetBitmapContext())
      {
        int width = bitmapContext.Width;
        int height = bitmapContext.Height;
        int[] pixels = bitmapContext.Pixels;
        int length = points.Length;
        int[] numArray = new int[points.Length >> 1];
        int num1 = height;
        int num2 = 0;
        for (int index = 1; index < length; index += 2)
        {
          int point = points[index];
          if (point < num1)
            num1 = point;
          if (point > num2)
            num2 = point;
        }
        if (num1 < 0)
          num1 = 0;
        if (num2 >= height)
          num2 = height - 1;
        for (int index1 = num1; index1 <= num2; ++index1)
        {
          float num3 = (float) points[0];
          float num4 = (float) points[1];
          int num5 = 0;
          for (int index2 = 2; index2 < length; index2 += 2)
          {
            float point1 = (float) points[index2];
            float point2 = (float) points[index2 + 1];
            if ((double) num4 < (double) index1 && (double) point2 >= (double) index1 || (double) point2 < (double) index1 && (double) num4 >= (double) index1)
              numArray[num5++] = (int) ((double) num3 + ((double) index1 - (double) num4) / ((double) point2 - (double) num4) * ((double) point1 - (double) num3));
            num3 = point1;
            num4 = point2;
          }
          for (int index3 = 1; index3 < num5; ++index3)
          {
            int num6 = numArray[index3];
            int index4;
            for (index4 = index3; index4 > 0 && numArray[index4 - 1] > num6; --index4)
              numArray[index4] = numArray[index4 - 1];
            numArray[index4] = num6;
          }
          for (int index5 = 0; index5 < num5 - 1; index5 += 2)
          {
            int num7 = numArray[index5];
            int num8 = numArray[index5 + 1];
            if (num8 > 0 && num7 < width)
            {
              if (num7 < 0)
                num7 = 0;
              if (num8 >= width)
                num8 = width - 1;
              for (int index6 = num7; index6 <= num8; ++index6)
                pixels[index1 * width + index6] = color;
            }
          }
        }
      }
    }

    /// <summary>Draws a filled quad.</summary>
    /// <param name="bmp">The WriteableBitmap.</param>
    /// <param name="x1">The x-coordinate of the 1st point.</param>
    /// <param name="y1">The y-coordinate of the 1st point.</param>
    /// <param name="x2">The x-coordinate of the 2nd point.</param>
    /// <param name="y2">The y-coordinate of the 2nd point.</param>
    /// <param name="x3">The x-coordinate of the 3rd point.</param>
    /// <param name="y3">The y-coordinate of the 3rd point.</param>
    /// <param name="x4">The x-coordinate of the 4th point.</param>
    /// <param name="y4">The y-coordinate of the 4th point.</param>
    /// <param name="color">The color.</param>
    public static void FillQuad(
      this WriteableBitmap bmp,
      int x1,
      int y1,
      int x2,
      int y2,
      int x3,
      int y3,
      int x4,
      int y4,
      Color color)
    {
      int color1 = WriteableBitmapExtensions.ConvertColor(color);
      bmp.FillQuad(x1, y1, x2, y2, x3, y3, x4, y4, color1);
    }

    /// <summary>Draws a filled quad.</summary>
    /// <param name="bmp">The WriteableBitmap.</param>
    /// <param name="x1">The x-coordinate of the 1st point.</param>
    /// <param name="y1">The y-coordinate of the 1st point.</param>
    /// <param name="x2">The x-coordinate of the 2nd point.</param>
    /// <param name="y2">The y-coordinate of the 2nd point.</param>
    /// <param name="x3">The x-coordinate of the 3rd point.</param>
    /// <param name="y3">The y-coordinate of the 3rd point.</param>
    /// <param name="x4">The x-coordinate of the 4th point.</param>
    /// <param name="y4">The y-coordinate of the 4th point.</param>
    /// <param name="color">The color.</param>
    public static void FillQuad(
      this WriteableBitmap bmp,
      int x1,
      int y1,
      int x2,
      int y2,
      int x3,
      int y3,
      int x4,
      int y4,
      int color)
    {
      bmp.FillPolygon(new int[10]
      {
        x1,
        y1,
        x2,
        y2,
        x3,
        y3,
        x4,
        y4,
        x1,
        y1
      }, color);
    }

    /// <summary>Draws a filled triangle.</summary>
    /// <param name="bmp">The WriteableBitmap.</param>
    /// <param name="x1">The x-coordinate of the 1st point.</param>
    /// <param name="y1">The y-coordinate of the 1st point.</param>
    /// <param name="x2">The x-coordinate of the 2nd point.</param>
    /// <param name="y2">The y-coordinate of the 2nd point.</param>
    /// <param name="x3">The x-coordinate of the 3rd point.</param>
    /// <param name="y3">The y-coordinate of the 3rd point.</param>
    /// <param name="color">The color.</param>
    public static void FillTriangle(
      this WriteableBitmap bmp,
      int x1,
      int y1,
      int x2,
      int y2,
      int x3,
      int y3,
      Color color)
    {
      int color1 = WriteableBitmapExtensions.ConvertColor(color);
      bmp.FillTriangle(x1, y1, x2, y2, x3, y3, color1);
    }

    /// <summary>Draws a filled triangle.</summary>
    /// <param name="bmp">The WriteableBitmap.</param>
    /// <param name="x1">The x-coordinate of the 1st point.</param>
    /// <param name="y1">The y-coordinate of the 1st point.</param>
    /// <param name="x2">The x-coordinate of the 2nd point.</param>
    /// <param name="y2">The y-coordinate of the 2nd point.</param>
    /// <param name="x3">The x-coordinate of the 3rd point.</param>
    /// <param name="y3">The y-coordinate of the 3rd point.</param>
    /// <param name="color">The color.</param>
    public static void FillTriangle(
      this WriteableBitmap bmp,
      int x1,
      int y1,
      int x2,
      int y2,
      int x3,
      int y3,
      int color)
    {
      bmp.FillPolygon(new int[8]
      {
        x1,
        y1,
        x2,
        y2,
        x3,
        y3,
        x1,
        y1
      }, color);
    }

    /// <summary>
    /// Draws a filled, cubic Beziér spline defined by start, end and two control points.
    /// </summary>
    /// <param name="x1">The x-coordinate of the start point.</param>
    /// <param name="y1">The y-coordinate of the start point.</param>
    /// <param name="cx1">The x-coordinate of the 1st control point.</param>
    /// <param name="cy1">The y-coordinate of the 1st control point.</param>
    /// <param name="cx2">The x-coordinate of the 2nd control point.</param>
    /// <param name="cy2">The y-coordinate of the 2nd control point.</param>
    /// <param name="x2">The x-coordinate of the end point.</param>
    /// <param name="y2">The y-coordinate of the end point.</param>
    /// <param name="color">The color.</param>
    /// <param name="context">The context with the pixels.</param>
    /// <param name="w">The width of the bitmap.</param>
    /// <param name="h">The height of the bitmap.</param>
    private static List<int> ComputeBezierPoints(
      int x1,
      int y1,
      int cx1,
      int cy1,
      int cx2,
      int cy2,
      int x2,
      int y2,
      int color,
      BitmapContext context,
      int w,
      int h)
    {
      int[] pixels = context.Pixels;
      int num1 = Math.Min(x1, Math.Min(cx1, Math.Min(cx2, x2)));
      int num2 = Math.Min(y1, Math.Min(cy1, Math.Min(cy2, y2)));
      int num3 = Math.Max(x1, Math.Max(cx1, Math.Max(cx2, x2)));
      int num4 = Math.Max(y1, Math.Max(cy1, Math.Max(cy2, y2)));
      int num5 = num3 - num1;
      int num6 = num4 - num2;
      if (num5 > num6)
        num6 = num5;
      List<int> bezierPoints = new List<int>();
      if (num6 != 0)
      {
        float num7 = 2f / (float) num6;
        for (float num8 = 0.0f; (double) num8 <= 1.0; num8 += num7)
        {
          float num9 = num8 * num8;
          float num10 = 1f - num8;
          float num11 = num10 * num10;
          int num12 = (int) ((double) num10 * (double) num11 * (double) x1 + 3.0 * (double) num8 * (double) num11 * (double) cx1 + 3.0 * (double) num10 * (double) num9 * (double) cx2 + (double) num8 * (double) num9 * (double) x2);
          int num13 = (int) ((double) num10 * (double) num11 * (double) y1 + 3.0 * (double) num8 * (double) num11 * (double) cy1 + 3.0 * (double) num10 * (double) num9 * (double) cy2 + (double) num8 * (double) num9 * (double) y2);
          bezierPoints.Add(num12);
          bezierPoints.Add(num13);
        }
        bezierPoints.Add(x2);
        bezierPoints.Add(y2);
      }
      return bezierPoints;
    }

    /// <summary>
    /// Draws a series of filled, cubic Beziér splines each defined by start, end and two control points.
    /// The ending point of the previous curve is used as starting point for the next.
    /// Therfore the inital curve needs four points and the subsequent 3 (2 control and 1 end point).
    /// </summary>
    /// <param name="bmp">The WriteableBitmap.</param>
    /// <param name="points">The points for the curve in x and y pairs, therefore the array is interpreted as (x1, y1, cx1, cy1, cx2, cy2, x2, y2, cx3, cx4 ..., xn, yn).</param>
    /// <param name="color">The color for the spline.</param>
    public static void FillBeziers(this WriteableBitmap bmp, int[] points, Color color)
    {
      int color1 = WriteableBitmapExtensions.ConvertColor(color);
      bmp.FillBeziers(points, color1);
    }

    /// <summary>
    /// Draws a series of filled, cubic Beziér splines each defined by start, end and two control points.
    /// The ending point of the previous curve is used as starting point for the next.
    /// Therfore the inital curve needs four points and the subsequent 3 (2 control and 1 end point).
    /// </summary>
    /// <param name="bmp">The WriteableBitmap.</param>
    /// <param name="points">The points for the curve in x and y pairs, therefore the array is interpreted as (x1, y1, cx1, cy1, cx2, cy2, x2, y2, cx3, cx4 ..., xn, yn).</param>
    /// <param name="color">The color for the spline.</param>
    public static void FillBeziers(this WriteableBitmap bmp, int[] points, int color)
    {
      using (BitmapContext bitmapContext = bmp.GetBitmapContext())
      {
        int width = bitmapContext.Width;
        int height = bitmapContext.Height;
        int x1 = points[0];
        int y1 = points[1];
        List<int> intList = new List<int>();
        for (int index = 2; index + 5 < points.Length; index += 6)
        {
          int point1 = points[index + 4];
          int point2 = points[index + 5];
          intList.AddRange((IEnumerable<int>) WriteableBitmapExtensions.ComputeBezierPoints(x1, y1, points[index], points[index + 1], points[index + 2], points[index + 3], point1, point2, color, bitmapContext, width, height));
          x1 = point1;
          y1 = point2;
        }
        bmp.FillPolygon(intList.ToArray(), color);
      }
    }

    /// <summary>
    /// Computes the discrete segment points of a Cardinal spline (cubic) defined by four control points.
    /// </summary>
    /// <param name="x1">The x-coordinate of the 1st control point.</param>
    /// <param name="y1">The y-coordinate of the 1st control point.</param>
    /// <param name="x2">The x-coordinate of the 2nd control point.</param>
    /// <param name="y2">The y-coordinate of the 2nd control point.</param>
    /// <param name="x3">The x-coordinate of the 3rd control point.</param>
    /// <param name="y3">The y-coordinate of the 3rd control point.</param>
    /// <param name="x4">The x-coordinate of the 4th control point.</param>
    /// <param name="y4">The y-coordinate of the 4th control point.</param>
    /// <param name="tension">The tension of the curve defines the shape. Usually between 0 and 1. 0 would be a straight line.</param>
    /// <param name="color">The color.</param>
    /// <param name="context">The context with the pixels.</param>
    /// <param name="w">The width of the bitmap.</param>
    /// <param name="h">The height of the bitmap.</param>
    private static List<int> ComputeSegmentPoints(
      int x1,
      int y1,
      int x2,
      int y2,
      int x3,
      int y3,
      int x4,
      int y4,
      float tension,
      int color,
      BitmapContext context,
      int w,
      int h)
    {
      int[] pixels = context.Pixels;
      int num1 = Math.Min(x1, Math.Min(x2, Math.Min(x3, x4)));
      int num2 = Math.Min(y1, Math.Min(y2, Math.Min(y3, y4)));
      int num3 = Math.Max(x1, Math.Max(x2, Math.Max(x3, x4)));
      int num4 = Math.Max(y1, Math.Max(y2, Math.Max(y3, y4)));
      int num5 = num3 - num1;
      int num6 = num4 - num2;
      if (num5 > num6)
        num6 = num5;
      List<int> segmentPoints = new List<int>();
      if (num6 != 0)
      {
        float num7 = 2f / (float) num6;
        float num8 = tension * (float) (x3 - x1);
        float num9 = tension * (float) (y3 - y1);
        float num10 = tension * (float) (x4 - x2);
        float num11 = tension * (float) (y4 - y2);
        float num12 = num8 + num10 + (float) (2 * x2) - (float) (2 * x3);
        float num13 = num9 + num11 + (float) (2 * y2) - (float) (2 * y3);
        float num14 = -2f * num8 - num10 - (float) (3 * x2) + (float) (3 * x3);
        float num15 = -2f * num9 - num11 - (float) (3 * y2) + (float) (3 * y3);
        for (float num16 = 0.0f; (double) num16 <= 1.0; num16 += num7)
        {
          float num17 = num16 * num16;
          int num18 = (int) ((double) num12 * (double) num17 * (double) num16 + (double) num14 * (double) num17 + (double) num8 * (double) num16 + (double) x2);
          int num19 = (int) ((double) num13 * (double) num17 * (double) num16 + (double) num15 * (double) num17 + (double) num9 * (double) num16 + (double) y2);
          segmentPoints.Add(num18);
          segmentPoints.Add(num19);
        }
        segmentPoints.Add(x3);
        segmentPoints.Add(y3);
      }
      return segmentPoints;
    }

    /// <summary>
    /// Draws a filled Cardinal spline (cubic) defined by a point collection.
    /// The cardinal spline passes through each point in the collection.
    /// </summary>
    /// <param name="bmp">The WriteableBitmap.</param>
    /// <param name="points">The points for the curve in x and y pairs, therefore the array is interpreted as (x1, y1, x2, y2, x3, y3, x4, y4, x1, x2 ..., xn, yn).</param>
    /// <param name="tension">The tension of the curve defines the shape. Usually between 0 and 1. 0 would be a straight line.</param>
    /// <param name="color">The color for the spline.</param>
    public static void FillCurve(
      this WriteableBitmap bmp,
      int[] points,
      float tension,
      Color color)
    {
      int color1 = WriteableBitmapExtensions.ConvertColor(color);
      bmp.FillCurve(points, tension, color1);
    }

    /// <summary>
    /// Draws a filled Cardinal spline (cubic) defined by a point collection.
    /// The cardinal spline passes through each point in the collection.
    /// </summary>
    /// <param name="bmp">The WriteableBitmap.</param>
    /// <param name="points">The points for the curve in x and y pairs, therefore the array is interpreted as (x1, y1, x2, y2, x3, y3, x4, y4, x1, x2 ..., xn, yn).</param>
    /// <param name="tension">The tension of the curve defines the shape. Usually between 0 and 1. 0 would be a straight line.</param>
    /// <param name="color">The color for the spline.</param>
    public static void FillCurve(this WriteableBitmap bmp, int[] points, float tension, int color)
    {
      using (BitmapContext bitmapContext = bmp.GetBitmapContext())
      {
        int width = bitmapContext.Width;
        int height = bitmapContext.Height;
        List<int> segmentPoints = WriteableBitmapExtensions.ComputeSegmentPoints(points[0], points[1], points[0], points[1], points[2], points[3], points[4], points[5], tension, color, bitmapContext, width, height);
        int index;
        for (index = 2; index < points.Length - 4; index += 2)
          segmentPoints.AddRange((IEnumerable<int>) WriteableBitmapExtensions.ComputeSegmentPoints(points[index - 2], points[index - 1], points[index], points[index + 1], points[index + 2], points[index + 3], points[index + 4], points[index + 5], tension, color, bitmapContext, width, height));
        segmentPoints.AddRange((IEnumerable<int>) WriteableBitmapExtensions.ComputeSegmentPoints(points[index - 2], points[index - 1], points[index], points[index + 1], points[index + 2], points[index + 3], points[index + 2], points[index + 3], tension, color, bitmapContext, width, height));
        bmp.FillPolygon(segmentPoints.ToArray(), color);
      }
    }

    /// <summary>
    /// Draws a filled, closed Cardinal spline (cubic) defined by a point collection.
    /// The cardinal spline passes through each point in the collection.
    /// </summary>
    /// <param name="bmp">The WriteableBitmap.</param>
    /// <param name="points">The points for the curve in x and y pairs, therefore the array is interpreted as (x1, y1, x2, y2, x3, y3, x4, y4, x1, x2 ..., xn, yn).</param>
    /// <param name="tension">The tension of the curve defines the shape. Usually between 0 and 1. 0 would be a straight line.</param>
    /// <param name="color">The color for the spline.</param>
    public static void FillCurveClosed(
      this WriteableBitmap bmp,
      int[] points,
      float tension,
      Color color)
    {
      int color1 = WriteableBitmapExtensions.ConvertColor(color);
      bmp.FillCurveClosed(points, tension, color1);
    }

    /// <summary>
    /// Draws a filled, closed Cardinal spline (cubic) defined by a point collection.
    /// The cardinal spline passes through each point in the collection.
    /// </summary>
    /// <param name="bmp">The WriteableBitmap.</param>
    /// <param name="points">The points for the curve in x and y pairs, therefore the array is interpreted as (x1, y1, x2, y2, x3, y3, x4, y4, x1, x2 ..., xn, yn).</param>
    /// <param name="tension">The tension of the curve defines the shape. Usually between 0 and 1. 0 would be a straight line.</param>
    /// <param name="color">The color for the spline.</param>
    public static void FillCurveClosed(
      this WriteableBitmap bmp,
      int[] points,
      float tension,
      int color)
    {
      using (BitmapContext bitmapContext = bmp.GetBitmapContext())
      {
        int width = bitmapContext.Width;
        int height = bitmapContext.Height;
        int length = points.Length;
        List<int> segmentPoints = WriteableBitmapExtensions.ComputeSegmentPoints(points[length - 2], points[length - 1], points[0], points[1], points[2], points[3], points[4], points[5], tension, color, bitmapContext, width, height);
        int index;
        for (index = 2; index < length - 4; index += 2)
          segmentPoints.AddRange((IEnumerable<int>) WriteableBitmapExtensions.ComputeSegmentPoints(points[index - 2], points[index - 1], points[index], points[index + 1], points[index + 2], points[index + 3], points[index + 4], points[index + 5], tension, color, bitmapContext, width, height));
        segmentPoints.AddRange((IEnumerable<int>) WriteableBitmapExtensions.ComputeSegmentPoints(points[index - 2], points[index - 1], points[index], points[index + 1], points[index + 2], points[index + 3], points[0], points[1], tension, color, bitmapContext, width, height));
        segmentPoints.AddRange((IEnumerable<int>) WriteableBitmapExtensions.ComputeSegmentPoints(points[index], points[index + 1], points[index + 2], points[index + 3], points[0], points[1], points[2], points[3], tension, color, bitmapContext, width, height));
        bmp.FillPolygon(segmentPoints.ToArray(), color);
      }
    }

    /// <summary>Creates a new filtered WriteableBitmap.</summary>
    /// <param name="bmp">The WriteableBitmap.</param>
    /// <param name="kernel">The kernel used for convolution.</param>
    /// <returns>A new WriteableBitmap that is a filtered version of the input.</returns>
    public static WriteableBitmap Convolute(this WriteableBitmap bmp, int[,] kernel)
    {
      int kernelFactorSum = 0;
      int[,] numArray = kernel;
      int upperBound1 = numArray.GetUpperBound(0);
      int upperBound2 = numArray.GetUpperBound(1);
      for (int lowerBound1 = numArray.GetLowerBound(0); lowerBound1 <= upperBound1; ++lowerBound1)
      {
        for (int lowerBound2 = numArray.GetLowerBound(1); lowerBound2 <= upperBound2; ++lowerBound2)
        {
          int num = numArray[lowerBound1, lowerBound2];
          kernelFactorSum += num;
        }
      }
      return bmp.Convolute(kernel, kernelFactorSum, 0);
    }

    /// <summary>Creates a new filtered WriteableBitmap.</summary>
    /// <param name="bmp">The WriteableBitmap.</param>
    /// <param name="kernel">The kernel used for convolution.</param>
    /// <param name="kernelFactorSum">The factor used for the kernel summing.</param>
    /// <param name="kernelOffsetSum">The offset used for the kernel summing.</param>
    /// <returns>A new WriteableBitmap that is a filtered version of the input.</returns>
    public static WriteableBitmap Convolute(
      this WriteableBitmap bmp,
      int[,] kernel,
      int kernelFactorSum,
      int kernelOffsetSum)
    {
      int num1 = kernel.GetUpperBound(0) + 1;
      int num2 = kernel.GetUpperBound(1) + 1;
      if ((num2 & 1) == 0)
        throw new InvalidOperationException("Kernel width must be odd!");
      if ((num1 & 1) == 0)
        throw new InvalidOperationException("Kernel height must be odd!");
      using (BitmapContext bitmapContext1 = bmp.GetBitmapContext(ReadWriteMode.ReadOnly))
      {
        int width = bitmapContext1.Width;
        int height = bitmapContext1.Height;
        WriteableBitmap bmp1 = BitmapFactory.New(width, height);
        using (BitmapContext bitmapContext2 = bmp1.GetBitmapContext())
        {
          int[] pixels1 = bitmapContext1.Pixels;
          int[] pixels2 = bitmapContext2.Pixels;
          int num3 = 0;
          int num4 = num2 >> 1;
          int num5 = num1 >> 1;
          for (int index1 = 0; index1 < height; ++index1)
          {
            for (int index2 = 0; index2 < width; ++index2)
            {
              int num6 = 0;
              int num7 = 0;
              int num8 = 0;
              int num9 = 0;
              for (int index3 = -num4; index3 <= num4; ++index3)
              {
                int num10 = index3 + index2;
                if (num10 < 0)
                  num10 = 0;
                else if (num10 >= width)
                  num10 = width - 1;
                for (int index4 = -num5; index4 <= num5; ++index4)
                {
                  int num11 = index4 + index1;
                  if (num11 < 0)
                    num11 = 0;
                  else if (num11 >= height)
                    num11 = height - 1;
                  int num12 = pixels1[num11 * width + num10];
                  int num13 = kernel[index4 + num4, index3 + num5];
                  num6 += (num12 >> 24 & (int) byte.MaxValue) * num13;
                  num7 += (num12 >> 16 & (int) byte.MaxValue) * num13;
                  num8 += (num12 >> 8 & (int) byte.MaxValue) * num13;
                  num9 += (num12 & (int) byte.MaxValue) * num13;
                }
              }
              int num14 = num6 / kernelFactorSum + kernelOffsetSum;
              int num15 = num7 / kernelFactorSum + kernelOffsetSum;
              int num16 = num8 / kernelFactorSum + kernelOffsetSum;
              int num17 = num9 / kernelFactorSum + kernelOffsetSum;
              byte num18 = num14 > (int) byte.MaxValue ? byte.MaxValue : (num14 < 0 ? (byte) 0 : (byte) num14);
              byte num19 = num15 > (int) byte.MaxValue ? byte.MaxValue : (num15 < 0 ? (byte) 0 : (byte) num15);
              byte num20 = num16 > (int) byte.MaxValue ? byte.MaxValue : (num16 < 0 ? (byte) 0 : (byte) num16);
              byte num21 = num17 > (int) byte.MaxValue ? byte.MaxValue : (num17 < 0 ? (byte) 0 : (byte) num17);
              pixels2[num3++] = (int) num18 << 24 | (int) num19 << 16 | (int) num20 << 8 | (int) num21;
            }
          }
          return bmp1;
        }
      }
    }

    /// <summary>
    /// Creates a new inverted WriteableBitmap and returns it.
    /// </summary>
    /// <param name="bmp">The WriteableBitmap.</param>
    /// <returns>The new inverted WriteableBitmap.</returns>
    public static WriteableBitmap Invert(this WriteableBitmap bmp)
    {
      using (BitmapContext bitmapContext1 = bmp.GetBitmapContext())
      {
        WriteableBitmap bmp1 = BitmapFactory.New(bitmapContext1.Width, bitmapContext1.Height);
        using (BitmapContext bitmapContext2 = bmp1.GetBitmapContext())
        {
          int[] pixels1 = bitmapContext2.Pixels;
          int[] pixels2 = bitmapContext1.Pixels;
          int length = bitmapContext1.Length;
          for (int index = 0; index < length; ++index)
          {
            int num1 = pixels2[index];
            int num2 = num1 >> 24 & (int) byte.MaxValue;
            int num3 = num1 >> 16 & (int) byte.MaxValue;
            int num4 = num1 >> 8 & (int) byte.MaxValue;
            int num5 = num1 & (int) byte.MaxValue;
            int num6 = (int) byte.MaxValue - num3;
            int num7 = (int) byte.MaxValue - num4;
            int num8 = (int) byte.MaxValue - num5;
            pixels1[index] = num2 << 24 | num6 << 16 | num7 << 8 | num8;
          }
          return bmp1;
        }
      }
    }

    /// <summary>Creates a new cropped WriteableBitmap.</summary>
    /// <param name="bmp">The WriteableBitmap.</param>
    /// <param name="x">The x coordinate of the rectangle that defines the crop region.</param>
    /// <param name="y">The y coordinate of the rectangle that defines the crop region.</param>
    /// <param name="width">The width of the rectangle that defines the crop region.</param>
    /// <param name="height">The height of the rectangle that defines the crop region.</param>
    /// <returns>A new WriteableBitmap that is a cropped version of the input.</returns>
    public static WriteableBitmap Crop(
      this WriteableBitmap bmp,
      int x,
      int y,
      int width,
      int height)
    {
      using (BitmapContext bitmapContext1 = bmp.GetBitmapContext())
      {
        int width1 = bitmapContext1.Width;
        int height1 = bitmapContext1.Height;
        if (x > width1 || y > height1)
          return BitmapFactory.New(0, 0);
        if (x < 0)
          x = 0;
        if (x + width > width1)
          width = width1 - x;
        if (y < 0)
          y = 0;
        if (y + height > height1)
          height = height1 - y;
        WriteableBitmap bmp1 = BitmapFactory.New(width, height);
        using (BitmapContext bitmapContext2 = bmp1.GetBitmapContext())
        {
          for (int index = 0; index < height; ++index)
          {
            int srcOffset = ((y + index) * width1 + x) * 4;
            int destOffset = index * width * 4;
            BitmapContext.BlockCopy(bitmapContext1, srcOffset, bitmapContext2, destOffset, width * 4);
          }
          return bmp1;
        }
      }
    }

        /// <summary>Creates a new cropped WriteableBitmap.</summary>
        /// <param name="bmp">The WriteableBitmap.</param>
        /// <param name="region">The rectangle that defines the crop region.</param>
        /// <returns>A new WriteableBitmap that is a cropped version of the input.</returns>
        public static WriteableBitmap Crop(this WriteableBitmap bmp, Rect region)
        {
            return bmp.Crop((int)region.X, (int)region.Y, (int)region.Width, 
                (int)region.Height);
        }

        /// <summary>Creates a new resized WriteableBitmap.</summary>
        /// <param name="bmp">The WriteableBitmap.</param>
        /// <param name="width">The new desired width.</param>
        /// <param name="height">The new desired height.</param>
        /// <param name="interpolation">The interpolation method that should be used.</param>
        /// <returns>A new WriteableBitmap that is a resized version of the input.</returns>
        public static WriteableBitmap Resize(
      this WriteableBitmap bmp,
      int width,
      int height,
      WriteableBitmapExtensions.Interpolation interpolation)
    {
      using (BitmapContext bitmapContext1 = bmp.GetBitmapContext())
      {
        int[] src = WriteableBitmapExtensions.Resize(bitmapContext1, bitmapContext1.Width, bitmapContext1.Height, width, height, interpolation);
        WriteableBitmap bmp1 = BitmapFactory.New(width, height);
        using (BitmapContext bitmapContext2 = bmp1.GetBitmapContext())
          BitmapContext.BlockCopy((Array) src, 0, bitmapContext2, 0, 4 * src.Length);
        return bmp1;
      }
    }

    /// <summary>Creates a new resized bitmap.</summary>
    /// <param name="srcContext">The source context.</param>
    /// <param name="widthSource">The width of the source pixels.</param>
    /// <param name="heightSource">The height of the source pixels.</param>
    /// <param name="width">The new desired width.</param>
    /// <param name="height">The new desired height.</param>
    /// <param name="interpolation">The interpolation method that should be used.</param>
    /// <returns>A new bitmap that is a resized version of the input.</returns>
    public static int[] Resize(
      BitmapContext srcContext,
      int widthSource,
      int heightSource,
      int width,
      int height,
      WriteableBitmapExtensions.Interpolation interpolation)
    {
      return WriteableBitmapExtensions.Resize(srcContext.Pixels, widthSource, heightSource, width, height, interpolation);
    }

    /// <summary>Creates a new resized bitmap.</summary>
    /// <param name="pixels">The source pixels.</param>
    /// <param name="widthSource">The width of the source pixels.</param>
    /// <param name="heightSource">The height of the source pixels.</param>
    /// <param name="width">The new desired width.</param>
    /// <param name="height">The new desired height.</param>
    /// <param name="interpolation">The interpolation method that should be used.</param>
    /// <returns>A new bitmap that is a resized version of the input.</returns>
    public static int[] Resize(
      int[] pixels,
      int widthSource,
      int heightSource,
      int width,
      int height,
      WriteableBitmapExtensions.Interpolation interpolation)
    {
      int[] numArray = new int[width * height];
      float num1 = (float) widthSource / (float) width;
      float num2 = (float) heightSource / (float) height;
      switch (interpolation)
      {
        case WriteableBitmapExtensions.Interpolation.NearestNeighbor:
          int num3 = 0;
          for (int index1 = 0; index1 < height; ++index1)
          {
            for (int index2 = 0; index2 < width; ++index2)
            {
              float num4 = (float) index2 * num1;
              float num5 = (float) index1 * num2;
              int num6 = (int) num4;
              int num7 = (int) num5;
              numArray[num3++] = pixels[num7 * widthSource + num6];
            }
          }
          break;
        case WriteableBitmapExtensions.Interpolation.Bilinear:
          int num8 = 0;
          for (int index3 = 0; index3 < height; ++index3)
          {
            for (int index4 = 0; index4 < width; ++index4)
            {
              float num9 = (float) index4 * num1;
              float num10 = (float) index3 * num2;
              int num11 = (int) num9;
              int num12 = (int) num10;
              float num13 = num9 - (float) num11;
              float num14 = num10 - (float) num12;
              float num15 = 1f - num13;
              float num16 = 1f - num14;
              int num17 = num11 + 1;
              if (num17 >= widthSource)
                num17 = num11;
              int num18 = num12 + 1;
              if (num18 >= heightSource)
                num18 = num12;
              int pixel1 = pixels[num12 * widthSource + num11];
              byte num19 = (byte) (pixel1 >> 24);
              byte num20 = (byte) (pixel1 >> 16);
              byte num21 = (byte) (pixel1 >> 8);
              byte num22 = (byte) pixel1;
              int pixel2 = pixels[num12 * widthSource + num17];
              byte num23 = (byte) (pixel2 >> 24);
              byte num24 = (byte) (pixel2 >> 16);
              byte num25 = (byte) (pixel2 >> 8);
              byte num26 = (byte) pixel2;
              int pixel3 = pixels[num18 * widthSource + num11];
              byte num27 = (byte) (pixel3 >> 24);
              byte num28 = (byte) (pixel3 >> 16);
              byte num29 = (byte) (pixel3 >> 8);
              byte num30 = (byte) pixel3;
              int pixel4 = pixels[num18 * widthSource + num17];
              byte num31 = (byte) (pixel4 >> 24);
              byte num32 = (byte) (pixel4 >> 16);
              byte num33 = (byte) (pixel4 >> 8);
              byte num34 = (byte) pixel4;
              float num35 = (float) ((double) num15 * (double) num19 + (double) num13 * (double) num23);
              float num36 = (float) ((double) num15 * (double) num27 + (double) num13 * (double) num31);
              byte num37 = (byte) ((double) num16 * (double) num35 + (double) num14 * (double) num36);
              float num38 = (float) ((double) num15 * (double) num20 + (double) num13 * (double) num24);
              float num39 = (float) ((double) num15 * (double) num28 + (double) num13 * (double) num32);
              float num40 = (float) ((double) num16 * (double) num38 + (double) num14 * (double) num39);
              float num41 = (float) ((double) num15 * (double) num21 + (double) num13 * (double) num25);
              float num42 = (float) ((double) num15 * (double) num29 + (double) num13 * (double) num33);
              float num43 = (float) ((double) num16 * (double) num41 + (double) num14 * (double) num42);
              float num44 = (float) ((double) num15 * (double) num22 + (double) num13 * (double) num26);
              float num45 = (float) ((double) num15 * (double) num30 + (double) num13 * (double) num34);
              float num46 = (float) ((double) num16 * (double) num44 + (double) num14 * (double) num45);
              byte num47 = (byte) num40;
              byte num48 = (byte) num43;
              byte num49 = (byte) num46;
              numArray[num8++] = (int) num37 << 24 | (int) num47 << 16 | (int) num48 << 8 | (int) num49;
            }
          }
          break;
      }
      return numArray;
    }

    /// <summary>
    /// Rotates the bitmap in 90° steps clockwise and returns a new rotated WriteableBitmap.
    /// </summary>
    /// <param name="bmp">The WriteableBitmap.</param>
    /// <param name="angle">The angle in degress the bitmap should be rotated in 90° steps clockwise.</param>
    /// <returns>A new WriteableBitmap that is a rotated version of the input.</returns>
    public static WriteableBitmap Rotate(this WriteableBitmap bmp, int angle)
    {
      using (BitmapContext bitmapContext1 = bmp.GetBitmapContext())
      {
        int width = bitmapContext1.Width;
        int height = bitmapContext1.Height;
        int[] pixels1 = bitmapContext1.Pixels;
        int index1 = 0;
        angle %= 360;
        WriteableBitmap bmp1;
        if (angle > 0 && angle <= 90)
        {
          bmp1 = BitmapFactory.New(height, width);
          using (BitmapContext bitmapContext2 = bmp1.GetBitmapContext())
          {
            int[] pixels2 = bitmapContext2.Pixels;
            for (int index2 = 0; index2 < width; ++index2)
            {
              for (int index3 = height - 1; index3 >= 0; --index3)
              {
                int index4 = index3 * width + index2;
                pixels2[index1] = pixels1[index4];
                ++index1;
              }
            }
          }
        }
        else if (angle > 90 && angle <= 180)
        {
          bmp1 = BitmapFactory.New(width, height);
          using (BitmapContext bitmapContext3 = bmp1.GetBitmapContext())
          {
            int[] pixels3 = bitmapContext3.Pixels;
            for (int index5 = height - 1; index5 >= 0; --index5)
            {
              for (int index6 = width - 1; index6 >= 0; --index6)
              {
                int index7 = index5 * width + index6;
                pixels3[index1] = pixels1[index7];
                ++index1;
              }
            }
          }
        }
        else if (angle > 180 && angle <= 270)
        {
          bmp1 = BitmapFactory.New(height, width);
          using (BitmapContext bitmapContext4 = bmp1.GetBitmapContext())
          {
            int[] pixels4 = bitmapContext4.Pixels;
            for (int index8 = width - 1; index8 >= 0; --index8)
            {
              for (int index9 = 0; index9 < height; ++index9)
              {
                int index10 = index9 * width + index8;
                pixels4[index1] = pixels1[index10];
                ++index1;
              }
            }
          }
        }
        else
          bmp1 = bmp.Clone();
        return bmp1;
      }
    }

    /// <summary>
    /// Rotates the bitmap in any degree returns a new rotated WriteableBitmap.
    /// </summary>
    /// <param name="bmp">The WriteableBitmap.</param>
    /// <param name="angle">Arbitrary angle in 360 Degrees (positive = clockwise).</param>
    /// <param name="crop">if true: keep the size, false: adjust canvas to new size</param>
    /// <returns>A new WriteableBitmap that is a rotated version of the input.</returns>
    public static WriteableBitmap RotateFree(this WriteableBitmap bmp, double angle, bool crop = true)
    {
      double num1 = -1.0 * Math.PI / 180.0 * angle;
      using (BitmapContext bitmapContext1 = bmp.GetBitmapContext())
      {
        int width1 = bitmapContext1.Width;
        int height = bitmapContext1.Height;
        int pixelWidth;
        int pixelHeight;
        if (crop)
        {
          pixelWidth = width1;
          pixelHeight = height;
        }
        else
        {
          double num2 = angle / (180.0 / Math.PI);
          pixelWidth = (int) Math.Ceiling(Math.Abs(Math.Sin(num2) * (double) height) + Math.Abs(Math.Cos(num2) * (double) width1));
          pixelHeight = (int) Math.Ceiling(Math.Abs(Math.Sin(num2) * (double) width1) + Math.Abs(Math.Cos(num2) * (double) height));
        }
        int num3 = width1 / 2;
        int num4 = height / 2;
        int num5 = pixelWidth / 2;
        int num6 = pixelHeight / 2;
        WriteableBitmap bmp1 = BitmapFactory.New(pixelWidth, pixelHeight);
        using (BitmapContext bitmapContext2 = bmp1.GetBitmapContext())
        {
          int[] pixels1 = bitmapContext2.Pixels;
          int[] pixels2 = bitmapContext1.Pixels;
          int width2 = bitmapContext1.Width;
          for (int index1 = 0; index1 < pixelHeight; ++index1)
          {
            for (int index2 = 0; index2 < pixelWidth; ++index2)
            {
              int x = index2 - num5;
              int y = num6 - index1;
              double num7 = Math.Sqrt((double) (x * x + y * y));
              double num8;
              if (x == 0)
              {
                if (y == 0)
                {
                  pixels1[index1 * pixelWidth + index2] = pixels2[num4 * width2 + num3];
                  continue;
                }
                num8 = y >= 0 ? Math.PI / 2.0 : 3.0 * Math.PI / 2.0;
              }
              else
                num8 = Math.Atan2((double) y, (double) x);
              double num9 = num8 - num1;
              double num10 = num7 * Math.Cos(num9);
              double num11 = num7 * Math.Sin(num9);
              double num12 = num10 + (double) num3;
              double num13 = (double) num4 - num11;
              int num14 = (int) Math.Floor(num12);
              int num15 = (int) Math.Floor(num13);
              int num16 = (int) Math.Ceiling(num12);
              int num17 = (int) Math.Ceiling(num13);
              if (num14 >= 0 && num16 >= 0 && num14 < width1 && num16 < width1 && num15 >= 0 && num17 >= 0 && num15 < height && num17 < height)
              {
                double num18 = num12 - (double) num14;
                double num19 = num13 - (double) num15;
                int num20 = pixels2[num15 * width2 + num14];
                int num21 = pixels2[num15 * width2 + num16];
                int num22 = pixels2[num17 * width2 + num14];
                int num23 = pixels2[num17 * width2 + num16];
                double num24 = (1.0 - num18) * (double) (num20 >> 24 & (int) byte.MaxValue) + num18 * (double) (num21 >> 24 & (int) byte.MaxValue);
                double num25 = (1.0 - num18) * (double) (num20 >> 16 & (int) byte.MaxValue) + num18 * (double) (num21 >> 16 & (int) byte.MaxValue);
                double num26 = (1.0 - num18) * (double) (num20 >> 8 & (int) byte.MaxValue) + num18 * (double) (num21 >> 8 & (int) byte.MaxValue);
                double num27 = (1.0 - num18) * (double) (num20 & (int) byte.MaxValue) + num18 * (double) (num21 & (int) byte.MaxValue);
                double num28 = (1.0 - num18) * (double) (num22 >> 24 & (int) byte.MaxValue) + num18 * (double) (num23 >> 24 & (int) byte.MaxValue);
                double num29 = (1.0 - num18) * (double) (num22 >> 16 & (int) byte.MaxValue) + num18 * (double) (num23 >> 16 & (int) byte.MaxValue);
                double num30 = (1.0 - num18) * (double) (num22 >> 8 & (int) byte.MaxValue) + num18 * (double) (num23 >> 8 & (int) byte.MaxValue);
                double num31 = (1.0 - num18) * (double) (num22 & (int) byte.MaxValue) + num18 * (double) (num23 & (int) byte.MaxValue);
                int num32 = (int) Math.Round((1.0 - num19) * num25 + num19 * num29);
                int num33 = (int) Math.Round((1.0 - num19) * num26 + num19 * num30);
                int num34 = (int) Math.Round((1.0 - num19) * num27 + num19 * num31);
                int num35 = (int) Math.Round((1.0 - num19) * num24 + num19 * num28);
                if (num32 < 0)
                  num32 = 0;
                if (num32 > (int) byte.MaxValue)
                  num32 = (int) byte.MaxValue;
                if (num33 < 0)
                  num33 = 0;
                if (num33 > (int) byte.MaxValue)
                  num33 = (int) byte.MaxValue;
                if (num34 < 0)
                  num34 = 0;
                if (num34 > (int) byte.MaxValue)
                  num34 = (int) byte.MaxValue;
                if (num35 < 0)
                  num35 = 0;
                if (num35 > (int) byte.MaxValue)
                  num35 = (int) byte.MaxValue;
                int num36 = num35 + 1;
                pixels1[index1 * pixelWidth + index2] = num35 << 24 | (int) (byte) (num32 * num36 >> 8) << 16 | (int) (byte) (num33 * num36 >> 8) << 8 | (int) (byte) (num34 * num36 >> 8);
              }
            }
          }
          return bmp1;
        }
      }
    }

    /// <summary>
    /// Flips (reflects the image) eiter vertical or horizontal.
    /// </summary>
    /// <param name="bmp">The WriteableBitmap.</param>
    /// <param name="flipMode">The flip mode.</param>
    /// <returns>A new WriteableBitmap that is a flipped version of the input.</returns>
    public static WriteableBitmap Flip(
      this WriteableBitmap bmp,
      WriteableBitmapExtensions.FlipMode flipMode)
    {
      using (BitmapContext bitmapContext1 = bmp.GetBitmapContext())
      {
        int width = bitmapContext1.Width;
        int height = bitmapContext1.Height;
        int[] pixels1 = bitmapContext1.Pixels;
        int index1 = 0;
        WriteableBitmap bmp1 = (WriteableBitmap) null;
        switch (flipMode)
        {
          case WriteableBitmapExtensions.FlipMode.Vertical:
            bmp1 = BitmapFactory.New(width, height);
            using (BitmapContext bitmapContext2 = bmp1.GetBitmapContext())
            {
              int[] pixels2 = bitmapContext2.Pixels;
              for (int index2 = 0; index2 < height; ++index2)
              {
                for (int index3 = width - 1; index3 >= 0; --index3)
                {
                  int index4 = index2 * width + index3;
                  pixels2[index1] = pixels1[index4];
                  ++index1;
                }
              }
              break;
            }
          case WriteableBitmapExtensions.FlipMode.Horizontal:
            bmp1 = BitmapFactory.New(width, height);
            using (BitmapContext bitmapContext3 = bmp1.GetBitmapContext())
            {
              int[] pixels3 = bitmapContext3.Pixels;
              for (int index5 = height - 1; index5 >= 0; --index5)
              {
                for (int index6 = 0; index6 < width; ++index6)
                {
                  int index7 = index5 * width + index6;
                  pixels3[index1] = pixels1[index7];
                  ++index1;
                }
              }
              break;
            }
        }
        return bmp1;
      }
    }

    private static int ConvertColor(Color color)
    {
      int num = (int) color.A + 1;
      return (int) color.A << 24 | (int) (byte) ((int) color.R * num >> 8) << 16 | (int) (byte) ((int) color.G * num >> 8) << 8 | (int) (byte) ((int) color.B * num >> 8);
    }

    /// <summary>Fills the whole WriteableBitmap with a color.</summary>
    /// <param name="bmp">The WriteableBitmap.</param>
    /// <param name="color">The color used for filling.</param>
    public static void Clear(this WriteableBitmap bmp, Color color)
    {
      int num1 = WriteableBitmapExtensions.ConvertColor(color);
      using (BitmapContext bitmapContext = bmp.GetBitmapContext())
      {
        int[] pixels = bitmapContext.Pixels;
        int width = bitmapContext.Width;
        int height = bitmapContext.Height;
        int num2 = width * 4;
        for (int index = 0; index < width; ++index)
          pixels[index] = num1;
        int num3 = 1;
        int num4 = 1;
        while (num4 < height)
        {
          BitmapContext.BlockCopy(bitmapContext, 0, bitmapContext, num4 * num2, num3 * num2);
          num4 += num3;
          num3 = Math.Min(2 * num3, height - num4);
        }
      }
    }

    /// <summary>
    /// Fills the whole WriteableBitmap with an empty color (0).
    /// </summary>
    /// <param name="bmp">The WriteableBitmap.</param>
    public static void Clear(this WriteableBitmap bmp)
    {
      using (BitmapContext bitmapContext = bmp.GetBitmapContext())
        bitmapContext.Clear();
    }

    /// <summary>Clones the specified WriteableBitmap.</summary>
    /// <param name="bmp">The WriteableBitmap.</param>
    /// <returns>A copy of the WriteableBitmap.</returns>
    public static WriteableBitmap Clone(this WriteableBitmap bmp)
    {
      using (BitmapContext bitmapContext1 = bmp.GetBitmapContext(ReadWriteMode.ReadOnly))
      {
        WriteableBitmap bmp1 = BitmapFactory.New(bitmapContext1.Width, bitmapContext1.Height);
        using (BitmapContext bitmapContext2 = bmp1.GetBitmapContext())
          BitmapContext.BlockCopy(bitmapContext1, 0, bitmapContext2, 0, bitmapContext1.Length * 4);
        return bmp1;
      }
    }

    /// <summary>
    /// Applies the given function to all the pixels of the bitmap in
    /// order to set their color.
    /// </summary>
    /// <param name="bmp">The WriteableBitmap.</param>
    /// <param name="func">The function to apply. With parameters x, y and a color as a result</param>
    public static void ForEach(this WriteableBitmap bmp, Func<int, int, Color> func)
    {
      using (BitmapContext bitmapContext = bmp.GetBitmapContext())
      {
        int[] pixels = bitmapContext.Pixels;
        int width = bitmapContext.Width;
        int height = bitmapContext.Height;
        int num = 0;
        for (int index1 = 0; index1 < height; ++index1)
        {
          for (int index2 = 0; index2 < width; ++index2)
          {
            Color color = func(index2, index1);
            pixels[num++] = WriteableBitmapExtensions.ConvertColor(color);
          }
        }
      }
    }

    /// <summary>
    /// Applies the given function to all the pixels of the bitmap in
    /// order to set their color.
    /// </summary>
    /// <param name="bmp">The WriteableBitmap.</param>
    /// <param name="func">The function to apply. With parameters x, y, source color and a color as a result</param>
    public static void ForEach(this WriteableBitmap bmp, Func<int, int, Color, Color> func)
    {
      using (BitmapContext bitmapContext = bmp.GetBitmapContext())
      {
        int[] pixels = bitmapContext.Pixels;
        int width = bitmapContext.Width;
        int height = bitmapContext.Height;
        int index1 = 0;
        for (int index2 = 0; index2 < height; ++index2)
        {
          for (int index3 = 0; index3 < width; ++index3)
          {
            int num1 = pixels[index1];
            byte num2 = (byte) (num1 >> 24);
            int num3 = (int) num2;
            if (num3 == 0)
              num3 = 1;
            int num4 = 65280 / num3;
            Color color1 = Color.FromArgb(num2, (byte) ((num1 >> 16 & (int) byte.MaxValue) * num4 >> 8), (byte) ((num1 >> 8 & (int) byte.MaxValue) * num4 >> 8), (byte) ((num1 & (int) byte.MaxValue) * num4 >> 8));
            Color color2 = func(index3, index2, color1);
            pixels[index1++] = WriteableBitmapExtensions.ConvertColor(color2);
          }
        }
      }
    }

    /// <summary>
    /// Gets the color of the pixel at the x, y coordinate as integer.
    /// For best performance this method should not be used in iterative real-time scenarios. Implement the code directly inside a loop.
    /// </summary>
    /// <param name="bmp">The WriteableBitmap.</param>
    /// <param name="x">The x coordinate of the pixel.</param>
    /// <param name="y">The y coordinate of the pixel.</param>
    /// <returns>The color of the pixel at x, y.</returns>
    public static int GetPixeli(this WriteableBitmap bmp, int x, int y)
    {
      using (BitmapContext bitmapContext = bmp.GetBitmapContext())
        return bitmapContext.Pixels[y * bitmapContext.Width + x];
    }

    /// <summary>
    /// Gets the color of the pixel at the x, y coordinate as a Color struct.
    /// For best performance this method should not be used in iterative real-time scenarios. Implement the code directly inside a loop.
    /// </summary>
    /// <param name="bmp">The WriteableBitmap.</param>
    /// <param name="x">The x coordinate of the pixel.</param>
    /// <param name="y">The y coordinate of the pixel.</param>
    /// <returns>The color of the pixel at x, y as a Color struct.</returns>
    public static Color GetPixel(this WriteableBitmap bmp, int x, int y)
    {
      using (BitmapContext bitmapContext = bmp.GetBitmapContext())
      {
        int pixel = bitmapContext.Pixels[y * bitmapContext.Width + x];
        byte num1 = (byte) (pixel >> 24);
        int num2 = (int) num1;
        if (num2 == 0)
          num2 = 1;
        int num3 = 65280 / num2;
        return Color.FromArgb(num1, (byte) ((pixel >> 16 & (int) byte.MaxValue) * num3 >> 8), (byte) ((pixel >> 8 & (int) byte.MaxValue) * num3 >> 8), (byte) ((pixel & (int) byte.MaxValue) * num3 >> 8));
      }
    }

    /// <summary>
    /// Gets the brightness / luminance of the pixel at the x, y coordinate as byte.
    /// </summary>
    /// <param name="bmp">The WriteableBitmap.</param>
    /// <param name="x">The x coordinate of the pixel.</param>
    /// <param name="y">The y coordinate of the pixel.</param>
    /// <returns>The brightness of the pixel at x, y.</returns>
    public static byte GetBrightness(this WriteableBitmap bmp, int x, int y)
    {
      using (BitmapContext bitmapContext = bmp.GetBitmapContext(ReadWriteMode.ReadOnly))
      {
        int pixel = bitmapContext.Pixels[y * bitmapContext.Width + x];
        return (byte) ((int) (byte) (pixel >> 16) * 6966 + (int) (byte) (pixel >> 8) * 23436 + (int) (byte) pixel * 2366 >> 15);
      }
    }

    /// <summary>
    /// Sets the color of the pixel using a precalculated index (faster).
    /// For best performance this method should not be used in iterative real-time scenarios. Implement the code directly inside a loop.
    /// </summary>
    /// <param name="bmp">The WriteableBitmap.</param>
    /// <param name="index">The coordinate index.</param>
    /// <param name="r">The red value of the color.</param>
    /// <param name="g">The green value of the color.</param>
    /// <param name="b">The blue value of the color.</param>
    public static void SetPixeli(this WriteableBitmap bmp, int index, byte r, byte g, byte b)
    {
      using (BitmapContext bitmapContext = bmp.GetBitmapContext())
        bitmapContext.Pixels[index] = -16777216 | (int) r << 16 | (int) g << 8 | (int) b;
    }

    /// <summary>
    /// Sets the color of the pixel.
    /// For best performance this method should not be used in iterative real-time scenarios. Implement the code directly inside a loop.
    /// </summary>
    /// <param name="bmp">The WriteableBitmap.</param>
    /// <param name="x">The x coordinate (row).</param>
    /// <param name="y">The y coordinate (column).</param>
    /// <param name="r">The red value of the color.</param>
    /// <param name="g">The green value of the color.</param>
    /// <param name="b">The blue value of the color.</param>
    public static void SetPixel(this WriteableBitmap bmp, int x, int y, byte r, byte g, byte b)
    {
      using (BitmapContext bitmapContext = bmp.GetBitmapContext())
        bitmapContext.Pixels[y * bitmapContext.Width + x] = -16777216 | (int) r << 16 | (int) g << 8 | (int) b;
    }

    /// <summary>
    /// Sets the color of the pixel including the alpha value and using a precalculated index (faster).
    /// For best performance this method should not be used in iterative real-time scenarios. Implement the code directly inside a loop.
    /// </summary>
    /// <param name="bmp">The WriteableBitmap.</param>
    /// <param name="index">The coordinate index.</param>
    /// <param name="a">The alpha value of the color.</param>
    /// <param name="r">The red value of the color.</param>
    /// <param name="g">The green value of the color.</param>
    /// <param name="b">The blue value of the color.</param>
    public static void SetPixeli(
      this WriteableBitmap bmp,
      int index,
      byte a,
      byte r,
      byte g,
      byte b)
    {
      using (BitmapContext bitmapContext = bmp.GetBitmapContext())
        bitmapContext.Pixels[index] = (int) a << 24 | (int) r << 16 | (int) g << 8 | (int) b;
    }

    /// <summary>
    /// Sets the color of the pixel including the alpha value.
    /// For best performance this method should not be used in iterative real-time scenarios. Implement the code directly inside a loop.
    /// </summary>
    /// <param name="bmp">The WriteableBitmap.</param>
    /// <param name="x">The x coordinate (row).</param>
    /// <param name="y">The y coordinate (column).</param>
    /// <param name="a">The alpha value of the color.</param>
    /// <param name="r">The red value of the color.</param>
    /// <param name="g">The green value of the color.</param>
    /// <param name="b">The blue value of the color.</param>
    public static void SetPixel(
      this WriteableBitmap bmp,
      int x,
      int y,
      byte a,
      byte r,
      byte g,
      byte b)
    {
      using (BitmapContext bitmapContext = bmp.GetBitmapContext())
        bitmapContext.Pixels[y * bitmapContext.Width + x] = (int) a << 24 | (int) r << 16 | (int) g << 8 | (int) b;
    }

    /// <summary>
    /// Sets the color of the pixel using a precalculated index (faster).
    /// For best performance this method should not be used in iterative real-time scenarios. Implement the code directly inside a loop.
    /// </summary>
    /// <param name="bmp">The WriteableBitmap.</param>
    /// <param name="index">The coordinate index.</param>
    /// <param name="color">The color.</param>
    public static void SetPixeli(this WriteableBitmap bmp, int index, Color color)
    {
      using (BitmapContext bitmapContext = bmp.GetBitmapContext())
        bitmapContext.Pixels[index] = WriteableBitmapExtensions.ConvertColor(color);
    }

    /// <summary>
    /// Sets the color of the pixel.
    /// For best performance this method should not be used in iterative real-time scenarios. Implement the code directly inside a loop.
    /// </summary>
    /// <param name="bmp">The WriteableBitmap.</param>
    /// <param name="x">The x coordinate (row).</param>
    /// <param name="y">The y coordinate (column).</param>
    /// <param name="color">The color.</param>
    public static void SetPixel(this WriteableBitmap bmp, int x, int y, Color color)
    {
      using (BitmapContext bitmapContext = bmp.GetBitmapContext())
        bitmapContext.Pixels[y * bitmapContext.Width + x] = WriteableBitmapExtensions.ConvertColor(color);
    }

    /// <summary>
    /// Sets the color of the pixel using an extra alpha value and a precalculated index (faster).
    /// For best performance this method should not be used in iterative real-time scenarios. Implement the code directly inside a loop.
    /// </summary>
    /// <param name="bmp">The WriteableBitmap.</param>
    /// <param name="index">The coordinate index.</param>
    /// <param name="a">The alpha value of the color.</param>
    /// <param name="color">The color.</param>
    public static void SetPixeli(this WriteableBitmap bmp, int index, byte a, Color color)
    {
      using (BitmapContext bitmapContext = bmp.GetBitmapContext())
      {
        int num = (int) a + 1;
        bitmapContext.Pixels[index] = (int) a << 24 | (int) (byte) ((int) color.R * num >> 8) << 16 | (int) (byte) ((int) color.G * num >> 8) << 8 | (int) (byte) ((int) color.B * num >> 8);
      }
    }

    /// <summary>
    /// Sets the color of the pixel using an extra alpha value.
    /// For best performance this method should not be used in iterative real-time scenarios. Implement the code directly inside a loop.
    /// </summary>
    /// <param name="bmp">The WriteableBitmap.</param>
    /// <param name="x">The x coordinate (row).</param>
    /// <param name="y">The y coordinate (column).</param>
    /// <param name="a">The alpha value of the color.</param>
    /// <param name="color">The color.</param>
    public static void SetPixel(this WriteableBitmap bmp, int x, int y, byte a, Color color)
    {
      using (BitmapContext bitmapContext = bmp.GetBitmapContext())
      {
        int num = (int) a + 1;
        bitmapContext.Pixels[y * bitmapContext.Width + x] = (int) a << 24 | (int) (byte) ((int) color.R * num >> 8) << 16 | (int) (byte) ((int) color.G * num >> 8) << 8 | (int) (byte) ((int) color.B * num >> 8);
      }
    }

    /// <summary>
    /// Sets the color of the pixel using a precalculated index (faster).
    /// For best performance this method should not be used in iterative real-time scenarios. Implement the code directly inside a loop.
    /// </summary>
    /// <param name="bmp">The WriteableBitmap.</param>
    /// <param name="index">The coordinate index.</param>
    /// <param name="color">The color.</param>
    public static void SetPixeli(this WriteableBitmap bmp, int index, int color)
    {
      using (BitmapContext bitmapContext = bmp.GetBitmapContext())
        bitmapContext.Pixels[index] = color;
    }

    /// <summary>
    /// Sets the color of the pixel.
    /// For best performance this method should not be used in iterative real-time scenarios. Implement the code directly inside a loop.
    /// </summary>
    /// <param name="bmp">The WriteableBitmap.</param>
    /// <param name="x">The x coordinate (row).</param>
    /// <param name="y">The y coordinate (column).</param>
    /// <param name="color">The color.</param>
    public static void SetPixel(this WriteableBitmap bmp, int x, int y, int color)
    {
      using (BitmapContext bitmapContext = bmp.GetBitmapContext())
        bitmapContext.Pixels[y * bitmapContext.Width + x] = color;
    }

    /// <summary>
    /// Copies (blits) the pixels from the WriteableBitmap source to the destination WriteableBitmap (this).
    /// </summary>
    /// <param name="bmp">The destination WriteableBitmap.</param>
    /// <param name="destRect">The rectangle that defines the destination region.</param>
    /// <param name="source">The source WriteableBitmap.</param>
    /// <param name="sourceRect">The rectangle that will be copied from the source to the destination.</param>
    /// <param name="BlendMode">The blending mode <see cref="T:System.Windows.Media.Imaging.WriteableBitmapExtensions.BlendMode" />.</param>
    public static void Blit(
      this WriteableBitmap bmp,
      Rect destRect,
      WriteableBitmap source,
      Rect sourceRect,
      WriteableBitmapExtensions.BlendMode BlendMode)
    {
      bmp.Blit(destRect, source, sourceRect, Colors.White, BlendMode);
    }

    /// <summary>
    /// Copies (blits) the pixels from the WriteableBitmap source to the destination WriteableBitmap (this).
    /// </summary>
    /// <param name="bmp">The destination WriteableBitmap.</param>
    /// <param name="destRect">The rectangle that defines the destination region.</param>
    /// <param name="source">The source WriteableBitmap.</param>
    /// <param name="sourceRect">The rectangle that will be copied from the source to the destination.</param>
    public static void Blit(
      this WriteableBitmap bmp,
      Rect destRect,
      WriteableBitmap source,
      Rect sourceRect)
    {
      bmp.Blit(destRect, source, sourceRect, Colors.White, WriteableBitmapExtensions.BlendMode.Alpha);
    }

    /// <summary>
    /// Copies (blits) the pixels from the WriteableBitmap source to the destination WriteableBitmap (this).
    /// </summary>
    /// <param name="bmp">The destination WriteableBitmap.</param>
    /// <param name="destPosition">The destination position in the destination bitmap.</param>
    /// <param name="source">The source WriteableBitmap.</param>
    /// <param name="sourceRect">The rectangle that will be copied from the source to the destination.</param>
    /// <param name="color">If not Colors.White, will tint the source image. A partially transparent color and the image will be drawn partially transparent.</param>
    /// <param name="BlendMode">The blending mode <see cref="T:System.Windows.Media.Imaging.WriteableBitmapExtensions.BlendMode" />.</param>
    public static void Blit(
      this WriteableBitmap bmp,
      Point destPosition,
      WriteableBitmap source,
      Rect sourceRect,
      Color color,
      WriteableBitmapExtensions.BlendMode BlendMode)
    {
      Rect destRect = new Rect(destPosition, new Size(sourceRect.Width, sourceRect.Height));
      bmp.Blit(destRect, source, sourceRect, color, BlendMode);
    }

    /// <summary>
    /// Copies (blits) the pixels from the WriteableBitmap source to the destination WriteableBitmap (this).
    /// </summary>
    /// <param name="bmp">The destination WriteableBitmap.</param>
    /// <param name="destRect">The rectangle that defines the destination region.</param>
    /// <param name="source">The source WriteableBitmap.</param>
    /// <param name="sourceRect">The rectangle that will be copied from the source to the destination.</param>
    /// <param name="color">If not Colors.White, will tint the source image. A partially transparent color and the image will be drawn partially transparent. If the BlendMode is ColorKeying, this color will be used as color key to mask all pixels with this value out.</param>
    /// <param name="BlendMode">The blending mode <see cref="T:System.Windows.Media.Imaging.WriteableBitmapExtensions.BlendMode" />.</param>
    public static void Blit(
      this WriteableBitmap bmp,
      Rect destRect,
      WriteableBitmap source,
      Rect sourceRect,
      Color color,
      WriteableBitmapExtensions.BlendMode BlendMode)
    {
      if (color.A == (byte) 0)
        return;
      int width1 = (int) destRect.Width;
      int height1 = (int) destRect.Height;
      using (BitmapContext bitmapContext1 = source.GetBitmapContext(ReadWriteMode.ReadOnly))
      {
        using (BitmapContext bitmapContext2 = bmp.GetBitmapContext())
        {
          int width2 = bitmapContext1.Width;
          int width3 = bitmapContext2.Width;
          int height2 = bitmapContext2.Height;
          Rect rect = new Rect(0.0, 0.0, (double) width3, (double) height2);
          rect.Intersect(destRect);
          if (rect.IsEmpty)
            return;
          int[] pixels1 = bitmapContext1.Pixels;
          int[] pixels2 = bitmapContext2.Pixels;
          int length1 = bitmapContext1.Length;
          int length2 = bitmapContext2.Length;
          int x1 = (int) destRect.X;
          int y1 = (int) destRect.Y;
          int num1 = 0;
          int num2 = 0;
          int num3 = 0;
          int num4 = 0;
          int a = (int) color.A;
          int r = (int) color.R;
          int g = (int) color.G;
          int b = (int) color.B;
          bool flag = Color.op_Inequality(color, Colors.White);
          int width4 = (int) sourceRect.Width;
          double num5 = sourceRect.Width / destRect.Width;
          double num6 = sourceRect.Height / destRect.Height;
          int x2 = (int) sourceRect.X;
          int y2 = (int) sourceRect.Y;
          int num7 = -1;
          int num8 = -1;
          double num9 = (double) y2;
          int num10 = y1;
          for (int index1 = 0; index1 < height1; ++index1)
          {
            if (num10 >= 0 && num10 < height2)
            {
              double num11 = (double) x2;
              int index2 = x1 + num10 * width3;
              int num12 = x1;
              int num13 = pixels1[0];
              if (BlendMode == WriteableBitmapExtensions.BlendMode.None && !flag)
              {
                int num14 = (int) num11 + (int) num9 * width2;
                int num15 = num12 < 0 ? -num12 : 0;
                int num16 = num12 + num15;
                int num17 = width2 - num15;
                int num18 = num16 + num17 < width3 ? num17 : width3 - num16;
                if (num18 > width4)
                  num18 = width4;
                if (num18 > width1)
                  num18 = width1;
                BitmapContext.BlockCopy(bitmapContext1, (num14 + num15) * 4, bitmapContext2, (index2 + num15) * 4, num18 * 4);
              }
              else
              {
                for (int index3 = 0; index3 < width1; ++index3)
                {
                  if (num12 >= 0 && num12 < width3)
                  {
                    if ((int) num11 != num7 || (int) num9 != num8)
                    {
                      int index4 = (int) num11 + (int) num9 * width2;
                      if (index4 >= 0 && index4 < length1)
                      {
                        num13 = pixels1[index4];
                        num4 = num13 >> 24 & (int) byte.MaxValue;
                        num1 = num13 >> 16 & (int) byte.MaxValue;
                        num2 = num13 >> 8 & (int) byte.MaxValue;
                        num3 = num13 & (int) byte.MaxValue;
                        if (flag && num4 != 0)
                        {
                          num4 = num4 * a * 32897 >> 23;
                          num1 = (num1 * r * 32897 >> 23) * a * 32897 >> 23;
                          num2 = (num2 * g * 32897 >> 23) * a * 32897 >> 23;
                          num3 = (num3 * b * 32897 >> 23) * a * 32897 >> 23;
                          num13 = num4 << 24 | num1 << 16 | num2 << 8 | num3;
                        }
                      }
                      else
                        num4 = 0;
                    }
                    switch (BlendMode)
                    {
                      case WriteableBitmapExtensions.BlendMode.Mask:
                        int num19 = pixels2[index2];
                        int num20 = num19 >> 24 & (int) byte.MaxValue;
                        int num21 = num19 >> 16 & (int) byte.MaxValue;
                        int num22 = num19 >> 8 & (int) byte.MaxValue;
                        int num23 = num19 & (int) byte.MaxValue;
                        int num24 = num20 * num4 * 32897 >> 23 << 24 | num21 * num4 * 32897 >> 23 << 16 | num22 * num4 * 32897 >> 23 << 8 | num23 * num4 * 32897 >> 23;
                        pixels2[index2] = num24;
                        break;
                      case WriteableBitmapExtensions.BlendMode.ColorKeying:
                        num1 = num13 >> 16 & (int) byte.MaxValue;
                        num2 = num13 >> 8 & (int) byte.MaxValue;
                        num3 = num13 & (int) byte.MaxValue;
                        if (num1 != (int) color.R || num2 != (int) color.G || num3 != (int) color.B)
                        {
                          pixels2[index2] = num13;
                          break;
                        }
                        break;
                      case WriteableBitmapExtensions.BlendMode.None:
                        pixels2[index2] = num13;
                        break;
                      default:
                        if (num4 > 0)
                        {
                          int num25 = pixels2[index2];
                          int num26 = num25 >> 24 & (int) byte.MaxValue;
                          if ((num4 == (int) byte.MaxValue || num26 == 0) && BlendMode != WriteableBitmapExtensions.BlendMode.Additive && BlendMode != WriteableBitmapExtensions.BlendMode.Subtractive && BlendMode != WriteableBitmapExtensions.BlendMode.Multiply)
                          {
                            pixels2[index2] = num13;
                            break;
                          }
                          int num27 = num25 >> 16 & (int) byte.MaxValue;
                          int num28 = num25 >> 8 & (int) byte.MaxValue;
                          int num29 = num25 & (int) byte.MaxValue;
                          switch (BlendMode)
                          {
                            case WriteableBitmapExtensions.BlendMode.Alpha:
                              int num30 = (int) byte.MaxValue - num4;
                              num25 = (num26 & (int) byte.MaxValue) << 24 | ((num1 << 8) + num30 * num27 >> 8 & (int) byte.MaxValue) << 16 | ((num2 << 8) + num30 * num28 >> 8 & (int) byte.MaxValue) << 8 | (num3 << 8) + num30 * num29 >> 8 & (int) byte.MaxValue;
                              break;
                            case WriteableBitmapExtensions.BlendMode.Additive:
                              int num31 = (int) byte.MaxValue <= num4 + num26 ? (int) byte.MaxValue : num4 + num26;
                              num25 = num31 << 24 | (num31 <= num1 + num27 ? num31 : num1 + num27) << 16 | (num31 <= num2 + num28 ? num31 : num2 + num28) << 8 | (num31 <= num3 + num29 ? num31 : num3 + num29);
                              break;
                            case WriteableBitmapExtensions.BlendMode.Subtractive:
                              num25 = num26 << 24 | (num1 >= num27 ? 0 : num1 - num27) << 16 | (num2 >= num28 ? 0 : num2 - num28) << 8 | (num3 >= num29 ? 0 : num3 - num29);
                              break;
                            case WriteableBitmapExtensions.BlendMode.Multiply:
                              int num32 = num4 * num26 + 128;
                              int num33 = num1 * num27 + 128;
                              int num34 = num2 * num28 + 128;
                              int num35 = num3 * num29 + 128;
                              int num36 = (num32 >> 8) + num32 >> 8;
                              int num37 = (num33 >> 8) + num33 >> 8;
                              int num38 = (num34 >> 8) + num34 >> 8;
                              int num39 = (num35 >> 8) + num35 >> 8;
                              num25 = num36 << 24 | (num36 <= num37 ? num36 : num37) << 16 | (num36 <= num38 ? num36 : num38) << 8 | (num36 <= num39 ? num36 : num39);
                              break;
                          }
                          pixels2[index2] = num25;
                          break;
                        }
                        break;
                    }
                  }
                  ++num12;
                  ++index2;
                  num11 += num5;
                }
              }
            }
            num9 += num6;
            ++num10;
          }
        }
      }
    }

    /// <summary>
    /// Copies the Pixels from the WriteableBitmap into a ARGB byte array starting at a specific Pixels index.
    /// </summary>
    /// <param name="bmp">The WriteableBitmap.</param>
    /// <param name="offset">The starting Pixels index.</param>
    /// <param name="count">The number of Pixels to copy, -1 for all</param>
    /// <returns>The color buffer as byte ARGB values.</returns>
    public static byte[] ToByteArray(this WriteableBitmap bmp, int offset, int count)
    {
      using (BitmapContext bitmapContext = bmp.GetBitmapContext())
      {
        if (count == -1)
          count = bitmapContext.Length;
        int count1 = count * 4;
        byte[] dest = new byte[count1];
        BitmapContext.BlockCopy(bitmapContext, offset, (Array) dest, 0, count1);
        return dest;
      }
    }

    /// <summary>
    /// Copies the Pixels from the WriteableBitmap into a ARGB byte array.
    /// </summary>
    /// <param name="bmp">The WriteableBitmap.</param>
    /// <param name="count">The number of pixels to copy.</param>
    /// <returns>The color buffer as byte ARGB values.</returns>
    public static byte[] ToByteArray(this WriteableBitmap bmp, int count) => bmp.ToByteArray(0, count);

    /// <summary>
    /// Copies all the Pixels from the WriteableBitmap into a ARGB byte array.
    /// </summary>
    /// <param name="bmp">The WriteableBitmap.</param>
    /// <returns>The color buffer as byte ARGB values.</returns>
    public static byte[] ToByteArray(this WriteableBitmap bmp) => bmp.ToByteArray(0, -1);

    /// <summary>
    /// Copies color information from an ARGB byte array into this WriteableBitmap starting at a specific buffer index.
    /// </summary>
    /// <param name="bmp">The WriteableBitmap.</param>
    /// <param name="offset">The starting index in the buffer.</param>
    /// <param name="count">The number of bytes to copy from the buffer.</param>
    /// <param name="buffer">The color buffer as byte ARGB values.</param>
    /// <returns>The WriteableBitmap that was passed as parameter.</returns>
    public static WriteableBitmap FromByteArray(
      this WriteableBitmap bmp,
      byte[] buffer,
      int offset,
      int count)
    {
      using (BitmapContext bitmapContext = bmp.GetBitmapContext())
      {
        BitmapContext.BlockCopy((Array) buffer, offset, bitmapContext, 0, count);
        return bmp;
      }
    }

    /// <summary>
    /// Copies color information from an ARGB byte array into this WriteableBitmap.
    /// </summary>
    /// <param name="bmp">The WriteableBitmap.</param>
    /// <param name="count">The number of bytes to copy from the buffer.</param>
    /// <param name="buffer">The color buffer as byte ARGB values.</param>
    /// <returns>The WriteableBitmap that was passed as parameter.</returns>
    public static WriteableBitmap FromByteArray(this WriteableBitmap bmp, byte[] buffer, int count) => bmp.FromByteArray(buffer, 0, count);

    /// <summary>
    /// Copies all the color information from an ARGB byte array into this WriteableBitmap.
    /// </summary>
    /// <param name="bmp">The WriteableBitmap.</param>
    /// <param name="buffer">The color buffer as byte ARGB values.</param>
    /// <returns>The WriteableBitmap that was passed as parameter.</returns>
    public static WriteableBitmap FromByteArray(this WriteableBitmap bmp, byte[] buffer) => bmp.FromByteArray(buffer, 0, buffer.Length);

    /// <summary>
    /// Writes the WriteableBitmap as a TGA image to a stream.
    /// Used with permission from Nokola: http://nokola.com/blog/post/2010/01/21/Quick-and-Dirty-Output-of-WriteableBitmap-as-TGA-Image.aspx
    /// </summary>
    /// <param name="bmp">The WriteableBitmap.</param>
    /// <param name="destination">The destination stream.</param>
    public static void WriteTga(this WriteableBitmap bmp, Stream destination)
    {
      using (BitmapContext bitmapContext = bmp.GetBitmapContext())
      {
        int width = bitmapContext.Width;
        int height = bitmapContext.Height;
        int[] pixels = bitmapContext.Pixels;
        byte[] buffer1 = new byte[bitmapContext.Length * 4];
        int index1 = 0;
        int num1 = width << 2;
        int num2 = width << 3;
        int index2 = (height - 1) * num1;
        for (int index3 = 0; index3 < height; ++index3)
        {
          for (int index4 = 0; index4 < width; ++index4)
          {
            int num3 = pixels[index1];
            byte num4 = (byte) (num3 >> 24);
            int num5 = (int) num4;
            if (num5 == 0)
              num5 = 1;
            int num6 = 65280 / num5;
            buffer1[index2 + 3] = num4;
            buffer1[index2 + 2] = (byte) ((num3 >> 16 & (int) byte.MaxValue) * num6 >> 8);
            buffer1[index2 + 1] = (byte) ((num3 >> 8 & (int) byte.MaxValue) * num6 >> 8);
            buffer1[index2] = (byte) ((num3 & (int) byte.MaxValue) * num6 >> 8);
            ++index1;
            index2 += 4;
          }
          index2 -= num2;
        }
        byte[] numArray = new byte[18];
        numArray[2] = (byte) 2;
        numArray[12] = (byte) (width & (int) byte.MaxValue);
        numArray[13] = (byte) ((width & 65280) >> 8);
        numArray[14] = (byte) (height & (int) byte.MaxValue);
        numArray[15] = (byte) ((height & 65280) >> 8);
        numArray[16] = (byte) 32;
        byte[] buffer2 = numArray;
        using (BinaryWriter binaryWriter = new BinaryWriter(destination))
        {
          binaryWriter.Write(buffer2);
          binaryWriter.Write(buffer1);
        }
      }
    }

    /// <summary>
    /// Loads an image from the applications resource file and returns a new WriteableBitmap. The passed WriteableBitmap is not used.
    /// </summary>
    /// <param name="bmp">The WriteableBitmap.</param>
    /// <param name="relativePath">Only the relative path to the resource file. The assembly name is retrieved automatically.</param>
    /// <returns>A new WriteableBitmap containing the pixel data.</returns>
    public static WriteableBitmap FromResource(this WriteableBitmap bmp, string relativePath)
    {
      string name = new AssemblyName(Assembly.GetCallingAssembly().FullName).Name;
      return bmp.FromContent(name + ";component/" + relativePath);
    }

    /// <summary>
    /// Loads an image from the applications content and returns a new WriteableBitmap. The passed WriteableBitmap is not used.
    /// </summary>
    /// <param name="bmp">The WriteableBitmap.</param>
    /// <param name="relativePath">Only the relative path to the content file.</param>
    /// <returns>A new WriteableBitmap containing the pixel data.</returns>
    public static WriteableBitmap FromContent(this WriteableBitmap bmp, string relativePath)
    {
      using (Stream stream = Application.GetResourceStream(new Uri(relativePath, UriKind.Relative)).Stream)
        return bmp.FromStream(stream);
    }

    /// <summary>
    /// Loads the data from an image stream and returns a new WriteableBitmap. The passed WriteableBitmap is not used.
    /// </summary>
    /// <param name="bmp">The WriteableBitmap.</param>
    /// <param name="stream">The stream with the image data.</param>
    /// <returns>A new WriteableBitmap containing the pixel data.</returns>
    public static WriteableBitmap FromStream(this WriteableBitmap bmp, Stream stream)
    {
      BitmapImage bitmapImage = new BitmapImage();
      ((BitmapSource) bitmapImage).SetSource(stream);
      bitmapImage.CreateOptions = (BitmapCreateOptions) 0;
      bmp = new WriteableBitmap((BitmapSource) bitmapImage);
      bitmapImage.UriSource = (Uri) null;
      return bmp;
    }

    /// <summary>
    /// Draws a colored line by connecting two points using the Bresenham algorithm.
    /// </summary>
    /// <param name="bmp">The WriteableBitmap.</param>
    /// <param name="x1">The x-coordinate of the start point.</param>
    /// <param name="y1">The y-coordinate of the start point.</param>
    /// <param name="x2">The x-coordinate of the end point.</param>
    /// <param name="y2">The y-coordinate of the end point.</param>
    /// <param name="color">The color for the line.</param>
    public static void DrawLineBresenham(
      this WriteableBitmap bmp,
      int x1,
      int y1,
      int x2,
      int y2,
      Color color)
    {
      int color1 = WriteableBitmapExtensions.ConvertColor(color);
      bmp.DrawLineBresenham(x1, y1, x2, y2, color1);
    }

    /// <summary>
    /// Draws a colored line by connecting two points using the Bresenham algorithm.
    /// </summary>
    /// <param name="bmp">The WriteableBitmap.</param>
    /// <param name="x1">The x-coordinate of the start point.</param>
    /// <param name="y1">The y-coordinate of the start point.</param>
    /// <param name="x2">The x-coordinate of the end point.</param>
    /// <param name="y2">The y-coordinate of the end point.</param>
    /// <param name="color">The color for the line.</param>
    public static void DrawLineBresenham(
      this WriteableBitmap bmp,
      int x1,
      int y1,
      int x2,
      int y2,
      int color)
    {
      using (BitmapContext bitmapContext = bmp.GetBitmapContext())
      {
        int width = bitmapContext.Width;
        int height = bitmapContext.Height;
        int[] pixels = bitmapContext.Pixels;
        int num1 = x2 - x1;
        int num2 = y2 - y1;
        int num3 = 0;
        if (num1 < 0)
        {
          num1 = -num1;
          num3 = -1;
        }
        else if (num1 > 0)
          num3 = 1;
        int num4 = 0;
        if (num2 < 0)
        {
          num2 = -num2;
          num4 = -1;
        }
        else if (num2 > 0)
          num4 = 1;
        int num5;
        int num6;
        int num7;
        int num8;
        int num9;
        int num10;
        if (num1 > num2)
        {
          num5 = num3;
          num6 = 0;
          num7 = num3;
          num8 = num4;
          num9 = num2;
          num10 = num1;
        }
        else
        {
          num5 = 0;
          num6 = num4;
          num7 = num3;
          num8 = num4;
          num9 = num1;
          num10 = num2;
        }
        int num11 = x1;
        int num12 = y1;
        int num13 = num10 >> 1;
        if (num12 < height && num12 >= 0 && num11 < width && num11 >= 0)
          pixels[num12 * width + num11] = color;
        for (int index = 0; index < num10; ++index)
        {
          num13 -= num9;
          if (num13 < 0)
          {
            num13 += num10;
            num11 += num7;
            num12 += num8;
          }
          else
          {
            num11 += num5;
            num12 += num6;
          }
          if (num12 < height && num12 >= 0 && num11 < width && num11 >= 0)
            pixels[num12 * width + num11] = color;
        }
      }
    }

    /// <summary>
    /// Draws a colored line by connecting two points using a DDA algorithm (Digital Differential Analyzer).
    /// </summary>
    /// <param name="bmp">The WriteableBitmap.</param>
    /// <param name="x1">The x-coordinate of the start point.</param>
    /// <param name="y1">The y-coordinate of the start point.</param>
    /// <param name="x2">The x-coordinate of the end point.</param>
    /// <param name="y2">The y-coordinate of the end point.</param>
    /// <param name="color">The color for the line.</param>
    public static void DrawLineDDA(
      this WriteableBitmap bmp,
      int x1,
      int y1,
      int x2,
      int y2,
      Color color)
    {
      int color1 = WriteableBitmapExtensions.ConvertColor(color);
      bmp.DrawLineDDA(x1, y1, x2, y2, color1);
    }

    /// <summary>
    /// Draws a colored line by connecting two points using a DDA algorithm (Digital Differential Analyzer).
    /// </summary>
    /// <param name="bmp">The WriteableBitmap.</param>
    /// <param name="x1">The x-coordinate of the start point.</param>
    /// <param name="y1">The y-coordinate of the start point.</param>
    /// <param name="x2">The x-coordinate of the end point.</param>
    /// <param name="y2">The y-coordinate of the end point.</param>
    /// <param name="color">The color for the line.</param>
    public static void DrawLineDDA(
      this WriteableBitmap bmp,
      int x1,
      int y1,
      int x2,
      int y2,
      int color)
    {
      using (BitmapContext bitmapContext = bmp.GetBitmapContext())
      {
        int width = bitmapContext.Width;
        int height = bitmapContext.Height;
        int[] pixels = bitmapContext.Pixels;
        int num1 = x2 - x1;
        int num2 = y2 - y1;
        int num3 = num2 >= 0 ? num2 : -num2;
        int num4 = num1 >= 0 ? num1 : -num1;
        if (num4 > num3)
          num3 = num4;
        if (num3 == 0)
          return;
        float num5 = (float) num1 / (float) num3;
        float num6 = (float) num2 / (float) num3;
        float num7 = (float) x1;
        float num8 = (float) y1;
        for (int index = 0; index < num3; ++index)
        {
          if ((double) num8 < (double) height && (double) num8 >= 0.0 && (double) num7 < (double) width && (double) num7 >= 0.0)
            pixels[(int) num8 * width + (int) num7] = color;
          num7 += num5;
          num8 += num6;
        }
      }
    }

    /// <summary>
    /// Draws a colored line by connecting two points using an optimized DDA.
    /// </summary>
    /// <param name="bmp">The WriteableBitmap.</param>
    /// <param name="x1">The x-coordinate of the start point.</param>
    /// <param name="y1">The y-coordinate of the start point.</param>
    /// <param name="x2">The x-coordinate of the end point.</param>
    /// <param name="y2">The y-coordinate of the end point.</param>
    /// <param name="color">The color for the line.</param>
    public static void DrawLine(
      this WriteableBitmap bmp,
      int x1,
      int y1,
      int x2,
      int y2,
      Color color)
    {
      int color1 = WriteableBitmapExtensions.ConvertColor(color);
      bmp.DrawLine(x1, y1, x2, y2, color1);
    }

    /// <summary>
    /// Draws a colored line by connecting two points using an optimized DDA.
    /// </summary>
    /// <param name="bmp">The WriteableBitmap.</param>
    /// <param name="x1">The x-coordinate of the start point.</param>
    /// <param name="y1">The y-coordinate of the start point.</param>
    /// <param name="x2">The x-coordinate of the end point.</param>
    /// <param name="y2">The y-coordinate of the end point.</param>
    /// <param name="color">The color for the line.</param>
    public static void DrawLine(
      this WriteableBitmap bmp,
      int x1,
      int y1,
      int x2,
      int y2,
      int color)
    {
      using (BitmapContext bitmapContext = bmp.GetBitmapContext())
        WriteableBitmapExtensions.DrawLine(bitmapContext, bitmapContext.Width, bitmapContext.Height, x1, y1, x2, y2, color);
    }

    /// <summary>
    /// Draws a colored line by connecting two points using an optimized DDA.
    /// Uses the pixels array and the width directly for best performance.
    /// </summary>
    /// <param name="context">The context containing the pixels as int RGBA value.</param>
    /// <param name="pixelWidth">The width of one scanline in the pixels array.</param>
    /// <param name="pixelHeight">The height of the bitmap.</param>
    /// <param name="x1">The x-coordinate of the start point.</param>
    /// <param name="y1">The y-coordinate of the start point.</param>
    /// <param name="x2">The x-coordinate of the end point.</param>
    /// <param name="y2">The y-coordinate of the end point.</param>
    /// <param name="color">The color for the line.</param>
    public static void DrawLine(
      BitmapContext context,
      int pixelWidth,
      int pixelHeight,
      int x1,
      int y1,
      int x2,
      int y2,
      int color)
    {
      int[] pixels = context.Pixels;
      int num1 = x2 - x1;
      int num2 = y2 - y1;
      int num3 = num2 < 0 ? -num2 : num2;
      if ((num1 < 0 ? -num1 : num1) > num3)
      {
        if (num1 < 0)
        {
          int num4 = x1;
          x1 = x2;
          x2 = num4;
          int num5 = y1;
          y1 = y2;
          y2 = num5;
        }
        int num6 = (num2 << 8) / num1;
        int num7 = y1 << 8;
        int num8 = y2 << 8;
        int num9 = pixelHeight << 8;
        if (y1 < y2)
        {
          if (y1 >= pixelHeight || y2 < 0)
            return;
          if (num7 < 0)
          {
            if (num6 == 0)
              return;
            int num10 = num7;
            num7 = num6 - 1 + (num7 + 1) % num6;
            x1 += (num7 - num10) / num6;
          }
          if (num8 >= num9 && num6 != 0)
          {
            int num11 = num9 - 1 - (num9 - 1 - num7) % num6;
            x2 = x1 + (num11 - num7) / num6;
          }
        }
        else
        {
          if (y2 >= pixelHeight || y1 < 0)
            return;
          if (num7 >= num9)
          {
            if (num6 == 0)
              return;
            int num12 = num7;
            num7 = num9 - 1 + (num6 - (num9 - 1 - num12) % num6);
            x1 += (num7 - num12) / num6;
          }
          if (num8 < 0 && num6 != 0)
          {
            int num13 = num7 % num6;
            x2 = x1 + (num13 - num7) / num6;
          }
        }
        if (x1 < 0)
        {
          num7 -= num6 * x1;
          x1 = 0;
        }
        if (x2 >= pixelWidth)
          x2 = pixelWidth - 1;
        int num14 = num7;
        int num15 = num14 >> 8;
        int num16 = num15;
        int index1 = x1 + num15 * pixelWidth;
        int num17 = num6 < 0 ? 1 - pixelWidth : 1 + pixelWidth;
        for (int index2 = x1; index2 <= x2; ++index2)
        {
          pixels[index1] = color;
          num14 += num6;
          int num18 = num14 >> 8;
          if (num18 != num16)
          {
            num16 = num18;
            index1 += num17;
          }
          else
            ++index1;
        }
      }
      else
      {
        if (num3 == 0)
          return;
        if (num2 < 0)
        {
          int num19 = x1;
          x1 = x2;
          x2 = num19;
          int num20 = y1;
          y1 = y2;
          y2 = num20;
        }
        int num21 = x1 << 8;
        int num22 = x2 << 8;
        int num23 = pixelWidth << 8;
        int num24 = (num1 << 8) / num2;
        if (x1 < x2)
        {
          if (x1 >= pixelWidth || x2 < 0)
            return;
          if (num21 < 0)
          {
            if (num24 == 0)
              return;
            int num25 = num21;
            num21 = num24 - 1 + (num21 + 1) % num24;
            y1 += (num21 - num25) / num24;
          }
          if (num22 >= num23 && num24 != 0)
          {
            int num26 = num23 - 1 - (num23 - 1 - num21) % num24;
            y2 = y1 + (num26 - num21) / num24;
          }
        }
        else
        {
          if (x2 >= pixelWidth || x1 < 0)
            return;
          if (num21 >= num23)
          {
            if (num24 == 0)
              return;
            int num27 = num21;
            num21 = num23 - 1 + (num24 - (num23 - 1 - num27) % num24);
            y1 += (num21 - num27) / num24;
          }
          if (num22 < 0 && num24 != 0)
          {
            int num28 = num21 % num24;
            y2 = y1 + (num28 - num21) / num24;
          }
        }
        if (y1 < 0)
        {
          num21 -= num24 * y1;
          y1 = 0;
        }
        if (y2 >= pixelHeight)
          y2 = pixelHeight - 1;
        int num29 = num21 + (y1 * pixelWidth << 8);
        int num30 = (pixelWidth << 8) + num24;
        for (int index = y1; index <= y2; ++index)
        {
          pixels[num29 >> 8] = color;
          num29 += num30;
        }
      }
    }

    /// <summary>
    /// Draws an anti-aliased line, using an optimized version of Gupta-Sproull algorithm
    /// From http://nokola.com/blog/post/2010/10/14/Anti-aliased-Lines-And-Optimizing-Code-for-Windows-Phone-7e28093First-Look.aspx
    /// <param name="bmp">The WriteableBitmap.</param>
    /// <param name="x1">The x-coordinate of the start point.</param>
    /// <param name="y1">The y-coordinate of the start point.</param>
    /// <param name="x2">The x-coordinate of the end point.</param>
    /// <param name="y2">The y-coordinate of the end point.</param>
    /// <param name="color">The color for the line.</param>
    /// </summary>
    public static void DrawLineAa(
      this WriteableBitmap bmp,
      int x1,
      int y1,
      int x2,
      int y2,
      Color color)
    {
      int color1 = WriteableBitmapExtensions.ConvertColor(color);
      bmp.DrawLineAa(x1, y1, x2, y2, color1);
    }

    /// <summary>
    /// Draws an anti-aliased line, using an optimized version of Gupta-Sproull algorithm
    /// From http://nokola.com/blog/post/2010/10/14/Anti-aliased-Lines-And-Optimizing-Code-for-Windows-Phone-7e28093First-Look.aspx
    /// <param name="bmp">The WriteableBitmap.</param>
    /// <param name="x1">The x-coordinate of the start point.</param>
    /// <param name="y1">The y-coordinate of the start point.</param>
    /// <param name="x2">The x-coordinate of the end point.</param>
    /// <param name="y2">The y-coordinate of the end point.</param>
    /// <param name="color">The color for the line.</param>
    /// </summary>
    public static void DrawLineAa(
      this WriteableBitmap bmp,
      int x1,
      int y1,
      int x2,
      int y2,
      int color)
    {
      using (BitmapContext bitmapContext = bmp.GetBitmapContext())
        WriteableBitmapExtensions.DrawLineAa(bitmapContext, bitmapContext.Width, bitmapContext.Height, x1, y1, x2, y2, color);
    }

    /// <summary>
    /// Draws an anti-aliased line, using an optimized version of Gupta-Sproull algorithm
    /// From http://nokola.com/blog/post/2010/10/14/Anti-aliased-Lines-And-Optimizing-Code-for-Windows-Phone-7e28093First-Look.aspx
    /// <param name="context">The context containing the pixels as int RGBA value.</param>
    /// <param name="pixelWidth">The width of one scanline in the pixels array.</param>
    /// <param name="pixelHeight">The height of the bitmap.</param>
    /// <param name="x1">The x-coordinate of the start point.</param>
    /// <param name="y1">The y-coordinate of the start point.</param>
    /// <param name="x2">The x-coordinate of the end point.</param>
    /// <param name="y2">The y-coordinate of the end point.</param>
    /// <param name="color">The color for the line.</param>
    /// </summary>
    public static void DrawLineAa(
      BitmapContext context,
      int pixelWidth,
      int pixelHeight,
      int x1,
      int y1,
      int x2,
      int y2,
      int color)
    {
      if (x1 == x2 && y1 == y2)
        return;
      if (x1 < 1)
        x1 = 1;
      if (x1 > pixelWidth - 2)
        x1 = pixelWidth - 2;
      if (y1 < 1)
        y1 = 1;
      if (y1 > pixelHeight - 2)
        y1 = pixelHeight - 2;
      if (x2 < 1)
        x2 = 1;
      if (x2 > pixelWidth - 2)
        x2 = pixelWidth - 2;
      if (y2 < 1)
        y2 = 1;
      if (y2 > pixelHeight - 2)
        y2 = pixelHeight - 2;
      int index = y1 * pixelWidth + x1;
      int num1 = x2 - x1;
      int num2 = y2 - y1;
      int num3 = color >> 24 & (int) byte.MaxValue;
      uint srb = (uint) (color & 16711935);
      uint sg = (uint) (color >> 8 & (int) byte.MaxValue);
      int num4 = num1;
      int num5 = num2;
      if (num1 < 0)
        num4 = -num1;
      if (num2 < 0)
        num5 = -num2;
      int num6;
      int num7;
      int num8;
      int num9;
      int num10;
      int num11;
      if (num4 > num5)
      {
        num6 = num4;
        num7 = num5;
        num8 = x2;
        num9 = y2;
        num10 = 1;
        num11 = pixelWidth;
        if (num1 < 0)
          num10 = -num10;
        if (num2 < 0)
          num11 = -num11;
      }
      else
      {
        num6 = num5;
        num7 = num4;
        num8 = y2;
        num9 = x2;
        num10 = pixelWidth;
        num11 = 1;
        if (num2 < 0)
          num10 = -num10;
        if (num1 < 0)
          num11 = -num11;
      }
      int num12 = num8 + num6;
      int num13 = (num7 << 1) - num6;
      int num14 = num7 << 1;
      int num15 = num7 - num6 << 1;
      double num16 = 1.0 / (4.0 * Math.Sqrt((double) (num6 * num6 + num7 * num7)));
      double num17 = 0.75 - 2.0 * ((double) num6 * num16);
      int num18 = (int) (num16 * 1024.0);
      int num19 = (int) (num17 * 1024.0 * (double) num3);
      int num20 = (int) (768.0 * (double) num3);
      int num21 = num18 * num3;
      int num22 = num6 * num21;
      int num23 = num13 * num21;
      int num24 = 0;
      int num25 = num14 * num21;
      int num26 = num15 * num21;
      do
      {
        WriteableBitmapExtensions.AlphaBlendNormalOnPremultiplied(context, index, num20 - num24 >> 10, srb, sg);
        WriteableBitmapExtensions.AlphaBlendNormalOnPremultiplied(context, index + num11, num19 + num24 >> 10, srb, sg);
        WriteableBitmapExtensions.AlphaBlendNormalOnPremultiplied(context, index - num11, num19 - num24 >> 10, srb, sg);
        if (num13 < 0)
        {
          num24 = num23 + num22;
          num13 += num14;
          num23 += num25;
        }
        else
        {
          num24 = num23 - num22;
          num13 += num15;
          num23 += num26;
          ++num9;
          index += num11;
        }
        ++num8;
        index += num10;
      }
      while (num8 < num12);
    }

    /// <summary>
    /// Blends a specific source color on top of a destination premultiplied color
    /// </summary>
    /// <param name="context">Array containing destination color</param>
    /// <param name="index">Index of destination pixel</param>
    /// <param name="sa">Source alpha (0..255)</param>
    /// <param name="srb">Source non-premultiplied red and blue component in the format 0x00rr00bb</param>
    /// <param name="sg">Source green component (0..255)</param>
    private static void AlphaBlendNormalOnPremultiplied(
      BitmapContext context,
      int index,
      int sa,
      uint srb,
      uint sg)
    {
      int[] pixels = context.Pixels;
      uint num1 = (uint) pixels[index];
      uint num2 = num1 >> 24;
      uint num3 = num1 >> 8 & (uint) byte.MaxValue;
      uint num4 = num1 & 16711935U;
      pixels[index] = (int) ((long) sa + ((long) num2 * (long) ((int) byte.MaxValue - sa) * 32897L >> 23) << 24 | (long) (sg - num3) * (long) sa + (long) (num3 << 8) & 4294967040L | ((long) (srb - num4) * (long) sa >> 8) + (long) num4 & 16711935L);
    }

    /// <summary>
    /// Draws a polyline. Add the first point also at the end of the array if the line should be closed.
    /// </summary>
    /// <param name="bmp">The WriteableBitmap.</param>
    /// <param name="points">The points of the polyline in x and y pairs, therefore the array is interpreted as (x1, y1, x2, y2, ..., xn, yn).</param>
    /// <param name="color">The color for the line.</param>
    public static void DrawPolyline(this WriteableBitmap bmp, int[] points, Color color)
    {
      int color1 = WriteableBitmapExtensions.ConvertColor(color);
      bmp.DrawPolyline(points, color1);
    }

    /// <summary>
    /// Draws a polyline. Add the first point also at the end of the array if the line should be closed.
    /// </summary>
    /// <param name="bmp">The WriteableBitmap.</param>
    /// <param name="points">The points of the polyline in x and y pairs, therefore the array is interpreted as (x1, y1, x2, y2, ..., xn, yn).</param>
    /// <param name="color">The color for the line.</param>
    public static void DrawPolyline(this WriteableBitmap bmp, int[] points, int color)
    {
      using (BitmapContext bitmapContext = bmp.GetBitmapContext())
      {
        int width = bitmapContext.Width;
        int height = bitmapContext.Height;
        int x1 = points[0];
        int y1 = points[1];
        for (int index = 2; index < points.Length; index += 2)
        {
          int point1 = points[index];
          int point2 = points[index + 1];
          WriteableBitmapExtensions.DrawLine(bitmapContext, width, height, x1, y1, point1, point2, color);
          x1 = point1;
          y1 = point2;
        }
      }
    }

    /// <summary>Draws a triangle.</summary>
    /// <param name="bmp">The WriteableBitmap.</param>
    /// <param name="x1">The x-coordinate of the 1st point.</param>
    /// <param name="y1">The y-coordinate of the 1st point.</param>
    /// <param name="x2">The x-coordinate of the 2nd point.</param>
    /// <param name="y2">The y-coordinate of the 2nd point.</param>
    /// <param name="x3">The x-coordinate of the 3rd point.</param>
    /// <param name="y3">The y-coordinate of the 3rd point.</param>
    /// <param name="color">The color.</param>
    public static void DrawTriangle(
      this WriteableBitmap bmp,
      int x1,
      int y1,
      int x2,
      int y2,
      int x3,
      int y3,
      Color color)
    {
      int color1 = WriteableBitmapExtensions.ConvertColor(color);
      bmp.DrawTriangle(x1, y1, x2, y2, x3, y3, color1);
    }

    /// <summary>Draws a triangle.</summary>
    /// <param name="bmp">The WriteableBitmap.</param>
    /// <param name="x1">The x-coordinate of the 1st point.</param>
    /// <param name="y1">The y-coordinate of the 1st point.</param>
    /// <param name="x2">The x-coordinate of the 2nd point.</param>
    /// <param name="y2">The y-coordinate of the 2nd point.</param>
    /// <param name="x3">The x-coordinate of the 3rd point.</param>
    /// <param name="y3">The y-coordinate of the 3rd point.</param>
    /// <param name="color">The color.</param>
    public static void DrawTriangle(
      this WriteableBitmap bmp,
      int x1,
      int y1,
      int x2,
      int y2,
      int x3,
      int y3,
      int color)
    {
      using (BitmapContext bitmapContext = bmp.GetBitmapContext())
      {
        int width = bitmapContext.Width;
        int height = bitmapContext.Height;
        WriteableBitmapExtensions.DrawLine(bitmapContext, width, height, x1, y1, x2, y2, color);
        WriteableBitmapExtensions.DrawLine(bitmapContext, width, height, x2, y2, x3, y3, color);
        WriteableBitmapExtensions.DrawLine(bitmapContext, width, height, x3, y3, x1, y1, color);
      }
    }

    /// <summary>Draws a quad.</summary>
    /// <param name="bmp">The WriteableBitmap.</param>
    /// <param name="x1">The x-coordinate of the 1st point.</param>
    /// <param name="y1">The y-coordinate of the 1st point.</param>
    /// <param name="x2">The x-coordinate of the 2nd point.</param>
    /// <param name="y2">The y-coordinate of the 2nd point.</param>
    /// <param name="x3">The x-coordinate of the 3rd point.</param>
    /// <param name="y3">The y-coordinate of the 3rd point.</param>
    /// <param name="x4">The x-coordinate of the 4th point.</param>
    /// <param name="y4">The y-coordinate of the 4th point.</param>
    /// <param name="color">The color.</param>
    public static void DrawQuad(
      this WriteableBitmap bmp,
      int x1,
      int y1,
      int x2,
      int y2,
      int x3,
      int y3,
      int x4,
      int y4,
      Color color)
    {
      int color1 = WriteableBitmapExtensions.ConvertColor(color);
      bmp.DrawQuad(x1, y1, x2, y2, x3, y3, x4, y4, color1);
    }

    /// <summary>Draws a quad.</summary>
    /// <param name="bmp">The WriteableBitmap.</param>
    /// <param name="x1">The x-coordinate of the 1st point.</param>
    /// <param name="y1">The y-coordinate of the 1st point.</param>
    /// <param name="x2">The x-coordinate of the 2nd point.</param>
    /// <param name="y2">The y-coordinate of the 2nd point.</param>
    /// <param name="x3">The x-coordinate of the 3rd point.</param>
    /// <param name="y3">The y-coordinate of the 3rd point.</param>
    /// <param name="x4">The x-coordinate of the 4th point.</param>
    /// <param name="y4">The y-coordinate of the 4th point.</param>
    /// <param name="color">The color.</param>
    public static void DrawQuad(
      this WriteableBitmap bmp,
      int x1,
      int y1,
      int x2,
      int y2,
      int x3,
      int y3,
      int x4,
      int y4,
      int color)
    {
      using (BitmapContext bitmapContext = bmp.GetBitmapContext())
      {
        int width = bitmapContext.Width;
        int height = bitmapContext.Height;
        WriteableBitmapExtensions.DrawLine(bitmapContext, width, height, x1, y1, x2, y2, color);
        WriteableBitmapExtensions.DrawLine(bitmapContext, width, height, x2, y2, x3, y3, color);
        WriteableBitmapExtensions.DrawLine(bitmapContext, width, height, x3, y3, x4, y4, color);
        WriteableBitmapExtensions.DrawLine(bitmapContext, width, height, x4, y4, x1, y1, color);
      }
    }

    /// <summary>
    /// Draws a rectangle.
    /// x2 has to be greater than x1 and y2 has to be greater than y1.
    /// </summary>
    /// <param name="bmp">The WriteableBitmap.</param>
    /// <param name="x1">The x-coordinate of the bounding rectangle's left side.</param>
    /// <param name="y1">The y-coordinate of the bounding rectangle's top side.</param>
    /// <param name="x2">The x-coordinate of the bounding rectangle's right side.</param>
    /// <param name="y2">The y-coordinate of the bounding rectangle's bottom side.</param>
    /// <param name="color">The color.</param>
    public static void DrawRectangle(
      this WriteableBitmap bmp,
      int x1,
      int y1,
      int x2,
      int y2,
      Color color)
    {
      int color1 = WriteableBitmapExtensions.ConvertColor(color);
      bmp.DrawRectangle(x1, y1, x2, y2, color1);
    }

    /// <summary>
    /// Draws a rectangle.
    /// x2 has to be greater than x1 and y2 has to be greater than y1.
    /// </summary>
    /// <param name="bmp">The WriteableBitmap.</param>
    /// <param name="x1">The x-coordinate of the bounding rectangle's left side.</param>
    /// <param name="y1">The y-coordinate of the bounding rectangle's top side.</param>
    /// <param name="x2">The x-coordinate of the bounding rectangle's right side.</param>
    /// <param name="y2">The y-coordinate of the bounding rectangle's bottom side.</param>
    /// <param name="color">The color.</param>
    public static void DrawRectangle(
      this WriteableBitmap bmp,
      int x1,
      int y1,
      int x2,
      int y2,
      int color)
    {
      using (BitmapContext bitmapContext = bmp.GetBitmapContext())
      {
        int width = bitmapContext.Width;
        int height = bitmapContext.Height;
        int[] pixels = bitmapContext.Pixels;
        if (x1 < 0 && x2 < 0 || y1 < 0 && y2 < 0 || x1 >= width && x2 >= width || y1 >= height && y2 >= height)
          return;
        if (x1 < 0)
          x1 = 0;
        if (y1 < 0)
          y1 = 0;
        if (x2 < 0)
          x2 = 0;
        if (y2 < 0)
          y2 = 0;
        if (x1 >= width)
          x1 = width - 1;
        if (y1 >= height)
          y1 = height - 1;
        if (x2 >= width)
          x2 = width - 1;
        if (y2 >= height)
          y2 = height - 1;
        int num1 = y1 * width;
        int index1 = y2 * width + x1;
        int num2 = num1 + x2;
        int num3 = num1 + x1;
        for (int index2 = num3; index2 <= num2; ++index2)
        {
          pixels[index2] = color;
          pixels[index1] = color;
          ++index1;
        }
        int index3 = num3 + width;
        int num4 = index1 - width;
        for (int index4 = num1 + x2 + width; index4 <= num4; index4 += width)
        {
          pixels[index4] = color;
          pixels[index3] = color;
          index3 += width;
        }
      }
    }

    /// <summary>
    /// A Fast Bresenham Type Algorithm For Drawing Ellipses http://homepage.smc.edu/kennedy_john/belipse.pdf
    /// x2 has to be greater than x1 and y2 has to be greater than y1.
    /// </summary>
    /// <param name="bmp">The WriteableBitmap.</param>
    /// <param name="x1">The x-coordinate of the bounding rectangle's left side.</param>
    /// <param name="y1">The y-coordinate of the bounding rectangle's top side.</param>
    /// <param name="x2">The x-coordinate of the bounding rectangle's right side.</param>
    /// <param name="y2">The y-coordinate of the bounding rectangle's bottom side.</param>
    /// <param name="color">The color for the line.</param>
    public static void DrawEllipse(
      this WriteableBitmap bmp,
      int x1,
      int y1,
      int x2,
      int y2,
      Color color)
    {
      int color1 = WriteableBitmapExtensions.ConvertColor(color);
      bmp.DrawEllipse(x1, y1, x2, y2, color1);
    }

    /// <summary>
    /// A Fast Bresenham Type Algorithm For Drawing Ellipses http://homepage.smc.edu/kennedy_john/belipse.pdf
    /// x2 has to be greater than x1 and y2 has to be greater than y1.
    /// </summary>
    /// <param name="bmp">The WriteableBitmap.</param>
    /// <param name="x1">The x-coordinate of the bounding rectangle's left side.</param>
    /// <param name="y1">The y-coordinate of the bounding rectangle's top side.</param>
    /// <param name="x2">The x-coordinate of the bounding rectangle's right side.</param>
    /// <param name="y2">The y-coordinate of the bounding rectangle's bottom side.</param>
    /// <param name="color">The color for the line.</param>
    public static void DrawEllipse(
      this WriteableBitmap bmp,
      int x1,
      int y1,
      int x2,
      int y2,
      int color)
    {
      int xr = x2 - x1 >> 1;
      int yr = y2 - y1 >> 1;
      int xc = x1 + xr;
      int yc = y1 + yr;
      bmp.DrawEllipseCentered(xc, yc, xr, yr, color);
    }

    /// <summary>
    /// A Fast Bresenham Type Algorithm For Drawing Ellipses http://homepage.smc.edu/kennedy_john/belipse.pdf
    /// Uses a different parameter representation than DrawEllipse().
    /// </summary>
    /// <param name="bmp">The WriteableBitmap.</param>
    /// <param name="xc">The x-coordinate of the ellipses center.</param>
    /// <param name="yc">The y-coordinate of the ellipses center.</param>
    /// <param name="xr">The radius of the ellipse in x-direction.</param>
    /// <param name="yr">The radius of the ellipse in y-direction.</param>
    /// <param name="color">The color for the line.</param>
    public static void DrawEllipseCentered(
      this WriteableBitmap bmp,
      int xc,
      int yc,
      int xr,
      int yr,
      Color color)
    {
      int color1 = WriteableBitmapExtensions.ConvertColor(color);
      bmp.DrawEllipseCentered(xc, yc, xr, yr, color1);
    }

    /// <summary>
    /// A Fast Bresenham Type Algorithm For Drawing Ellipses http://homepage.smc.edu/kennedy_john/belipse.pdf
    /// Uses a different parameter representation than DrawEllipse().
    /// </summary>
    /// <param name="bmp">The WriteableBitmap.</param>
    /// <param name="xc">The x-coordinate of the ellipses center.</param>
    /// <param name="yc">The y-coordinate of the ellipses center.</param>
    /// <param name="xr">The radius of the ellipse in x-direction.</param>
    /// <param name="yr">The radius of the ellipse in y-direction.</param>
    /// <param name="color">The color for the line.</param>
    public static void DrawEllipseCentered(
      this WriteableBitmap bmp,
      int xc,
      int yc,
      int xr,
      int yr,
      int color)
    {
      using (BitmapContext bitmapContext = bmp.GetBitmapContext())
      {
        int[] pixels = bitmapContext.Pixels;
        int width = bitmapContext.Width;
        int height = bitmapContext.Height;
        if (xr < 1 || yr < 1)
          return;
        int num1 = xr;
        int num2 = 0;
        int num3 = xr * xr << 1;
        int num4 = yr * yr << 1;
        int num5 = yr * yr * (1 - (xr << 1));
        int num6 = xr * xr;
        int num7 = 0;
        int num8 = num4 * xr;
        int num9 = 0;
        while (num8 >= num9)
        {
          int num10 = yc + num2;
          int num11 = yc - num2;
          if (num10 < 0)
            num10 = 0;
          if (num10 >= height)
            num10 = height - 1;
          if (num11 < 0)
            num11 = 0;
          if (num11 >= height)
            num11 = height - 1;
          int num12 = num10 * width;
          int num13 = num11 * width;
          int num14 = xc + num1;
          int num15 = xc - num1;
          if (num14 < 0)
            num14 = 0;
          if (num14 >= width)
            num14 = width - 1;
          if (num15 < 0)
            num15 = 0;
          if (num15 >= width)
            num15 = width - 1;
          pixels[num14 + num12] = color;
          pixels[num15 + num12] = color;
          pixels[num15 + num13] = color;
          pixels[num14 + num13] = color;
          ++num2;
          num9 += num3;
          num7 += num6;
          num6 += num3;
          if (num5 + (num7 << 1) > 0)
          {
            --num1;
            num8 -= num4;
            num7 += num5;
            num5 += num4;
          }
        }
        int num16 = 0;
        int num17 = yr;
        int num18 = yc + num17;
        int num19 = yc - num17;
        if (num18 < 0)
          num18 = 0;
        if (num18 >= height)
          num18 = height - 1;
        if (num19 < 0)
          num19 = 0;
        if (num19 >= height)
          num19 = height - 1;
        int num20 = num18 * width;
        int num21 = num19 * width;
        int num22 = yr * yr;
        int num23 = xr * xr * (1 - (yr << 1));
        int num24 = 0;
        int num25 = 0;
        int num26 = num3 * yr;
        while (num25 <= num26)
        {
          int num27 = xc + num16;
          int num28 = xc - num16;
          if (num27 < 0)
            num27 = 0;
          if (num27 >= width)
            num27 = width - 1;
          if (num28 < 0)
            num28 = 0;
          if (num28 >= width)
            num28 = width - 1;
          pixels[num27 + num20] = color;
          pixels[num28 + num20] = color;
          pixels[num28 + num21] = color;
          pixels[num27 + num21] = color;
          ++num16;
          num25 += num4;
          num24 += num22;
          num22 += num4;
          if (num23 + (num24 << 1) > 0)
          {
            --num17;
            int num29 = yc + num17;
            int num30 = yc - num17;
            if (num29 < 0)
              num29 = 0;
            if (num29 >= height)
              num29 = height - 1;
            if (num30 < 0)
              num30 = 0;
            if (num30 >= height)
              num30 = height - 1;
            num20 = num29 * width;
            num21 = num30 * width;
            num26 -= num3;
            num24 += num23;
            num23 += num3;
          }
        }
      }
    }

    /// <summary>
    /// Draws a cubic Beziér spline defined by start, end and two control points.
    /// </summary>
    /// <param name="bmp">The WriteableBitmap.</param>
    /// <param name="x1">The x-coordinate of the start point.</param>
    /// <param name="y1">The y-coordinate of the start point.</param>
    /// <param name="cx1">The x-coordinate of the 1st control point.</param>
    /// <param name="cy1">The y-coordinate of the 1st control point.</param>
    /// <param name="cx2">The x-coordinate of the 2nd control point.</param>
    /// <param name="cy2">The y-coordinate of the 2nd control point.</param>
    /// <param name="x2">The x-coordinate of the end point.</param>
    /// <param name="y2">The y-coordinate of the end point.</param>
    /// <param name="color">The color.</param>
    public static void DrawBezier(
      this WriteableBitmap bmp,
      int x1,
      int y1,
      int cx1,
      int cy1,
      int cx2,
      int cy2,
      int x2,
      int y2,
      Color color)
    {
      int color1 = WriteableBitmapExtensions.ConvertColor(color);
      bmp.DrawBezier(x1, y1, cx1, cy1, cx2, cy2, x2, y2, color1);
    }

    /// <summary>
    /// Draws a cubic Beziér spline defined by start, end and two control points.
    /// </summary>
    /// <param name="bmp">The WriteableBitmap.</param>
    /// <param name="x1">The x-coordinate of the start point.</param>
    /// <param name="y1">The y-coordinate of the start point.</param>
    /// <param name="cx1">The x-coordinate of the 1st control point.</param>
    /// <param name="cy1">The y-coordinate of the 1st control point.</param>
    /// <param name="cx2">The x-coordinate of the 2nd control point.</param>
    /// <param name="cy2">The y-coordinate of the 2nd control point.</param>
    /// <param name="x2">The x-coordinate of the end point.</param>
    /// <param name="y2">The y-coordinate of the end point.</param>
    /// <param name="color">The color.</param>
    public static void DrawBezier(
      this WriteableBitmap bmp,
      int x1,
      int y1,
      int cx1,
      int cy1,
      int cx2,
      int cy2,
      int x2,
      int y2,
      int color)
    {
      int num1 = Math.Min(x1, Math.Min(cx1, Math.Min(cx2, x2)));
      int num2 = Math.Min(y1, Math.Min(cy1, Math.Min(cy2, y2)));
      int num3 = Math.Max(x1, Math.Max(cx1, Math.Max(cx2, x2)));
      int num4 = Math.Max(y1, Math.Max(cy1, Math.Max(cy2, y2)));
      int num5 = num3 - num1;
      int num6 = num4 - num2;
      if (num5 > num6)
        num6 = num5;
      if (num6 == 0)
        return;
      using (BitmapContext bitmapContext = bmp.GetBitmapContext())
      {
        int width = bitmapContext.Width;
        int height = bitmapContext.Height;
        float num7 = 2f / (float) num6;
        int x1_1 = x1;
        int y1_1 = y1;
        for (float num8 = num7; (double) num8 <= 1.0; num8 += num7)
        {
          float num9 = num8 * num8;
          float num10 = 1f - num8;
          float num11 = num10 * num10;
          int x2_1 = (int) ((double) num10 * (double) num11 * (double) x1 + 3.0 * (double) num8 * (double) num11 * (double) cx1 + 3.0 * (double) num10 * (double) num9 * (double) cx2 + (double) num8 * (double) num9 * (double) x2);
          int y2_1 = (int) ((double) num10 * (double) num11 * (double) y1 + 3.0 * (double) num8 * (double) num11 * (double) cy1 + 3.0 * (double) num10 * (double) num9 * (double) cy2 + (double) num8 * (double) num9 * (double) y2);
          WriteableBitmapExtensions.DrawLine(bitmapContext, width, height, x1_1, y1_1, x2_1, y2_1, color);
          x1_1 = x2_1;
          y1_1 = y2_1;
        }
        WriteableBitmapExtensions.DrawLine(bitmapContext, width, height, x1_1, y1_1, x2, y2, color);
      }
    }

    /// <summary>
    /// Draws a series of cubic Beziér splines each defined by start, end and two control points.
    /// The ending point of the previous curve is used as starting point for the next.
    /// Therfore the inital curve needs four points and the subsequent 3 (2 control and 1 end point).
    /// </summary>
    /// <param name="bmp">The WriteableBitmap.</param>
    /// <param name="points">The points for the curve in x and y pairs, therefore the array is interpreted as (x1, y1, cx1, cy1, cx2, cy2, x2, y2, cx3, cx4 ..., xn, yn).</param>
    /// <param name="color">The color for the spline.</param>
    public static void DrawBeziers(this WriteableBitmap bmp, int[] points, Color color)
    {
      int color1 = WriteableBitmapExtensions.ConvertColor(color);
      bmp.DrawBeziers(points, color1);
    }

    /// <summary>
    /// Draws a series of cubic Beziér splines each defined by start, end and two control points.
    /// The ending point of the previous curve is used as starting point for the next.
    /// Therfore the inital curve needs four points and the subsequent 3 (2 control and 1 end point).
    /// </summary>
    /// <param name="bmp">The WriteableBitmap.</param>
    /// <param name="points">The points for the curve in x and y pairs, therefore the array is interpreted as (x1, y1, cx1, cy1, cx2, cy2, x2, y2, cx3, cx4 ..., xn, yn).</param>
    /// <param name="color">The color for the spline.</param>
    public static void DrawBeziers(this WriteableBitmap bmp, int[] points, int color)
    {
      int x1 = points[0];
      int y1 = points[1];
      for (int index = 2; index + 5 < points.Length; index += 6)
      {
        int point1 = points[index + 4];
        int point2 = points[index + 5];
        bmp.DrawBezier(x1, y1, points[index], points[index + 1], points[index + 2], points[index + 3], point1, point2, color);
        x1 = point1;
        y1 = point2;
      }
    }

    /// <summary>
    /// Draws a segment of a Cardinal spline (cubic) defined by four control points.
    /// </summary>
    /// <param name="x1">The x-coordinate of the 1st control point.</param>
    /// <param name="y1">The y-coordinate of the 1st control point.</param>
    /// <param name="x2">The x-coordinate of the 2nd control point.</param>
    /// <param name="y2">The y-coordinate of the 2nd control point.</param>
    /// <param name="x3">The x-coordinate of the 3rd control point.</param>
    /// <param name="y3">The y-coordinate of the 3rd control point.</param>
    /// <param name="x4">The x-coordinate of the 4th control point.</param>
    /// <param name="y4">The y-coordinate of the 4th control point.</param>
    /// <param name="tension">The tension of the curve defines the shape. Usually between 0 and 1. 0 would be a straight line.</param>
    /// <param name="color">The color.</param>
    /// <param name="context">The pixel context.</param>
    /// <param name="w">The width of the bitmap.</param>
    /// <param name="h">The height of the bitmap.</param>
    private static void DrawCurveSegment(
      int x1,
      int y1,
      int x2,
      int y2,
      int x3,
      int y3,
      int x4,
      int y4,
      float tension,
      int color,
      BitmapContext context,
      int w,
      int h)
    {
      int num1 = Math.Min(x1, Math.Min(x2, Math.Min(x3, x4)));
      int num2 = Math.Min(y1, Math.Min(y2, Math.Min(y3, y4)));
      int num3 = Math.Max(x1, Math.Max(x2, Math.Max(x3, x4)));
      int num4 = Math.Max(y1, Math.Max(y2, Math.Max(y3, y4)));
      int num5 = num3 - num1;
      int num6 = num4 - num2;
      if (num5 > num6)
        num6 = num5;
      if (num6 == 0)
        return;
      float num7 = 2f / (float) num6;
      int x1_1 = x2;
      int y1_1 = y2;
      float num8 = tension * (float) (x3 - x1);
      float num9 = tension * (float) (y3 - y1);
      float num10 = tension * (float) (x4 - x2);
      float num11 = tension * (float) (y4 - y2);
      float num12 = num8 + num10 + (float) (2 * x2) - (float) (2 * x3);
      float num13 = num9 + num11 + (float) (2 * y2) - (float) (2 * y3);
      float num14 = -2f * num8 - num10 - (float) (3 * x2) + (float) (3 * x3);
      float num15 = -2f * num9 - num11 - (float) (3 * y2) + (float) (3 * y3);
      for (float num16 = num7; (double) num16 <= 1.0; num16 += num7)
      {
        float num17 = num16 * num16;
        int x2_1 = (int) ((double) num12 * (double) num17 * (double) num16 + (double) num14 * (double) num17 + (double) num8 * (double) num16 + (double) x2);
        int y2_1 = (int) ((double) num13 * (double) num17 * (double) num16 + (double) num15 * (double) num17 + (double) num9 * (double) num16 + (double) y2);
        WriteableBitmapExtensions.DrawLine(context, w, h, x1_1, y1_1, x2_1, y2_1, color);
        x1_1 = x2_1;
        y1_1 = y2_1;
      }
      WriteableBitmapExtensions.DrawLine(context, w, h, x1_1, y1_1, x3, y3, color);
    }

    /// <summary>
    /// Draws a Cardinal spline (cubic) defined by a point collection.
    /// The cardinal spline passes through each point in the collection.
    /// </summary>
    /// <param name="bmp">The WriteableBitmap.</param>
    /// <param name="points">The points for the curve in x and y pairs, therefore the array is interpreted as (x1, y1, x2, y2, x3, y3, x4, y4, x1, x2 ..., xn, yn).</param>
    /// <param name="tension">The tension of the curve defines the shape. Usually between 0 and 1. 0 would be a straight line.</param>
    /// <param name="color">The color for the spline.</param>
    public static void DrawCurve(
      this WriteableBitmap bmp,
      int[] points,
      float tension,
      Color color)
    {
      int color1 = WriteableBitmapExtensions.ConvertColor(color);
      bmp.DrawCurve(points, tension, color1);
    }

    /// <summary>
    /// Draws a Cardinal spline (cubic) defined by a point collection.
    /// The cardinal spline passes through each point in the collection.
    /// </summary>
    /// <param name="bmp">The WriteableBitmap.</param>
    /// <param name="points">The points for the curve in x and y pairs, therefore the array is interpreted as (x1, y1, x2, y2, x3, y3, x4, y4, x1, x2 ..., xn, yn).</param>
    /// <param name="tension">The tension of the curve defines the shape. Usually between 0 and 1. 0 would be a straight line.</param>
    /// <param name="color">The color for the spline.</param>
    public static void DrawCurve(this WriteableBitmap bmp, int[] points, float tension, int color)
    {
      using (BitmapContext bitmapContext = bmp.GetBitmapContext())
      {
        int width = bitmapContext.Width;
        int height = bitmapContext.Height;
        WriteableBitmapExtensions.DrawCurveSegment(points[0], points[1], points[0], points[1], points[2], points[3], points[4], points[5], tension, color, bitmapContext, width, height);
        int index;
        for (index = 2; index < points.Length - 4; index += 2)
          WriteableBitmapExtensions.DrawCurveSegment(points[index - 2], points[index - 1], points[index], points[index + 1], points[index + 2], points[index + 3], points[index + 4], points[index + 5], tension, color, bitmapContext, width, height);
        WriteableBitmapExtensions.DrawCurveSegment(points[index - 2], points[index - 1], points[index], points[index + 1], points[index + 2], points[index + 3], points[index + 2], points[index + 3], tension, color, bitmapContext, width, height);
      }
    }

    /// <summary>
    /// Draws a closed Cardinal spline (cubic) defined by a point collection.
    /// The cardinal spline passes through each point in the collection.
    /// </summary>
    /// <param name="bmp">The WriteableBitmap.</param>
    /// <param name="points">The points for the curve in x and y pairs, therefore the array is interpreted as (x1, y1, x2, y2, x3, y3, x4, y4, x1, x2 ..., xn, yn).</param>
    /// <param name="tension">The tension of the curve defines the shape. Usually between 0 and 1. 0 would be a straight line.</param>
    /// <param name="color">The color for the spline.</param>
    public static void DrawCurveClosed(
      this WriteableBitmap bmp,
      int[] points,
      float tension,
      Color color)
    {
      int color1 = WriteableBitmapExtensions.ConvertColor(color);
      bmp.DrawCurveClosed(points, tension, color1);
    }

    /// <summary>
    /// Draws a closed Cardinal spline (cubic) defined by a point collection.
    /// The cardinal spline passes through each point in the collection.
    /// </summary>
    /// <param name="bmp">The WriteableBitmap.</param>
    /// <param name="points">The points for the curve in x and y pairs, therefore the array is interpreted as (x1, y1, x2, y2, x3, y3, x4, y4, x1, x2 ..., xn, yn).</param>
    /// <param name="tension">The tension of the curve defines the shape. Usually between 0 and 1. 0 would be a straight line.</param>
    /// <param name="color">The color for the spline.</param>
    public static void DrawCurveClosed(
      this WriteableBitmap bmp,
      int[] points,
      float tension,
      int color)
    {
      using (BitmapContext bitmapContext = bmp.GetBitmapContext())
      {
        int width = bitmapContext.Width;
        int height = bitmapContext.Height;
        int length = points.Length;
        WriteableBitmapExtensions.DrawCurveSegment(points[length - 2], points[length - 1], points[0], points[1], points[2], points[3], points[4], points[5], tension, color, bitmapContext, width, height);
        int index;
        for (index = 2; index < length - 4; index += 2)
          WriteableBitmapExtensions.DrawCurveSegment(points[index - 2], points[index - 1], points[index], points[index + 1], points[index + 2], points[index + 3], points[index + 4], points[index + 5], tension, color, bitmapContext, width, height);
        WriteableBitmapExtensions.DrawCurveSegment(points[index - 2], points[index - 1], points[index], points[index + 1], points[index + 2], points[index + 3], points[0], points[1], tension, color, bitmapContext, width, height);
        WriteableBitmapExtensions.DrawCurveSegment(points[index], points[index + 1], points[index + 2], points[index + 3], points[0], points[1], points[2], points[3], tension, color, bitmapContext, width, height);
      }
    }

    /// <summary>The interpolation method.</summary>
    public enum Interpolation
    {
      /// <summary>
      /// The nearest neighbor algorithm simply selects the color of the nearest pixel.
      /// </summary>
      NearestNeighbor,
      /// <summary>
      /// Linear interpolation in 2D using the average of 3 neighboring pixels.
      /// </summary>
      Bilinear,
    }

    /// <summary>The mode for flipping.</summary>
    public enum FlipMode
    {
      /// <summary>
      /// Flips the image vertical (around the center of the y-axis).
      /// </summary>
      Vertical,
      /// <summary>
      /// Flips the image horizontal (around the center of the x-axis).
      /// </summary>
      Horizontal,
    }

    /// <summary>The blending mode.</summary>
    public enum BlendMode
    {
      /// <summary>
      /// Alpha blendiing uses the alpha channel to combine the source and destination.
      /// </summary>
      Alpha,
      /// <summary>
      /// Additive blending adds the colors of the source and the destination.
      /// </summary>
      Additive,
      /// <summary>
      /// Subtractive blending subtracts the source color from the destination.
      /// </summary>
      Subtractive,
      /// <summary>Uses the source color as a mask.</summary>
      Mask,
      /// <summary>
      /// Multiplies the source color with the destination color.
      /// </summary>
      Multiply,
      /// <summary>Ignores the specified Color</summary>
      ColorKeying,
      /// <summary>No blending just copies the pixels from the source.</summary>
      None,
    }
  }
}
