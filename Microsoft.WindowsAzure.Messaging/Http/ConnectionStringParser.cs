// Microsoft.WindowsAzure.Messaging.Http.ConnectionStringParser

using System;
using System.Collections.Generic;
using System.Globalization;

namespace Microsoft.WindowsAzure.Messaging.Http
{
  internal class ConnectionStringParser
  {
    private string _argumentName;
    private string _value;
    private int _pos;
    private ConnectionStringParser.ParserState _state;

    internal static IEnumerable<KeyValuePair<string, string>> Parse(
      string argumentName,
      string connectionString)
    {
      return new ConnectionStringParser(argumentName, connectionString).Parse();
    }

    private ConnectionStringParser(string argumentName, string value)
    {
      this._argumentName = argumentName;
      this._value = value;
      this._pos = 0;
      this._state = ConnectionStringParser.ParserState.ExpectKey;
    }

    private IEnumerable<KeyValuePair<string, string>> Parse()
    {
      string key = (string) null;
      string value = (string) null;
      while (true)
      {
        this.SkipWhitespaces();
        if (this._pos != this._value.Length || this._state == ConnectionStringParser.ParserState.ExpectValue)
        {
          switch (this._state)
          {
            case ConnectionStringParser.ParserState.ExpectKey:
              key = this.ExtractKey();
              this._state = ConnectionStringParser.ParserState.ExpectAssignment;
              continue;
            case ConnectionStringParser.ParserState.ExpectAssignment:
              this.SkipOperator('=');
              this._state = ConnectionStringParser.ParserState.ExpectValue;
              continue;
            case ConnectionStringParser.ParserState.ExpectValue:
              value = this.ExtractValue();
              this._state = ConnectionStringParser.ParserState.ExpectSeparator;
              yield return new KeyValuePair<string, string>(key, value);
              key = (string) null;
              value = (string) null;
              continue;
            default:
              this.SkipOperator(';');
              this._state = ConnectionStringParser.ParserState.ExpectKey;
              continue;
          }
        }
        else
          break;
      }
      if (this._state == ConnectionStringParser.ParserState.ExpectAssignment)
        throw this.CreateException(this._pos, "ErrorConnectionStringMissingCharacter", (object) "=");
    }

    private Exception CreateException(int position, string errorString, params object[] args)
    {
      errorString = string.Format((IFormatProvider) CultureInfo.InvariantCulture, errorString, args);
      errorString = string.Format((IFormatProvider) CultureInfo.InvariantCulture, 
          "ErrorParsingConnectionString", (object) errorString, (object) this._pos);
      errorString = string.Format((IFormatProvider) CultureInfo.InvariantCulture, 
          "ErrorInvalidConnectionString", (object) this._argumentName, (object) errorString);
      return (Exception) new ArgumentException(errorString);
    }

    private void SkipWhitespaces()
    {
      while (this._pos < this._value.Length && char.IsWhiteSpace(this._value[this._pos]))
        ++this._pos;
    }

    private string ExtractKey()
    {
      int pos = this._pos;
      char quote = this._value[this._pos++];
      string str;
      switch (quote)
      {
        case '"':
        case '\'':
          str = this.ExtractString(quote);
          break;
        case ';':
        case '=':
          throw this.CreateException(pos, "ErrorConnectionStringMissingKey");
        default:
          while (this._pos < this._value.Length && this._value[this._pos] != '=')
            ++this._pos;
          str = this._value.Substring(pos, this._pos - pos).TrimEnd();
          break;
      }
      return str.Length != 0 ? str : throw this.CreateException(pos, "ErrorConnectionStringEmptyKey");
    }

    private string ExtractString(char quote)
    {
      int pos = this._pos;
      while (this._pos < this._value.Length && (int) this._value[this._pos] != (int) quote)
        ++this._pos;
      if (this._pos == this._value.Length)
        throw this.CreateException(this._pos, "ErrorConnectionStringMissingCharacter", (object) quote);
      return this._value.Substring(pos, this._pos++ - pos);
    }

    private void SkipOperator(char operatorChar)
    {
      if ((int) this._value[this._pos] != (int) operatorChar)
        throw this.CreateException(this._pos, "ErrorConnectionStringMissingCharacter", (object) operatorChar);
      ++this._pos;
    }

    private string ExtractValue()
    {
      string str = string.Empty;
      if (this._pos < this._value.Length)
      {
        char quote = this._value[this._pos];
        switch (quote)
        {
          case '"':
          case '\'':
            ++this._pos;
            str = this.ExtractString(quote);
            break;
          default:
            int pos = this._pos;
            bool flag = false;
            while (this._pos < this._value.Length && !flag)
            {
              if (this._value[this._pos] == ';')
              {
                if (this.IsStartWithKnownKey())
                  flag = true;
                else
                  ++this._pos;
              }
              else
                ++this._pos;
            }
            str = this._value.Substring(pos, this._pos - pos).TrimEnd();
            break;
        }
      }
      return str;
    }

    private bool IsStartWithKnownKey() => this._value.Length <= this._pos + 1 || this._value.Substring(this._pos + 1).StartsWith("Endpoint", StringComparison.OrdinalIgnoreCase) || this._value.Substring(this._pos + 1).StartsWith("StsEndpoint", StringComparison.OrdinalIgnoreCase) || this._value.Substring(this._pos + 1).StartsWith("SharedSecretIssuer", StringComparison.OrdinalIgnoreCase) || this._value.Substring(this._pos + 1).StartsWith("SharedSecretValue", StringComparison.OrdinalIgnoreCase) || this._value.Substring(this._pos + 1).StartsWith("SharedAccessKeyName", StringComparison.OrdinalIgnoreCase) || this._value.Substring(this._pos + 1).StartsWith("SharedAccessKey", StringComparison.OrdinalIgnoreCase);

    private enum ParserState
    {
      ExpectKey,
      ExpectAssignment,
      ExpectValue,
      ExpectSeparator,
    }
  }
}
