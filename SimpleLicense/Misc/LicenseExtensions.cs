using ijat.my.SimpleLicense.Models;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace ijat.my.Misc
{
    class LicenseExtensions
    {
        public static string LicenseToXmlString(MyLicense license)
        {
            XmlSerializer xsSubmit = new XmlSerializer(typeof(MyLicense));
            using (var sww = new StringWriter())
            {
                using (XmlWriter writer = XmlWriter.Create(sww))
                {
                    xsSubmit.Serialize(writer, license);
                    return sww.ToString();
                }
            }
        }

        public static MyLicense XmlStringToLicense(string xmlString)
        {
            XmlSerializer xsSubmit = new XmlSerializer(typeof(MyLicense));
            StringReader stringReader = new StringReader(xmlString);
            return (MyLicense) xsSubmit.Deserialize(stringReader);
        }
    }
}
