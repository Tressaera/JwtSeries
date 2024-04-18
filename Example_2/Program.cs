using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var issur=builder. Configuration["JwtConfig: Issuer"] ;
var audience=builder.Configuration["JwtConfig:Audience"] ;
var signingKey=builder.Configuration["JwtConfig:SigningKey"] ;
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options=>{
    options.TokenValidationParameters=new TokenValidationParameters
{
    ValidateIssuer = true,
    ValidateAudience=true,
    ValidateLifetime=true,
    ValidateIssuerSigningKey = true,
    ValidIssuer=issur,
    ValidAudience=audience,
    IssuerSigningKey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey))
};
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
