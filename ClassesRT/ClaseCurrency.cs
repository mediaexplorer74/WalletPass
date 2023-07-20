﻿// Decompiled with JetBrains decompiler
// Type: Wallet_Pass.ClaseCurrency
// Assembly: ClassesRT, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4763AD48-8D96-449E-A89B-9A47C90F0CDD
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\ClassesRT.dll

using System.Collections.Generic;

namespace Wallet_Pass
{
  internal class ClaseCurrency
  {
    private List<string> ISOCurrencySymbol { get; set; }

    private List<string> CurrencySymbol { get; set; }

    public ClaseCurrency()
    {
      this.ISOCurrencySymbol = new List<string>();
      this.CurrencySymbol = new List<string>();
      this.ISOCurrencySymbol.Add("AED");
      this.ISOCurrencySymbol.Add("AFN");
      this.ISOCurrencySymbol.Add("ALL");
      this.ISOCurrencySymbol.Add("AMD");
      this.ISOCurrencySymbol.Add("ARS");
      this.ISOCurrencySymbol.Add("AUD");
      this.ISOCurrencySymbol.Add("AZN");
      this.ISOCurrencySymbol.Add("BAM");
      this.ISOCurrencySymbol.Add("BDT");
      this.ISOCurrencySymbol.Add("BGN");
      this.ISOCurrencySymbol.Add("BHD");
      this.ISOCurrencySymbol.Add("BND");
      this.ISOCurrencySymbol.Add("BOB");
      this.ISOCurrencySymbol.Add("BRL");
      this.ISOCurrencySymbol.Add("BYR");
      this.ISOCurrencySymbol.Add("BZD");
      this.ISOCurrencySymbol.Add("CAD");
      this.ISOCurrencySymbol.Add("CHF");
      this.ISOCurrencySymbol.Add("CLP");
      this.ISOCurrencySymbol.Add("CNY");
      this.ISOCurrencySymbol.Add("COP");
      this.ISOCurrencySymbol.Add("CRC");
      this.ISOCurrencySymbol.Add("CSD");
      this.ISOCurrencySymbol.Add("CZK");
      this.ISOCurrencySymbol.Add("DKK");
      this.ISOCurrencySymbol.Add("DOP");
      this.ISOCurrencySymbol.Add("DZD");
      this.ISOCurrencySymbol.Add("EEK");
      this.ISOCurrencySymbol.Add("EGP");
      this.ISOCurrencySymbol.Add("ETB");
      this.ISOCurrencySymbol.Add("EUR");
      this.ISOCurrencySymbol.Add("GBP");
      this.ISOCurrencySymbol.Add("GEL");
      this.ISOCurrencySymbol.Add("GTQ");
      this.ISOCurrencySymbol.Add("HKD");
      this.ISOCurrencySymbol.Add("HNL");
      this.ISOCurrencySymbol.Add("HRK");
      this.ISOCurrencySymbol.Add("HUF");
      this.ISOCurrencySymbol.Add("IDR");
      this.ISOCurrencySymbol.Add("ILS");
      this.ISOCurrencySymbol.Add("INR");
      this.ISOCurrencySymbol.Add("IQD");
      this.ISOCurrencySymbol.Add("IRR");
      this.ISOCurrencySymbol.Add("ISK");
      this.ISOCurrencySymbol.Add("JMD");
      this.ISOCurrencySymbol.Add("JOD");
      this.ISOCurrencySymbol.Add("JPY");
      this.ISOCurrencySymbol.Add("KES");
      this.ISOCurrencySymbol.Add("KGS");
      this.ISOCurrencySymbol.Add("KHR");
      this.ISOCurrencySymbol.Add("KRW");
      this.ISOCurrencySymbol.Add("KWD");
      this.ISOCurrencySymbol.Add("KZT");
      this.ISOCurrencySymbol.Add("LAK");
      this.ISOCurrencySymbol.Add("LBP");
      this.ISOCurrencySymbol.Add("LKR");
      this.ISOCurrencySymbol.Add("LTL");
      this.ISOCurrencySymbol.Add("LVL");
      this.ISOCurrencySymbol.Add("LYD");
      this.ISOCurrencySymbol.Add("MAD");
      this.ISOCurrencySymbol.Add("MKD");
      this.ISOCurrencySymbol.Add("MNT");
      this.ISOCurrencySymbol.Add("MOP");
      this.ISOCurrencySymbol.Add("MVR");
      this.ISOCurrencySymbol.Add("MXN");
      this.ISOCurrencySymbol.Add("MYR");
      this.ISOCurrencySymbol.Add("NIO");
      this.ISOCurrencySymbol.Add("NOK");
      this.ISOCurrencySymbol.Add("NPR");
      this.ISOCurrencySymbol.Add("NZD");
      this.ISOCurrencySymbol.Add("OMR");
      this.ISOCurrencySymbol.Add("PAB");
      this.ISOCurrencySymbol.Add("PEN");
      this.ISOCurrencySymbol.Add("PHP");
      this.ISOCurrencySymbol.Add("PKR");
      this.ISOCurrencySymbol.Add("PLN");
      this.ISOCurrencySymbol.Add("PYG");
      this.ISOCurrencySymbol.Add("QAR");
      this.ISOCurrencySymbol.Add("RON");
      this.ISOCurrencySymbol.Add("RSD");
      this.ISOCurrencySymbol.Add("RUB");
      this.ISOCurrencySymbol.Add("RWF");
      this.ISOCurrencySymbol.Add("SAR");
      this.ISOCurrencySymbol.Add("SEK");
      this.ISOCurrencySymbol.Add("SGD");
      this.ISOCurrencySymbol.Add("SYP");
      this.ISOCurrencySymbol.Add("THB");
      this.ISOCurrencySymbol.Add("TJS");
      this.ISOCurrencySymbol.Add("TMT");
      this.ISOCurrencySymbol.Add("TND");
      this.ISOCurrencySymbol.Add("TRY");
      this.ISOCurrencySymbol.Add("TTD");
      this.ISOCurrencySymbol.Add("TWD");
      this.ISOCurrencySymbol.Add("UAH");
      this.ISOCurrencySymbol.Add("USD");
      this.ISOCurrencySymbol.Add("UYU");
      this.ISOCurrencySymbol.Add("UZS");
      this.ISOCurrencySymbol.Add("VEF");
      this.ISOCurrencySymbol.Add("VND");
      this.ISOCurrencySymbol.Add("XOF");
      this.ISOCurrencySymbol.Add("YER");
      this.ISOCurrencySymbol.Add("ZAR");
      this.ISOCurrencySymbol.Add("ZWL");
      this.CurrencySymbol.Add("د.إ.\u200F");
      this.CurrencySymbol.Add(" ؋");
      this.CurrencySymbol.Add("Lek");
      this.CurrencySymbol.Add("դր.");
      this.CurrencySymbol.Add("$");
      this.CurrencySymbol.Add("$");
      this.CurrencySymbol.Add("man.");
      this.CurrencySymbol.Add("KM");
      this.CurrencySymbol.Add("৳");
      this.CurrencySymbol.Add("лв.");
      this.CurrencySymbol.Add("د.ب.\u200F");
      this.CurrencySymbol.Add("$");
      this.CurrencySymbol.Add("$b");
      this.CurrencySymbol.Add("R$");
      this.CurrencySymbol.Add("р.");
      this.CurrencySymbol.Add("BZ$");
      this.CurrencySymbol.Add("$");
      this.CurrencySymbol.Add("fr.");
      this.CurrencySymbol.Add("$");
      this.CurrencySymbol.Add("¥");
      this.CurrencySymbol.Add("$");
      this.CurrencySymbol.Add("₡");
      this.CurrencySymbol.Add("Din.");
      this.CurrencySymbol.Add("Kč");
      this.CurrencySymbol.Add("kr.");
      this.CurrencySymbol.Add("RD$");
      this.CurrencySymbol.Add("DZD");
      this.CurrencySymbol.Add("kr");
      this.CurrencySymbol.Add("ج.م.\u200F");
      this.CurrencySymbol.Add("ETB");
      this.CurrencySymbol.Add("€");
      this.CurrencySymbol.Add("£");
      this.CurrencySymbol.Add("Lari");
      this.CurrencySymbol.Add("Q");
      this.CurrencySymbol.Add("HK$");
      this.CurrencySymbol.Add("L.");
      this.CurrencySymbol.Add("kn");
      this.CurrencySymbol.Add("Ft");
      this.CurrencySymbol.Add("Rp");
      this.CurrencySymbol.Add("₪");
      this.CurrencySymbol.Add("रु");
      this.CurrencySymbol.Add("د.ع.\u200F");
      this.CurrencySymbol.Add("ريال");
      this.CurrencySymbol.Add("kr.");
      this.CurrencySymbol.Add("J$");
      this.CurrencySymbol.Add("د.ا.\u200F");
      this.CurrencySymbol.Add("¥");
      this.CurrencySymbol.Add("S");
      this.CurrencySymbol.Add("сом");
      this.CurrencySymbol.Add("៛");
      this.CurrencySymbol.Add("₩");
      this.CurrencySymbol.Add("د.ك.\u200F");
      this.CurrencySymbol.Add("Т");
      this.CurrencySymbol.Add("₭");
      this.CurrencySymbol.Add("ل.ل.\u200F");
      this.CurrencySymbol.Add("රු.");
      this.CurrencySymbol.Add("Lt");
      this.CurrencySymbol.Add("Ls");
      this.CurrencySymbol.Add("د.ل.\u200F");
      this.CurrencySymbol.Add("د.م.\u200F");
      this.CurrencySymbol.Add("ден.");
      this.CurrencySymbol.Add("₮");
      this.CurrencySymbol.Add("MOP");
      this.CurrencySymbol.Add("ރ.");
      this.CurrencySymbol.Add("$");
      this.CurrencySymbol.Add("RM");
      this.CurrencySymbol.Add("N");
      this.CurrencySymbol.Add("kr");
      this.CurrencySymbol.Add("रु");
      this.CurrencySymbol.Add("$");
      this.CurrencySymbol.Add("ر.ع.\u200F");
      this.CurrencySymbol.Add("B/.");
      this.CurrencySymbol.Add("S/.");
      this.CurrencySymbol.Add("PhP");
      this.CurrencySymbol.Add("Rs");
      this.CurrencySymbol.Add("zł");
      this.CurrencySymbol.Add("Gs");
      this.CurrencySymbol.Add("ر.ق.\u200F");
      this.CurrencySymbol.Add("lei");
      this.CurrencySymbol.Add("Din.");
      this.CurrencySymbol.Add("р.");
      this.CurrencySymbol.Add("RWF");
      this.CurrencySymbol.Add("ر.س.\u200F");
      this.CurrencySymbol.Add("kr");
      this.CurrencySymbol.Add("$");
      this.CurrencySymbol.Add("ل.س.\u200F");
      this.CurrencySymbol.Add("฿");
      this.CurrencySymbol.Add("т.р.");
      this.CurrencySymbol.Add("m.");
      this.CurrencySymbol.Add("د.ت.\u200F");
      this.CurrencySymbol.Add("TL");
      this.CurrencySymbol.Add("TT$");
      this.CurrencySymbol.Add("NT$");
      this.CurrencySymbol.Add("₴");
      this.CurrencySymbol.Add("$");
      this.CurrencySymbol.Add("$U");
      this.CurrencySymbol.Add("so'm");
      this.CurrencySymbol.Add("Bs. F.");
      this.CurrencySymbol.Add("₫");
      this.CurrencySymbol.Add("XOF");
      this.CurrencySymbol.Add("ر.ي.\u200F");
      this.CurrencySymbol.Add("R");
      this.CurrencySymbol.Add("Z$");
    }

    public string getCurrencySymbol(string ISOcurrencySymbol) => this.CurrencySymbol[this.ISOCurrencySymbol.IndexOf(ISOcurrencySymbol)];
  }
}