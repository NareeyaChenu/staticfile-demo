using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using staticfile_sv.Interface;
using staticfile_sv.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
    {
      c.SwaggerDoc("v2", new OpenApiInfo { Title = "My API", Version = "v2" });
      
    });
builder.Services.AddScoped<IStaticfileService, StaticfileServiceV1>();
builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft
            .Json.ReferenceLoopHandling.Ignore)
                .AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver
                    = new DefaultContractResolver()
);

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

// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
