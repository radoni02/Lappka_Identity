using Application;
using Convey;
using Infrastructure.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Infrastructure;
using System.Text;
using Application.Auth.Commands;
using Api.Extensions;
using FluentValidation.AspNetCore;
using System.Reflection;
using Infrastructure.GrpcClient;
using Infrastructure.Exceptions;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;
// Add services to the container.
builder.Services.AddInfrastructure(configuration);
builder.Services.AddControllers().AddFluentValidation(opt =>
{
    opt.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
});
builder.Services.AddCredentialsConfig();
builder.Services.AddConvey()
    .AddApplication();

var grpcOptions = builder.Configuration.GetProjectOptions<GrpcSettings>("gRPC");
builder.Services.AddSingleton(grpcOptions);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddAuth(configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerA();
builder.Services.AddSwaggerGen();
builder.Services.AddGrpc();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();
app.UseMiddleware<ExceptionMiddleware>();
app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();

app.MapControllers();

app.Run();
