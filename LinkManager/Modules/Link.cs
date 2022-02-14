using Discord.Interactions;
using LinkManager.Utils;

namespace LinkManager.Modules
{
   [Group("link", "Actions for managing links")]
   public class Link : InteractionModuleBase
   {
      [SlashCommand("add", "Adds a new link to a given category")]
      public async Task AddLink()
      {
         await Context.Interaction.RespondWithModalAsync<LinkModal>("link_add_modal");
      }

      [ModalInteraction("link_add_modal", ignoreGroupNames: true)]
      public async Task ModalResponse(LinkModal modal)
      {
         await RespondAsync($"The link <{modal.Link}> has been saved to the category {modal.Category}");
      }
   }
}
