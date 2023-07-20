// Decompiled with JetBrains decompiler
// Type: WalletPass.ToolStackCRCLib.Adler32
// Assembly: Wallet Pass, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1E8AC314-47AB-4931-BC36-563B31C55EF5
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Wallet Pass.dll

using System;

namespace WalletPass.ToolStackCRCLib
{
  public class Adler32
  {
    private const int MOD_ADLER = 65521;
    private uint AdlerA = 1;
    private uint AdlerB;

    public uint adler() => this.AdlerB << 16 | this.AdlerA;

    public uint adler(byte[] data) => this.adler(data, data.Length, 0U);

    public uint adler(byte[] data, int len) => this.adler(data, len, 0U);

    public uint adler(byte[] data, int len, uint offset)
    {
      uint num1 = 1;
      uint num2 = 0;
      for (uint index = offset; (long) index < (long) offset + (long) len; ++index)
      {
        num1 = (num1 + (uint) data[(IntPtr) index]) % 65521U;
        num2 = (num2 + num1) % 65521U;
      }
      return num2 << 16 | num1;
    }

    public void addToAdler(byte[] data) => this.addToAdler(data, data.Length, 0U);

    public void addToAdler(byte[] data, int len) => this.addToAdler(data, len, 0U);

    public void addToAdler(byte[] data, int len, uint offset)
    {
      for (uint index = offset; (long) index < (long) offset + (long) len; ++index)
      {
        this.AdlerA = (this.AdlerA + (uint) data[(IntPtr) index]) % 65521U;
        this.AdlerB = (this.AdlerB + this.AdlerA) % 65521U;
      }
    }

    public void resetAdler()
    {
      this.AdlerA = 1U;
      this.AdlerB = 0U;
    }
  }
}
