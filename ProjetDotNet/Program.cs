using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.IdentityModel.Tokens;
using NuGet.Protocol;
using ProjetDotNet.Data.Context;
using ProjetDotNet.Models;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAuthorization();


builder.Services.AddAuthentication(options =>
{
	options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
	o.TokenValidationParameters = new TokenValidationParameters
	{
		ValidIssuer = builder.Configuration["Jwt:Issuer"],
		ValidAudience = builder.Configuration["Jwt:Audience"],
		IssuerSigningKey = new SymmetricSecurityKey
		(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
		ValidateIssuer = true,
		ValidateAudience = true,
		ValidateLifetime = false,
		ValidateIssuerSigningKey = true
	};
});
var app = builder.Build();

app.UseHttpsRedirection();

if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	app.UseHsts();
}

app.MapGet("/salma", (string msg) => "Hello" + msg);
app.MapGet("/security/getMessage", () => "Hello World!").RequireAuthorization();
app.MapPost("/security/createToken", (string email, string password) =>
{
	UnitOfWork unitOfWork = new UnitOfWork(ChatAppContext.Instance);
	Debug.WriteLine("hello");
	User U = unitOfWork.Users.Find(x => x.Email.ToLower() == "kcritzen5@epa.gov").FirstOrDefault();
	if (U is not null && email == U.Email && password == U.Password)
	{
		var issuer = builder.Configuration["Jwt:Issuer"];
		var audience = builder.Configuration["Jwt:Audience"];
		var key = Encoding.ASCII.GetBytes
		(builder.Configuration["Jwt:Key"]);
		var tokenDescriptor = new SecurityTokenDescriptor
		{
			Subject = new ClaimsIdentity(new[]
			{
				new Claim("Id", Guid.NewGuid().ToString()),
				new Claim(JwtRegisteredClaimNames.Sub, U.Name),
				new Claim(JwtRegisteredClaimNames.Email, U.Email),
				new Claim(JwtRegisteredClaimNames.Jti,
				Guid.NewGuid().ToString())
			 }),
			Expires = DateTime.UtcNow.AddMinutes(5),
			Issuer = issuer,
			Audience = audience,
			SigningCredentials = new SigningCredentials
			(new SymmetricSecurityKey(key),
			SecurityAlgorithms.HmacSha512Signature)
		};
		var tokenHandler = new JwtSecurityTokenHandler();
		var token = tokenHandler.CreateToken(tokenDescriptor);
		var jwtToken = tokenHandler.WriteToken(token);
		var stringToken = tokenHandler.WriteToken(token);
		return Results.Ok(stringToken);
	}

	return Results.Unauthorized();
});

app.UseStaticFiles();
app.UseAuthentication();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=connection}/{action=Index}/{id?}");

app.Run();
