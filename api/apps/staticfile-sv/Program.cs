using System.Reflection;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using staticfile_sv.Config;
using staticfile_sv.Interface;
using staticfile_sv.Service;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
// Add services to the container.

// builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.Configure<DatabaseSetting>(configuration.GetSection("StaticServiceDatabase"));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
    {
      c.SwaggerDoc("v2", new OpenApiInfo { Title = "My API", Version = "v2" });
      var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
      c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

    }).AddSwaggerGenNewtonsoftSupport();
builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft
            .Json.ReferenceLoopHandling.Ignore)
                .AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver
                    = new DefaultContractResolver()
);
builder.Services.AddCors(options =>
{
  options.AddPolicy(name: configuration["CorsName"], build =>
  {
    build.WithOrigins(configuration["AllowedHosts"])
        .AllowAnyHeader()
        .AllowAnyMethod();

  });
});

builder.Services.AddScoped<IBlogService, BlogService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI(c =>
      {
        c.SwaggerEndpoint("/swagger/v2/swagger.json", "My API V1");
      });
}
app.UseCors(configuration["CorsName"]);
// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
