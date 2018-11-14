using System.IO;
using System.IO.Compression;
using System.Text;

namespace TextEditorBel
{
    public class Archive
    {
        public static byte[] CompressData(byte[] data)
        {
            byte[] compressData = null;
            //поток для чтения исходных данных
            using (MemoryStream sourceStream = new MemoryStream(data))
            {
                // поток для записи сжатых данных
                using (MemoryStream targetStream = new MemoryStream())
                {
                    // поток архивации
                    using (GZipStream compressionStream = new GZipStream(targetStream, CompressionMode.Compress))
                    {
                        sourceStream.CopyTo(compressionStream); // копируем байты из одного потока в другой                                                                
                    }
                    compressData = targetStream.ToArray();
                    object r = targetStream.ToArray();
                }
            }
            return compressData;
        }

        public static string OutPutDecompressData(byte[] compressData)
        {
            string text;
            using (MemoryStream sourceStream = new MemoryStream(compressData))
            {
                using (MemoryStream targetStream = new MemoryStream())
                {
                    using (GZipStream decompressionStream = new GZipStream(sourceStream, CompressionMode.Decompress))
                    {
                        decompressionStream.CopyTo(targetStream);
                    }
                     text = Encoding.Unicode.GetString(targetStream.ToArray());                    
                }
            }
            return text;
        }
    }
}
