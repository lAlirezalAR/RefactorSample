using Contact.Api.Configuration;
using Contact.Api.Swagger;
using Contact.Application.Configurations;
using Contact.Infrastructure.Configuration;
using FluentValidation;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Utilities.Framework;
using Utilities.Framework.Config;

var builder = WebApplication.CreateBuilder(args);

ConfigurationManager configuration = builder.Configuration;
builder.Services.AddControllers();
builder.Services.AddCors();
builder.Services.AddJwtAuthentication(configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCustomVersioningSwagger();
builder.Services.AddApplicationServices(configuration);
builder.Services.AddPersistance(configuration);
builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient();
builder.Services.AddScoped<IClaimHelper, ClaimHelper>();
builder.Services.AddJaeger(builder.Configuration);
builder.Services.Configure<UrlsConfig>(configuration.GetSection("urls"));
builder.Services.TryAddTransient<IValidatorFactory, ServiceProviderValidatorFactory>();
builder.Services.AddFluentValidationRulesToSwagger();
var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseDeveloperExceptionPage();
// }
// else
// {
//    app.UseCustomExceptionHandler();
//}

app.UseCustomExceptionHandler();
app.InitializeDatabase();

app.UseCustomSwagger();

app.UseCors(policy =>
{
    policy.AllowAnyOrigin();
    policy.AllowAnyMethod();
    policy.AllowAnyHeader();
});

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
public partial class Program { }