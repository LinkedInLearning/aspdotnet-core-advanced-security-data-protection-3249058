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
Console.WriteLine($"Salt - {salt}");

Console.WriteLine("----------------------------------------------------------");


Console.ReadLine();