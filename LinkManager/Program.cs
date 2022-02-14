using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LinkManager
{
   public class Program
   {
      public static void Main(string[] args)
      {
         RunAsync().GetAwaiter().GetResult();
      }

      private static async Task RunAsync()
      {
         var config = new ConfigurationBuilder()
             .AddJsonFile("appsettings.json", optional: false)
             .Build();

         using var services = ConfigureServices(config);

         var client = services.GetRequiredService<DiscordSocketClient>();
         var commands = services.GetRequiredService<InteractionService>();

         client.Log += LogAsync;
         commands.Log += LogAsync;

         client.Ready += async () =>
         {
            if (config.GetValue<bool>("debug"))
            {
               await commands.RegisterCommandsToGuildAsync(config.GetValue<ulong>("devGuild"), deleteMissing: true);
            }
            else
            {
               await commands.RegisterCommandsGloballyAsync(deleteMissing: true);
            }
         };

         await services.GetRequiredService<CommandHandler>().InitializeAsync();

         await client.LoginAsync(TokenType.Bot, config["token"]);
         await client.StartAsync();

         await Task.Delay(Timeout.Infinite);
      }

      private static Task LogAsync(LogMessage message)
      {
         Console.WriteLine(message.ToString());
         return Task.CompletedTask;
      }

      private static ServiceProvider ConfigureServices(IConfiguration config)
      {
         var socketConfig = new DiscordSocketConfig()
         {
            GatewayIntents = GatewayIntents.None,
         };

         return new ServiceCollection()
            .AddSingleton(config)
            .AddSingleton(socketConfig)
            .AddSingleton<DiscordSocketClient>()
            .AddSingleton<InteractionService>()
            .AddSingleton<CommandHandler>()
            .BuildServiceProvider();
      }
   }
}