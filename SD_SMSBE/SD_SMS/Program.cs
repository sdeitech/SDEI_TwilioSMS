using Microsoft.Extensions.Configuration;
using SD_SMS.Models;
using SD_SMS.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddTransient<ISMSService, SMSService>();
builder.Services.AddTransient<IMFAService, MFAService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(config => {
    var basePath = AppContext.BaseDirectory;
    var xmlPath = Path.Combine(basePath, "SD_SMS.xml");
    config.IncludeXmlComments(xmlPath);
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()
            .WithExposedHeaders("Content-Disposition", "Content-Length");
        });
});


// Loading Twilio config
builder.Services.Configure<TwilioConfig>(builder.Configuration.GetSection("TwilioConfig"));

var app = builder.Build();

app.UseCors("AllowAll");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
