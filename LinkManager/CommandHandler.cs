using Discord.Interactions;
using Discord.WebSocket;
using System.Reflection;

namespace LinkManager
{
   public class CommandHandler
   {
      private readonly DiscordSocketClient client;
      private readonly InteractionService commands;
      private readonly IServiceProvider services;

      public CommandHandler(DiscordSocketClient client, InteractionService commands, IServiceProvider services)
      {
         this.client = client;
         this.commands = commands;
         this.services = services;
      }

      public async Task InitializeAsync()
      {
         await commands.AddModulesAsync(Assembly.GetEntryAssembly(), services);

         client.InteractionCreated += HandleInteraction;
      }

      private async Task HandleInteraction(SocketInteraction interaction)
      {
         var ctx = new SocketInteractionContext(client, interaction);

         await commands.ExecuteCommandAsync(ctx, services);
      }
   }
}
