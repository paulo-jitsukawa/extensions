using System.IO;
using System.IO.Compression;
using System.Text;

namespace Jitsukawa.Extensions.Compression
{
    public static class Compression
    {
        public static MemoryStream GZipCompress(this Stream stream)
        {
            using (var ms = new MemoryStream())
            {
                using (var gs = new GZipStream(ms, CompressionMode.Compress))
                    stream.CopyTo(gs);

                return ms;
            }
        }

        public static byte[] GZipCompress(this byte[] content)
        {
            using (var stream = new MemoryStream(content))
                return GZipCompress(stream).ToArray();
        }

        public static byte[] GZipCompress(this string text, Encoding encode)
        {
            using (var stream = new MemoryStream(encode.GetBytes(text)))
                return GZipCompress(stream).ToArray();
        }

        public static MemoryStream GZipDecompress(this Stream stream)
        {
            using (var gs = new GZipStream(stream, CompressionMode.Decompress))
            using (var ms = new MemoryStream())
            {
                gs.CopyTo(ms);
                return ms;
            }
        }

        public static byte[] GZipDecompress(this byte[] content)
        {
            using (var stream = new MemoryStream(content))
                return GZipDecompress(stream).ToArray();
        }

        public static string GZipDecompress(this byte[] content, Encoding encode)
        {
            using (var stream = new MemoryStream(content))
            {
                var array = GZipDecompress(stream).ToArray();
                return encode.GetString(array, 0, array.Length);
            }
        }
    }
}
