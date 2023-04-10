using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using System.Security.Cryptography;

namespace SecurityApp.WebApi.Algorithms
{
    public class CustomAuthenticatedEncryptor : IAuthenticatedEncryptor
    {
        private readonly SymmetricAlgorithm _symmetricAlgorithm;
        public CustomAuthenticatedEncryptor(SymmetricAlgorithm symmetricAlgorithm)
        {
            _symmetricAlgorithm = symmetricAlgorithm;  
        }

        public byte[] Decrypt(ArraySegment<byte> ciphertext, ArraySegment<byte> additionalAuthenticatedData)
        {
            using var decryptor = _symmetricAlgorithm.CreateDecryptor(_symmetricAlgorithm.Key, _symmetricAlgorithm.IV);
            using var ms = new MemoryStream(ciphertext.Array, ciphertext.Offset, ciphertext.Count);
            using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
            using var br = new BinaryReader(cs);

            byte[] plaintext = br.ReadBytes(ciphertext.Count);
            return plaintext;
        }

        public byte[] Encrypt(ArraySegment<byte> plaintext, ArraySegment<byte> additionalAuthenticatedData)
        {
            using var encryptor = _symmetricAlgorithm.CreateEncryptor(_symmetricAlgorithm.Key, _symmetricAlgorithm.IV);
            using var ms = new MemoryStream();
            using var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write);
            using var bw = new BinaryWriter(cs);

            bw.Write(plaintext.Array, plaintext.Offset, plaintext.Count);
            bw.Flush();
            cs.FlushFinalBlock();

            byte[] ciphertext = ms.ToArray();
            return ciphertext;
        }
    }
}
