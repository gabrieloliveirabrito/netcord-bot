using NetCord;
using NetCord.Gateway;
using NetCord.Hosting.Gateway;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddOpenApi();
builder.Services.AddControllers();
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
builder.Services.AddGatewayHandlers(typeof(Program).Assembly);

var app = builder.Build();

app.UseSerilogRequestLogging();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

await app.RunAsync();