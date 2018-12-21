using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace Jitsukawa.Extensions.Serializer
{
    public static class Serializer
    {
        public static string XmlSerialize<T>(this T obj)
        {
            var xml = new XmlSerializer(typeof(T));
            using (var writer = new StringWriter())
            {
                xml.Serialize(writer, obj);
                return writer.ToString();
            }
        }

        public static T XmlDeserialize<T>(this string content)
        {
            var xml = new XmlSerializer(typeof(T));
            using (var reader = new StringReader(content))
            {
                return (T)xml.Deserialize(reader);
            }
        }

        public static T XmlDeserialize<T>(this byte[] buffer, Encoding encoding)
        {
            var xml = new XmlSerializer(typeof(T));

            using (var reader = new StringReader(encoding.GetString(buffer, 0, buffer.Length)))
                return (T)xml.Deserialize(reader);
        }
    }
}
