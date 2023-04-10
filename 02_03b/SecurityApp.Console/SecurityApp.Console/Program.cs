using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.DependencyInjection;

// 1. Add Data Protection Services
var serviceCollection = new ServiceCollection();
serviceCollection.AddDataProtection();
var services = serviceCollection.BuildServiceProvider();

// 2. Create an instance of IDataProtector interface
var dataProtectionProvider = services.GetService<IDataProtectionProvider>();
var dataProtector = dataProtectionProvider.CreateProtector("FirstExample");

// 3. Protect and Unprotect a payload
string title = "Welcome to this course!";
Console.WriteLine($"Original value = {title}");

var protectedTitle = dataProtector.Protect(title);
Console.WriteLine($"Protected value = {protectedTitle}");

var unprotectedTitle = dataProtector.Unprotect(protectedTitle);
Console.WriteLine($"UnProtected value = {unprotectedTitle}");

Console.ReadLine();