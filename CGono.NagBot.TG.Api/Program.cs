using CGono.NagBot.TG.Api.Startup;
using CGono.NagBot.TG.Services;
using CGono.NagBot.TG.Services.Impl;
using CGono.NagBot.TG.Services.Settings;
using Microsoft.Extensions.Options;
using Sentry.AspNetCore;
using Telegram.Bot;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register the webhook only in production
if (builder.Environment.IsProduction())
{
    builder.Services.AddHostedService<RegisterTelegramBotApiService>();
}

// Services
builder.Services.AddScoped<IBotApiService, TelegramBotApiService>();
builder.Services.AddScoped<ITelegramBotClient>(prov => new TelegramBotClient(prov.GetRequiredService<IOptions<BotApiSettings>>().Value.Token));

// Settings
builder.Services.Configure<BotApiSettings>(builder.Configuration.GetSection(BotApiSettings.BotApi));

// Sentry
builder.Services.AddSentry()
    .AddSentryOptions(opt =>
    {
        if (!bool.TryParse(builder.Configuration["Sentry:Debug"], out var debug))
            debug = false;
        if (!double.TryParse(builder.Configuration["Sentry:TracesSampleRate"], out var tracesSampleRate))
            tracesSampleRate = 0;

        opt.Dsn = builder.Configuration["Sentry:Dsn"];
        opt.Debug = debug;
        opt.TracesSampleRate = tracesSampleRate;
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseSentryTracing();

app.Run();
