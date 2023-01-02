using Blog.Server.Service;

var builder = WebApplication.CreateBuilder(args);

var  myAllowSpecificOrigins = "_myAllowSpecificOrigins";

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy(myAllowSpecificOrigins, policyBuilder =>
    {
        policyBuilder.WithOrigins("https://localhost:7136/")
            .AllowAnyOrigin();
    });
});

builder.Services.AddScoped<IRenderItemService, RenderItemService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(myAllowSpecificOrigins);

app.UseHttpsRedirection();



var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
    {
        var forecast = Enumerable.Range(1, 5).Select(index =>
                new WeatherForecast
                (
                    DateTime.Now.AddDays(index),
                    Random.Shared.Next(-20, 55),
                    summaries[Random.Shared.Next(summaries.Length)]
                ))
            .ToArray();
        return forecast;
    })
    .WithName("GetWeatherForecast");

// E:\Code\C#\Tool\BlazorBlog\BlazorBlog.Server\assets\hello.md
app.MapGet("/parse-markdown-to-html", async (string markdownFilePath) =>
{
    Console.WriteLine(markdownFilePath);
    var markdown = File.ReadAllText(markdownFilePath);

    var service = new RenderItemService();
    var html = service.ParseMarkdownToHtml(markdown);
    return html;
}).WithName("ParseMarkdownToHtml");

// E:\Code\C#\Tool\BlazorBlog\BlazorBlog.Server\assets\hello.md
app.MapGet("/parse-markdown-to-dom", async (string markdownFilePath) =>
{
    Console.WriteLine(markdownFilePath);
    var markdown = File.ReadAllText(markdownFilePath);

    var service = new RenderItemService();
    var dom = await service.ParseMarkdown(markdown);
    return dom;
}).WithName("ParseMarkdownToDom");

app.Run();

internal record WeatherForecast(DateTime Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}