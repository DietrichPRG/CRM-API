using System.Text.Json;
using System.Text.Json.Serialization;
using CRM_API.SchemaFilter;
using Data.ModelsCrm;
using Data.ModelsCrmClient;
using DomainDependencyInjection;
using Lamar.Microsoft.DependencyInjection;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseLamar(
    (context, registry) =>
    {
        // register services using Lamar

        // add the controllers
        registry.AddControllers(opt =>
        {
            opt.OutputFormatters.RemoveType<SystemTextJsonOutputFormatter>();
            opt.OutputFormatters.Add(new SystemTextJsonOutputFormatter(new JsonSerializerOptions(JsonSerializerDefaults.General)
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
            }));
        });

        registry.AddDbContext<CrmContext>(opt =>
        {
            opt.UseSqlite("Data Source=crmDataBase.db3", b => b.MigrationsAssembly("CRM-API"));
        });

        registry.AddDbContext<CrmClientContext>(opt =>
        {
            opt.UseSqlite("Data Source=crmClientDataBase.db3", b => b.MigrationsAssembly("CRM-API"));
        });

        registry.Include(DomainServiceRegister.GetRegister());

        registry.AddEndpointsApiExplorer();
        registry.AddSwaggerGen(opt =>
        {
            opt.SchemaFilter<SwaggerExcludeFilter>();
        });
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.DefaultModelsExpandDepth(-1);
        options.DefaultModelExpandDepth(-1);
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
