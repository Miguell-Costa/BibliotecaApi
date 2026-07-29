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

//// Services
//var baseUrl = builder.Configuration["AuthenticationApi:BaseUrl"]
//	?? throw new InvalidOperationException();

//builder.Services.AddHttpClient<IPublicKeyService, PublicKeyService>(cliente =>
//{
//	cliente.BaseAddress = new Uri(baseUrl!);

//});

//var serviceProvider = builder.Services.BuildServiceProvider();
//var publicKeyService = serviceProvider.GetRequiredService<IPublicKeyService>();
//var result = await publicKeyService.GetPublicKeyAsync();

//if (!result.IsSuccess)
//{
//	throw new Exception(result.Errors.First());
//}

//var rsa = RSA.Create();

//var publicKey = result.Data;
//Debug.WriteLine(publicKey);
//rsa.ImportFromPem(publicKey!);

// Configuração Jwt
//var jwtSettings = builder.Configuration.GetSection("JwtSettings");

//builder.Services.AddAuthentication(options =>
//{
//	// Quando precisares de descobrir quem é o utilizador, usa o JWT.
//	options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;

//	// Se o utilizador não estiver autenticado, como devo responder?
//	options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

//})
//.AddJwtBearer(options =>
//{
//	options.TokenValidationParameters = new TokenValidationParameters
//	{
//		ValidateIssuer = true,
//		ValidateAudience = true,
//		ValidateLifetime = true,
//		ValidateIssuerSigningKey = true,
//		ValidIssuer = jwtSettings["Issuer"],
//		ValidAudience = jwtSettings["Audience"],
//		IssuerSigningKey = new RsaSecurityKey(rsa),
//		ClockSkew = TimeSpan.Zero
//	};
//});

// Policies
builder.Services.AddAuthorization(options =>
{
	// User 
	options.AddPolicy("User.Read", policy => policy.RequireClaim("Utilizador", "CanRead"));
	options.AddPolicy("User.Update", policy => policy.RequireClaim("Utilizador", "CanUpdate"));
	options.AddPolicy("User.ManageRoles", policy => policy.RequireClaim("Utilizador", "CanManageRoles"));

	// Roles
	options.AddPolicy("Role.Create", policy => policy.RequireClaim("Role", "CanCreate"));
	options.AddPolicy("Role.Read", policy => policy.RequireClaim("Role", "CanRead"));
	options.AddPolicy("Role.Update", policy => policy.RequireClaim("Role", "CanUpdate"));
	options.AddPolicy("Role.Delete", policy => policy.RequireClaim("Role", "CanDelete"));
	options.AddPolicy("Role.ManageClaims", policy => policy.RequireClaim("Role", "CanManageClaims"));

	// Livro
	options.AddPolicy("Livro.Create", policy => policy.RequireClaim("Livro", "CanCreate"));
	options.AddPolicy("Livro.Read", policy => policy.RequireClaim("Livro", "CanRead"));
	options.AddPolicy("Livro.Update", policy => policy.RequireClaim("Livro", "CanUpdate"));
	options.AddPolicy("Livro.Delete", policy => policy.RequireClaim("Livro", "CanDelete"));

	// Autor
	options.AddPolicy("Autor.Create", policy => policy.RequireClaim("Autor", "CanCreate"));
	options.AddPolicy("Autor.Read", policy => policy.RequireClaim("Autor", "CanRead"));
	options.AddPolicy("Autor.Update", policy => policy.RequireClaim("Autor", "CanUpdate"));
	options.AddPolicy("Autor.Delete", policy => policy.RequireClaim("Autor", "CanDelete"));

	// Categoria
	options.AddPolicy("Categoria.Create", policy => policy.RequireClaim("Categoria", "CanCreate"));
	options.AddPolicy("Categoria.Read", policy => policy.RequireClaim("Categoria", "CanRead"));
	options.AddPolicy("Categoria.Update", policy => policy.RequireClaim("Categoria", "CanUpdate"));
	options.AddPolicy("Categoria.Delete", policy => policy.RequireClaim("Categoria", "CanDelete"));


});

// -------- Serviços --------
builder.Services.AddScoped<ILivroRepository, LivroRepository>();
builder.Services.AddScoped<ILivroService, LivroService>();
builder.Services.AddScoped<IAutorRepository, AutorRepository>();
builder.Services.AddScoped<IAutorService, AutorService>();
builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddScoped<ICategoriaService, CategoriaService>();

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
