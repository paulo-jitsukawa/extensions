using System;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;

namespace Jitsukawa.Extensions.StorageFile
{
    public static class StorageFile
    {
        /// <summary>
        /// Equivalente a 'FileIO.WriteBytesAsync(IStorageFile file, byte[] buffer)'.
        /// </summary>
        public static async void WriteBytesAsync(this StorageFile file, byte[] buffer)
        {
            using (var stream = await file.OpenAsync(FileAccessMode.ReadWrite))
            {
                using (DataWriter writer = new DataWriter(stream))
                {
                    writer.WriteBytes(buffer);
                }
            }
        }

        /// <summary>
        /// Retorna o conteúdo de um StorageFile em bytes.
        /// </summary>
        public static async Task<byte[]> ReadBytesAsync(this StorageFile file)
        {
            byte[] fileBytes = null;
            using (IRandomAccessStreamWithContentType stream = await file.OpenReadAsync())
            {
                fileBytes = new byte[stream.Size];
                using (DataReader reader = new DataReader(stream))
                {
                    await reader.LoadAsync((uint)stream.Size);
                    reader.ReadBytes(fileBytes);
                }
            }

            return fileBytes;
        }
    }
}