using NetCord;
using NetCord.Gateway;
using NetCord.Hosting.Gateway;
using NetCord.Hosting.Services;
using NetCord.Hosting.Services.ApplicationCommands;
using NetCord.Hosting.Services.Commands;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
#if !DEBUG
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
#endif
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();

builder.Host.UseSerilog();
builder.Services.AddHttpClient();

builder.Services.AddOpenApi();
builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
    });
});

builder.Services.AddDiscordGateway(options =>
{
    options.PublicKey = builder.Configuration["Discord:PublicKey"];
    options.Token = builder.Configuration["Discord:Token"];
    options.AutoStartStop = true;
    options.Intents = GatewayIntents.Guilds |
    GatewayIntents.GuildMessages |
    GatewayIntents.DirectMessages |
    GatewayIntents.MessageContent |
    GatewayIntents.DirectMessageReactions |
    GatewayIntents.GuildMessageReactions;

    options.Presence = new PresenceProperties(UserStatusType.Online).WithActivities([
        new UserActivityProperties("Coding with NetCord", UserActivityType.Competing)
    ]);
});

builder.Services.AddApplicationCommands(options =>
{
    options.AutoRegisterCommands = true;
});

builder.Services.AddCommands(options =>
{
    options.Prefix = "!";
});

builder.Services.AddGatewayHandlers(typeof(Program).Assembly);

var app = builder.Build();

app.UseSerilogRequestLogging();
app.UseCors();
app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.AddModules(typeof(Program).Assembly);

await app.RunAsync();