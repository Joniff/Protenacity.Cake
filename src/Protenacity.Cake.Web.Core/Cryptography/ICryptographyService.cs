namespace Protenacity.Cake.Web.Core.Cryptography;

public interface ICryptographyService
{
    string Zip(string text);

    string Unzip(string compressedString);

}