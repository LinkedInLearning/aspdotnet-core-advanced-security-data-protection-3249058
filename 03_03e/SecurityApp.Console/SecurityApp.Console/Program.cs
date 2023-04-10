using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Win32;
using System.Text;

// 1. Add Data Protection Services
var serviceCollection = new ServiceCollection();


//serviceCollection.AddDataProtection()
//    .SetDefaultKeyLifetime(TimeSpan.FromDays(10))
//    .PersistKeysToFileSystem(new DirectoryInfo(@"c:\temp-encryption-keys"))
//    .ProtectKeysWithDpapi();

var registryKey = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Keys\LinkedInLearning");

serviceCollection.AddDataProtection()
    .PersistKeysToRegistry(registryKey);


var services = serviceCollection.BuildServiceProvider();

var dataProtectionProvider = services.GetService<IDataProtectionProvider>();
var dataProtector = dataProtectionProvider.CreateProtector("Lifetime.Test1");

var dataProtected = dataProtector.Protect("Simple string");

//var keyManagerService = services.GetService<IKeyManager>();
//var allKeys = keyManagerService.GetAllKeys();

//foreach (var key in allKeys)
//{
//    Console.WriteLine($"Key Id: {key.KeyId}, Created: {key.CreationDate}, Expiration: {key.ExpirationDate}, Is Revoked = {key.IsRevoked}");
//}

//keyManagerService.RevokeAllKeys(DateTimeOffset.Now);

//var allKeysAfterRevoked = keyManagerService.GetAllKeys();
//foreach (var key in allKeysAfterRevoked)
//{
//    Console.WriteLine($"Key Id: {key.KeyId}, Created: {key.CreationDate}, Expiration: {key.ExpirationDate}, Is Revoked = {key.IsRevoked}");
//}

//string title = "Welcome to this course!";
//Console.WriteLine($"Original value = {title}");

//byte[] titleByte = Encoding.UTF8.GetBytes(title);
//var protectedTitle = dataProtector.Protect(titleByte);

//Console.WriteLine($"Protected value = {Convert.ToBase64String(protectedTitle)}");

//var unprotectedTitle = dataProtector.Unprotect(protectedTitle);
//Console.WriteLine($"UnProtected value = {Encoding.UTF8.GetString(unprotectedTitle)}");

////Get a reference to KeyManager
//var keyManagerService = services.GetService<IKeyManager>();
//keyManagerService.RevokeAllKeys(DateTimeOffset.Now);

//try
//{
//    var tryUnprotect = dataProtector.Unprotect(protectedTitle);
//}
//catch (Exception ex)
//{
//    Console.WriteLine(ex.Message);
//}

//try
//{
//    IPersistedDataProtector persistedDataProtector = dataProtector as IPersistedDataProtector;
//    if (persistedDataProtector == null)
//        throw new Exception("Could not convert...");

//    bool requiresMigration, wasRevoked;
//    var unprotectedPayload = persistedDataProtector.DangerousUnprotect(
//        protectedData: protectedTitle,
//        ignoreRevocationErrors: true,
//        requiresMigration: out requiresMigration,
//        wasRevoked: out wasRevoked);

//    Console.WriteLine($"requiresMigration = {requiresMigration}");
//    Console.WriteLine($"wasRevoked = {wasRevoked}");

//    string value = Encoding.UTF8.GetString(unprotectedPayload);

//    Console.WriteLine($"UnProtected value = {Encoding.UTF8.GetString(unprotectedTitle)}");


//}
//catch (Exception ex)
//{
//    Console.WriteLine(ex.Message);
//}


//Console.WriteLine("---------------------------- Password Hashing ------------------------------");

//string password = "Passw0rd!@";

////Generate a Random Salt
//byte[] salt = new byte[128 / 8];
//using(var rng = System.Security.Cryptography.RandomNumberGenerator.Create())
//{
//    rng.GetBytes(salt);
//}

//string hashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
//    password: password,
//    salt: salt,
//    prf: KeyDerivationPrf.HMACSHA1,
//    iterationCount: 10000,
//    numBytesRequested: 256/8));

//Console.WriteLine($"Password - {password}");
//Console.WriteLine($"Hashed Password - {hashedPassword}");

//Console.WriteLine("-------------------- Data Protector - Set Lifetime -------------------------");

//var _dataProtector = dataProtectionProvider.CreateProtector("FirstExample.WithLifeTime");
//var _timeLimitedDataProtector = _dataProtector.ToTimeLimitedDataProtector();

//string _title = "Welcome to this course!";
//Console.WriteLine($"Original value = {_title}");

//var _protectedTitle = _timeLimitedDataProtector.Protect(title, lifetime: TimeSpan.FromSeconds(10));
//Console.WriteLine($"Protected value = {_protectedTitle}");

//var _unprotectedTitle = _timeLimitedDataProtector.Unprotect(_protectedTitle);
//Console.WriteLine($"UnProtected value = {_unprotectedTitle}");

//Console.WriteLine("Waiting 11 seconds...");
//Thread.Sleep(11000);
//_timeLimitedDataProtector.Unprotect(_unprotectedTitle);



Console.ReadLine();