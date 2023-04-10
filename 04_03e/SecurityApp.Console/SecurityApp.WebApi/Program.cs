using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.DataProtection.Repositories;
using Microsoft.Extensions.Options;
using SecurityApp.WebApi.Algorithms;
using SecurityApp.WebApi.Xml;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Register the custom authenticated encryptor factory
//builder.Services.AddSingleton<IAuthenticatedEncryptorFactory, CustomAuthenticatedEncryptorFactory>();

builder.Services.AddSingleton<IXmlRepository, InMemoryXmlRepository>();

builder.Services.AddDataProtection()
    .Services.AddSingleton<IConfigureOptions<KeyManagementOptions>>(options =>
    {
        var xmlRepository = options.GetRequiredService<IXmlRepository>();
        return new ConfigureOptions<KeyManagementOptions>(opt => opt.XmlRepository = xmlRepository);
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
