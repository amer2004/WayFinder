using GraduationProjectWebApi;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Authorization;
using System.Text;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
const string ConStr = "Data Source=AMER-LAPTOP\\SQLEXPRESS;" +
    "Integrated Security=True;" +
    "Connect Timeout=30;" +
    "Database = GraduationProjectDB" +
    "Encrypt=false;" +
    "Trust Server Certificate=True;" +
    "Application Intent=ReadWrite;" +
    "Multi Subnet Failover=False";

builder.Services.AddAuthentication().AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateAudience = false,
        ValidateIssuer = false,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("J7y*9Q!bN5@Gw@QWxsDWATFFMJ7y*is!bN5@Gw@QWxsDWATFFM")),
    };
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "Bearer",
        Type = SecuritySchemeType.ApiKey,
    });
    options.OperationFilter<SecurityRequirementsOperationFilter>();

});
builder.Services.AddDbContext<AppDbContext>(option => option.UseSqlServer(ConStr));


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
