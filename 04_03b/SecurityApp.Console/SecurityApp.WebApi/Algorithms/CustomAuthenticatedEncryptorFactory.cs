using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System.Security.Cryptography;

namespace SecurityApp.WebApi.Algorithms
{
    public class CustomAuthenticatedEncryptorFactory : IAuthenticatedEncryptorFactory
    {
        public IAuthenticatedEncryptor? CreateEncryptorInstance(IKey key)
        {
            //Some Code for the Symmetric Algorithm

            //var symmetricAlgorithm = new SymmetricAlgorithm()
            //{
            //    Key = new byte[],
            //    IV = new byte[] {}
            //}

            return new CustomAuthenticatedEncryptor(null);
        }
    }
}
