// Microsoft.WindowsAzure.Messaging.Http.SerializationHelper

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Xml;
using System.Xml.Linq;

namespace Microsoft.WindowsAzure.Messaging.Http
{
  internal static class SerializationHelper
  {
    internal static IEnumerable<T> DeserializeCollection<T>(
      XElement content,
      IEnumerable<Type> extraTypes)
    {
      return Enumerable.Select<XElement, T>(Enumerable.Where<XElement>(content.Elements(),
          (Func<XElement, bool>) (e => e.Name.LocalName.Equals("entry"))), 
          (Func<XElement, T>) (entry => SerializationHelper.DeserializeItem<T>(entry, extraTypes)));
    }

    internal static T DeserializeItem<T>(XElement content, IEnumerable<Type> extraTypes)
    {
      if (typeof (T) != typeof (Registration) && typeof (T) != typeof (TemplateRegistration))
        throw new NotSupportedException();

      //RnD
      return default;//(T) SerializationHelper.DeserializeRegistration(content);
    }

    private static Registration DeserializeRegistration(XElement entry)
    {
      if (!string.Equals(entry.Name.LocalName, nameof (entry), StringComparison.Ordinal))
        return (Registration) null;
      XElement content = Enumerable.Where<XElement>(entry.Elements(), 
          (Func<XElement, bool>) (i => i.Name.LocalName.Equals("content", StringComparison.OrdinalIgnoreCase))).Single<XElement>().Elements().Single<XElement>();
      if (content.Name.LocalName.Equals("MpnsRegistrationDescription",
          StringComparison.OrdinalIgnoreCase))
        return new Registration(content);
      if (content.Name.LocalName.Equals("MpnsTemplateRegistrationDescription",
          StringComparison.OrdinalIgnoreCase))
        return (Registration) new TemplateRegistration(content);
      throw new NotSupportedException();
    }

        internal static string GetElementValueAsString(this XElement content, string name)
        {
            return Enumerable.FirstOrDefault<XElement>(content.Elements(), (Func<XElement, bool>)(i => i.Name.LocalName.Equals(name, StringComparison.OrdinalIgnoreCase)))?.Value;
        }

        internal static DateTime? GetElementValueAsDateTime(this XElement content, string name)
    {
      XElement xelement = Enumerable.FirstOrDefault<XElement>(content.Elements(), (Func<XElement, bool>) (i => i.Name.LocalName.Equals(name, StringComparison.OrdinalIgnoreCase)));
      DateTime result;
      return xelement != null && DateTime.TryParse(xelement.Value, out result) ? new DateTime?(result) : new DateTime?();
    }

    internal static T GetElementValue<T>(this XElement content, string name)
    {
      XElement xelement = Enumerable.FirstOrDefault<XElement>(content.Elements(), (Func<XElement, bool>) (i => i.Name.LocalName.Equals(name, StringComparison.OrdinalIgnoreCase)));
      if (xelement == null)
        return default (T);
      string s = ((object) xelement).ToString();
      using (StringReader input = new StringReader(s))
        return (T) new DataContractSerializer(typeof (T)).ReadObject(XmlReader.Create((TextReader) input));
    }

    internal static string Serialize(object item, params Type[] knownTypes)
    {
      if (item is Registration registration)
        return registration.ToXmlString();
      using (MemoryStream memoryStream = new MemoryStream())
      {
        new DataContractSerializer(item.GetType(), (IEnumerable<Type>) knownTypes).WriteObject((Stream) memoryStream, item);
        memoryStream.Flush();
        memoryStream.Seek(0L, SeekOrigin.Begin);
        return new StreamReader((Stream) memoryStream).ReadToEnd();
      }
    }

    internal static string JsonSerialize(object item, params Type[] knownTypes)
    {
      using (MemoryStream memoryStream = new MemoryStream())
      {
        new DataContractJsonSerializer(item.GetType(), (IEnumerable<Type>) knownTypes).WriteObject((Stream) memoryStream, item);
        memoryStream.Flush();
        memoryStream.Seek(0L, SeekOrigin.Begin);
        return new StreamReader((Stream) memoryStream).ReadToEnd();
      }
    }
  }
}
