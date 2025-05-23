using GraduationProjectWebApi;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Text;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
const string ConStr = "workstation id=WayFinderDB.mssql.somee.com;packet size=4096;user id=BayanAlkh_SQLLogin_1;pwd=4f9grdx8iz;data source=WayFinderDB.mssql.somee.com;persist security info=False;initial catalog=WayFinderDB;TrustServerCertificate=True";
//const string ConStr = "Data Source=AMER-LAPTOP\\SQLEXPRESS;initial catalog=WayFinderDB;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False";

builder.Services.AddAuthentication().AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateAudience = false,
        RequireExpirationTime = false,
        ValidateIssuer = false,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("J7y*9Q!bN5@Gw@QWxsDWATFFMJ7y*is!bN5@Gw@QWxsDWATFFM")),
    };
});

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter<AdminType>());
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCors();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
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

app.UseCors(c => c.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.UseAuthorization();

app.MapControllers();

app.Run();
