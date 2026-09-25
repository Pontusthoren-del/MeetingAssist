using MotesAssistant.Api.Services;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

//IAiService får de GeminiService
builder.Services.AddScoped<IAiService, GeminiAiService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("Frontend", policy =>
        policy.WithOrigins("http://localhost:5173")
        .AllowAnyHeader()
        .AllowAnyMethod()
    );
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options.WithTitle("MötesAssistent API");
    });
    app.MapGet("/", () => Results.Redirect("/scalar"));
}
app.UseCors("Frontend");
app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();

app.Run();