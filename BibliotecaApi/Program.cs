using System.Diagnostics;
using System.Security.Cryptography;
using BibliotecaApi.Authentication;
using BibliotecaApi.Data;
using BibliotecaApi.Interfaces;
using BibliotecaApi.Interfaces.IRepositories;
using BibliotecaApi.Interfaces.IServices;
using BibliotecaApi.Repository;
using BibliotecaApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Configuração base de dados
builder.Services.AddDbContext<AppDbContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
	sqlOptions => sqlOptions.EnableRetryOnFailure(
			maxRetryCount: 5,
			maxRetryDelay: TimeSpan.FromSeconds(10),
			errorNumbersToAdd: null)));

// Services
var baseUrl = builder.Configuration["AuthenticationApi:BaseUrl"]
	?? throw new InvalidOperationException();

builder.Services.AddHttpClient<IPublicKeyService, PublicKeyService>(cliente =>
{
	cliente.BaseAddress = new Uri(baseUrl!);

});

var serviceProvider = builder.Services.BuildServiceProvider();
var publicKeyService = serviceProvider.GetRequiredService<IPublicKeyService>();
var result = await publicKeyService.GetPublicKeyAsync();

if (!result.IsSuccess)
{
	throw new Exception(result.Errors.First());
}

var rsa = RSA.Create();

var publicKey = result.Data;
Debug.WriteLine(publicKey);
rsa.ImportFromPem(publicKey!);

// Configuração Jwt
var jwtSettings = builder.Configuration.GetSection("JwtSettings");

builder.Services.AddAuthentication(options =>
{
	// Quando precisares de descobrir quem é o utilizador, usa o JWT.
	options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;

	// Se o utilizador não estiver autenticado, como devo responder?
	options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

})
.AddJwtBearer(options =>
{
	options.TokenValidationParameters = new TokenValidationParameters
	{
		ValidateIssuer = true,
		ValidateAudience = true,
		ValidateLifetime = true,
		ValidateIssuerSigningKey = true,
		ValidIssuer = jwtSettings["Issuer"],
		ValidAudience = jwtSettings["Audience"],
		IssuerSigningKey = new RsaSecurityKey(rsa),
		ClockSkew = TimeSpan.Zero
	};
});

// -------- Serviços --------
builder.Services.AddScoped<ILivroRepository, LivroRepository>();
builder.Services.AddScoped<ILivroService, LivroService>();

builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
	options.SwaggerDoc("v1", new OpenApiInfo
	{
		Title = "Biblioteca API",
		Version = "v1",
		Description = "API para gestão de livros"
	});

	options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
	{
		Name = "Authorization",
		Type = SecuritySchemeType.Http,
		Scheme = "Bearer",
		BearerFormat = "JWT",
		In = ParameterLocation.Header,
		Description = "Introduz o token JWT no campo abaixo."
	});

	options.AddSecurityRequirement(new OpenApiSecurityRequirement
	{
		{
			new OpenApiSecurityScheme
			{
				Reference = new OpenApiReference
				{
					Type = ReferenceType.SecurityScheme,
					Id = "Bearer"
				}
			},
			Array.Empty<string>()
		}
	});
});

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
