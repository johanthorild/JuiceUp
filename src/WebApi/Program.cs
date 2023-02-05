using Application;

using Carter;

using Infrastructure;

using Presentation;

var builder = WebApplication.CreateBuilder(args);

// Makes it possible for developers to have their own personal and ignored appsettings file
builder.Configuration.AddJsonFile($"appsettings.Local.json", optional: true, reloadOnChange: true);

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration)
    .AddPresentation();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(opt =>
    {
        opt.SwaggerEndpoint($"juiceup/swagger.json", "JuiceUp API");
    });
}

app.UseHttpsRedirection();

app.MapCarter();

app.Run();