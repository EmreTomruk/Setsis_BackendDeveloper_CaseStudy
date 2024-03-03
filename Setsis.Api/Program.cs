using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using MediatR;

using Setsis.Api.Extensions;
using Setsis.Core.Configurations;
using Setsis.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;


builder.Services.RegisterMappers();
builder.Services.RegisterDbContext(builder.Configuration);
builder.Services.AddIdentity();
builder.Services.RegisterServices();
builder.Services.AddMediatR(typeof(SetsisDbContext));

builder.Services.Configure<CustomTokenOption>(configuration.GetSection("TokenOption"));

// Adding Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options => // Adding Jwt Bearer
{
    var tokenOptions = configuration.GetSection("TokenOption").Get<CustomTokenOption>();

    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ClockSkew = TimeSpan.Zero,

        ValidAudience = tokenOptions.ValidAudience,
        ValidIssuer = tokenOptions.ValidIssuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOptions.Secret))
    };
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
