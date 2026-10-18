using Luftborn.Application.Products;
using Luftborn.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Allow requests from our local JavaScript frontend.
builder.Services.AddCors(options =>
{
    options.AddPolicy("LuftbornClient", policy =>
    {
        policy.WithOrigins("http://127.0.0.1:8080")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Add services to the container.
// Register the product application service.
builder.Services.AddScoped<ProductService>();

// Register database and data-access services.
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// Apply the CORS policy before authorization.
app.UseCors("LuftbornClient");

app.UseAuthorization();

app.MapControllers();

app.Run();
