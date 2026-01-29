using Microsoft.IdentityModel.Tokens;
using System.IO.Compression;
using System.Text;

namespace Protenacity.Cake.Web.Core.Cryptography;

internal class CryptographyService : ICryptographyService
{
    private const string Password = "8aab8f16-05da-4d3d-99b7-ccc0246fc804";

    public string Zip(string text)
    {
        byte[] compressedBytes;
        using (var uncompressedStream = new MemoryStream(Encoding.UTF8.GetBytes(text)))
        {
            using (var compressedStream = new MemoryStream())
            {
                using (var compressorStream = new DeflateStream(compressedStream, CompressionLevel.Fastest, true))
                {
                    uncompressedStream.CopyTo(compressorStream);
                }
                compressedBytes = compressedStream.ToArray();
            }
        }

        return Base64UrlEncoder.Encode(compressedBytes);
    }

    public string Unzip(string compressedString)
    {
        byte[] decompressedBytes;
        using (var compressedStream = new MemoryStream(Base64UrlEncoder.DecodeBytes(compressedString)))
        {
            using (var decompressorStream = new DeflateStream(compressedStream, CompressionMode.Decompress))
            {
                using (var decompressedStream = new MemoryStream())
                {
                    decompressorStream.CopyTo(decompressedStream);
                    decompressedBytes = decompressedStream.ToArray();
                }
            }
        }
        return Encoding.UTF8.GetString(decompressedBytes);
    }
}