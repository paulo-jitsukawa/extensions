using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Windows.Storage;

namespace Jitsukawa.Extensions.Serializer
{
    public class Serializer
    {
        public string XmlSerialize<T>(this T obj)
        {
            var xml = new XmlSerializer(typeof(T));

            using (var writer = new StringWriter())
            {
                xml.Serialize(writer, obj);
                return writer.ToString();
            }
        }

        public T XmlDeserialize<T>(this string content)
        {
            var xml = new XmlSerializer(typeof(T));

            using (var reader = new StringReader(content))
                return (T)xml.Deserialize(reader);
        }

        public T XmlDeserialize<T>(this byte[] buffer, Encoding encoding)
        {
            var xml = new XmlSerializer(typeof(T));

            using (var reader = new StringReader(encoding.GetString(buffer, 0, buffer.Length)))
                return (T)xml.Deserialize(reader);
        }

        /// <summary>
        /// Serializa um objeto em um arquivo no WinRT.
        /// Exemplo:  user.Serialize(await ApplicationData.Current.LocalFolder.CreateFileAsync("Fulano.xml", CreationCollisionOption.ReplaceExisting));
        /// </summary>
        /// <param name="file">Utilizar 'CreationCollisionOption.ReplaceExisting' ao instanciar este parâmetro para evitar problemas.</param>
        public async void Serialize<T>(this T obj, StorageFile file)
        {
            var xml = new XmlSerializer(typeof(T));

            using (var writer = await file.OpenStreamForWriteAsync())
                xml.Serialize(writer, obj);
        }

        /// <summary>
        /// Deserializa um objeto de um arquivo no WinRT.
        /// Exemplo:  var user = (await ApplicationData.Current.LocalFolder.GetFileAsync("Fulano.xml")).Deserialize().Result;
        /// </summary>
        /// <param name="file">Objeto que deve ser serializado no arquivo.</param>
        /// <returns>Utilizar '.Result' para obter o objeto deserializado.</returns>
        public async Task<T> Deserialize<T>(this StorageFile file)
        {
            var xml = new XmlSerializer(typeof(T));

            using (var reader = await file.OpenStreamForReadAsync())
                return (T)xml.Deserialize(reader);
        }
    }
}