// Microsoft.WindowsAzure.Messaging.TemplateRegistration

using Microsoft.WindowsAzure.Messaging.Http;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Xml;
using System.Xml.Linq;

namespace Microsoft.WindowsAzure.Messaging
{
  public sealed class TemplateRegistration : Registration
  {
    public const string NotificationType = "X-WindowsPhone-Target";
    public const string NotificationClass = "X-NotificationClass";
    internal const string NamespaceName = "WPNotification";
    internal const string Tile = "token";
    internal const string Toast = "toast";
    internal const string TileClass = "1";
    internal const string ToastClass = "2";
    internal const string RawClass = "3";

    public TemplateRegistration(string channelUri, string bodyTemplate, string templateName)
      : this(channelUri, bodyTemplate, templateName, (IEnumerable<string>) null, (IDictionary<string, string>) null)
    {
    }

    public TemplateRegistration(
      string channelUri,
      string bodyTemplate,
      string templateName,
      IEnumerable<string> tags)
      : this(channelUri, bodyTemplate, templateName, tags, (IDictionary<string, string>) null)
    {
    }

    public TemplateRegistration(
      string channelUri,
      string bodyTemplate,
      string templateName,
      IEnumerable<string> tags,
      IDictionary<string, string> additionalHeaders)
      : base(channelUri, tags)
    {
      if (string.IsNullOrWhiteSpace(templateName))
        throw new ArgumentNullException(nameof (templateName));
      if (string.IsNullOrWhiteSpace(bodyTemplate))
        throw new ArgumentNullException(nameof (bodyTemplate));
      if (templateName.Equals("$Default"))
        throw new ArgumentException("ConflictWithReservedName");

      this.TemplateName = !templateName.Contains(":")
                && !templateName.Contains(";")
                ? templateName : throw new ArgumentException("InvalidTemplateName");
      this.MpnsHeaders = new MpnsHeaderCollection();
      if (additionalHeaders != null)
      {
        foreach (KeyValuePair<string, string> additionalHeader in (IEnumerable<KeyValuePair<string, string>>) additionalHeaders)
          this.MpnsHeaders.Add(additionalHeader.Key, additionalHeader.Value);
      }
      this.BodyTemplate = bodyTemplate;
      this.DetectBodyType();
    }

    internal TemplateRegistration(
      string notificationHubPath,
      string channelUri,
      string bodyTemplate,
      string templateName,
      IEnumerable<string> tags,
      IDictionary<string, string> additionalHeaders)
      : this(channelUri, bodyTemplate, templateName, tags, additionalHeaders)
    {
      this.NotificationHubPath = !string.IsNullOrWhiteSpace(notificationHubPath)
                ? notificationHubPath : 
                throw new ArgumentNullException(nameof (notificationHubPath));
    }

    internal TemplateRegistration(string channelUri)
      : base(channelUri)
    {
      this.MpnsHeaders = new MpnsHeaderCollection();
    }

    internal TemplateRegistration(XElement content)
      : base(content)
    {
      this.MpnsHeaders = content != null ? content.GetElementValue<MpnsHeaderCollection>(
          nameof (MpnsHeaders)) : throw new ArgumentNullException(nameof (content));
      this.BodyTemplate = content.GetElementValueAsString(nameof (BodyTemplate));
      this.TemplateName = content.GetElementValueAsString(nameof (TemplateName));
    }

    public MpnsHeaderCollection MpnsHeaders { get; private set; }

    public string TemplateName { get; set; }

    public string BodyTemplate { get; set; }

    private void DetectBodyType()
    {
      if (this.MpnsHeaders.ContainsKey("X-NotificationClass") && this.MpnsHeaders["X-NotificationClass"].Equals("3", StringComparison.OrdinalIgnoreCase))
        return;
      XElement xelement = (XElement) null;
      try
      {
        foreach (XElement element in XElement.Parse(this.BodyTemplate).Elements())
        {
          if (((XObject) element).NodeType == XmlNodeType.Element)
          {
            xelement = element;
            break;
          }
        }
      }
      catch (Exception ex)
      {
        Debug.WriteLine(ex.Message);
        throw new ArgumentException("NotSupportedXMLFormatAsBodyTemplate");
      }

      if (xelement == null)
        throw new ArgumentException("NotSupportedXMLFormatAsBodyTemplate");

      this.MpnsHeaders.Remove("X-WindowsPhone-Target");
      TemplateRegistration.TemplateRegistrationType registrationType;
           
        if (!string.Equals(xelement.Name.Namespace.NamespaceName, "WPNotification",
            StringComparison.OrdinalIgnoreCase) ||
            !Enum.TryParse<TemplateRegistration.TemplateRegistrationType>(
                xelement.Name.LocalName, true, out registrationType))
        {
            throw new ArgumentException("NotSupportedXMLFormatAsBodyTemplate");
        }

      switch (registrationType)
      {
        case TemplateRegistration.TemplateRegistrationType.Toast:
          this.MpnsHeaders.Add("X-WindowsPhone-Target", "toast");
          this.MpnsHeaders.Add("X-NotificationClass", "2");
          break;
        case TemplateRegistration.TemplateRegistrationType.Tile:
          this.MpnsHeaders.Add("X-WindowsPhone-Target", "token");
          this.MpnsHeaders.Add("X-NotificationClass", "1");
          break;
        default:
          throw new NotSupportedException(registrationType.ToString());
      }
    }

    internal override List<XElement> GetXElements()
    {
      List<XElement> xelements = base.GetXElements();
      if (!string.IsNullOrEmpty(this.BodyTemplate))
        xelements.Add(new XElement(HttpUtilities.serviceBusDef + "BodyTemplate", 
            (object) new XCData(this.BodyTemplate)));
      if (this.MpnsHeaders != null && this.MpnsHeaders.Count > 0)
      {
        XElement xelement = XElement.Parse(
            SerializationHelper.Serialize((object) this.MpnsHeaders, (Type[]) null));

        xelements.Add(new XElement(HttpUtilities.serviceBusDef + "MpnsHeaders",
            (object) xelement.Elements()));
      }
      if (!string.IsNullOrEmpty(this.TemplateName))
        xelements.Add(new XElement(HttpUtilities.serviceBusDef + "TemplateName",
            (object) this.TemplateName));
      return xelements;
    }

        internal override string ToXmlString()
        {
            return ((object)new XElement(HttpUtilities.serviceBusDef 
                + "MpnsTemplateRegistrationDescription",
                (object)this.GetXElements())).ToString();
        }

        internal override string Name
        {
            get
            {
                return this.TemplateName;
            }
        }

        private enum TemplateRegistrationType
        {
          Toast,
          Tile,
        }
  }
}
