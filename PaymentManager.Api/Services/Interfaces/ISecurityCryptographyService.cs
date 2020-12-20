namespace PaymentManager.Api.Services.Interfaces
{
    public interface ISecurityCryptographyService
    {
        string DecryptStringAes(string cipherText);
        string EncryptStringAes(string plainText);
        string DecryptStringRsa(string cipherText);
        string EncryptStringRsa(string plainText);
    }
}