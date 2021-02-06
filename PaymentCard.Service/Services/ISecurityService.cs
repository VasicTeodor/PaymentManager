using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Service.Services
{
    public interface ISecurityService
    {
        string DecryptStringAes(string cipherText);
        string EncryptStringAes(string plainText);
    }
}
