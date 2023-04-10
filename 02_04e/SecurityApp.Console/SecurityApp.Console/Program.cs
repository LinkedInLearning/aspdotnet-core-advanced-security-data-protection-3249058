using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.DependencyInjection;

// 1. Add Data Protection Services
var serviceCollection = new ServiceCollection();
serviceCollection.AddDataProtection();
var services = serviceCollection.BuildServiceProvider();

// 2. Create an instance of IDataProtector interface
var dataProtectionProvider = services.GetService<IDataProtectionProvider>();
var dataProtector = dataProtectionProvider.CreateProtector("FirstExample");

Console.WriteLine("----------------------------Data Protector ------------------------------");

// 3. Protect and Unprotect a payload
string title = "Welcome to this course!";
Console.WriteLine($"Original value = {title}");

var protectedTitle = dataProtector.Protect(title);
Console.WriteLine($"Protected value = {protectedTitle}");

var unprotectedTitle = dataProtector.Unprotect(protectedTitle);
Console.WriteLine($"UnProtected value = {unprotectedTitle}");

Console.WriteLine("---------------------------- Password Hashing ------------------------------");

string password = "Passw0rd!@";

//Generate a Random Salt
byte[] salt = new byte[128 / 8];
using(var rng = System.Security.Cryptography.RandomNumberGenerator.Create())
{
    rng.GetBytes(salt);
}

string hashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
    password: password,
    salt: salt,
    prf: KeyDerivationPrf.HMACSHA1,
    iterationCount: 10000,
    numBytesRequested: 256/8));

Console.WriteLine($"Password - {password}");
Console.WriteLine($"Hashed Password - {hashedPassword}");

Console.WriteLine("-------------------- Data Protector - Set Lifetime -------------------------");

var _dataProtector = dataProtectionProvider.CreateProtector("FirstExample.WithLifeTime");
var _timeLimitedDataProtector = _dataProtector.ToTimeLimitedDataProtector();

string _title = "Welcome to this course!";
Console.WriteLine($"Original value = {_title}");

var _protectedTitle = _timeLimitedDataProtector.Protect(title, lifetime: TimeSpan.FromSeconds(10));
Console.WriteLine($"Protected value = {_protectedTitle}");

var _unprotectedTitle = _timeLimitedDataProtector.Unprotect(_protectedTitle);
Console.WriteLine($"UnProtected value = {_unprotectedTitle}");

Console.WriteLine("Waiting 11 seconds...");
Thread.Sleep(11000);
_timeLimitedDataProtector.Unprotect(_unprotectedTitle);



Console.ReadLine();