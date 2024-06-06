using Backend.Business;
using Backend.Database;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLogging(loggingBuilder =>
{
    // Add configuration for logging (optional)
    loggingBuilder.AddConfiguration(builder.Configuration.GetSection("Logging"));
    loggingBuilder.AddConsole(); // Example: Add console logging
    loggingBuilder.AddDebug();   // Example: Add debug logging

    // Add file logging using custom FileLoggerProvider
    string logFilePath = Path.Combine(builder.Environment.ContentRootPath, "logs", "bookstore.log");
    loggingBuilder.AddProvider(new FileLoggerProvider(logFilePath));
    // Add other logging providers as needed
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

ConfigurationManager configuration = builder.Configuration;

// Configure DatabaseConfig from the configuration section
builder.Services.Configure<DbConfig>(configuration.GetSection("studyGroupDataBase"));

// Register other services
builder.Services.AddSingleton<IDbClient, DbClient>();
builder.Services.AddTransient<IDBService, DbService>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAnyOrigin",
        builder => builder
            .AllowAnyOrigin()   // Allow requests from any origin
            .AllowAnyHeader()   // Allow any header
            .AllowAnyMethod()); // Allow any HTTP method

});

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// CORS (Cross-Origin Resource Sharing) is a security feature that restricts what resources a web page can request from another domain
// Use case: Single-Page Applications (SPAs): If your frontend application (like Angular, React, or Vue.js) is served from a different origin (domain, protocol, or port) than your backend API.
// Use case: Cross-Domain API Requests: When your API is accessed from different domains or subdomains, especially during development or when integrating with third-party services.


app.UseHttpsRedirection();


app.UseRouting();
app.UseCors("AllowAnyOrigin");

app.UseAuthorization();

app.MapControllers();

app.Run();


 
