using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LinkManager
{
    public class Program
    {
        static void Main(string[] args)
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            RunAsync(config).GetAwaiter().GetResult();
        }

        static async Task RunAsync(IConfiguration configuration)
        {
            using var services = ConfigureServices(configuration);

            var client = services.GetRequiredService<DiscordSocketClient>();
            var commands = services.GetRequiredService<InteractionService>();

            client.Log += LogAsync;
            commands.Log += LogAsync;

            client.Ready += async () =>
            {
                if (configuration.GetValue<bool>("debug"))
                {
                    await commands.RegisterCommandsToGuildAsync(configuration.GetValue<ulong>("devGuild"), deleteMissing: true);
                }
                else
                {
                    await commands.RegisterCommandsGloballyAsync(true);
                }
            };

            await services.GetRequiredService<CommandHandler>().InitializeAsync();

            await client.LoginAsync(TokenType.Bot, configuration["token"]);
            await client.StartAsync();

            await Task.Delay(Timeout.Infinite);
        }

        static Task LogAsync(LogMessage message)
        {
            Console.WriteLine(message.ToString());
            return Task.CompletedTask;
        }

        static ServiceProvider ConfigureServices(IConfiguration config)
        {
            return new ServiceCollection()
                        .AddSingleton(config)
                        .AddSingleton<DiscordSocketClient>()
                        .AddSingleton(x => new InteractionService(x.GetRequiredService<DiscordSocketClient>()))
                        .AddSingleton<CommandHandler>()
                        .BuildServiceProvider();
        }
    }
}