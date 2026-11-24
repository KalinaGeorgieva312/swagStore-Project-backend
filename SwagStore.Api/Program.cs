using SwagStore.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddSingleton<ShoppingCartsManager>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins",
        policy =>
        {
            policy.WithOrigins("http://localhost:5118", "file://") // Front-end URL
                  .AllowAnyHeader()
                  .AllowAnyMethod()
                   .AllowAnyOrigin(); // Important for file:// protocol;
        });
});


var app = builder.Build();

app.UseRouting();          // Add this
app.UseCors("AllowSpecificOrigins"); // Must be after UseRouting and before UseAuthorization

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
