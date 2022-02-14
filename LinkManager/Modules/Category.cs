using Discord.Interactions;

namespace LinkManager.Modules
{
   [Group("category", "Actions for managing categories")]
   public class Category : InteractionModuleBase
   {
      [SlashCommand("add", "Adds a new category")]
      public async Task AddCategory([Summary("name", "The name of the new category")] string category)
      {
         await Context.Interaction.RespondAsync($"A category with the name {category} has been created");
      }

      [SlashCommand("remove", "Removes an existing category")]
      public async Task RemoveCategory([Summary("name", "The name of the category you wish to remove")] string category)
      {
         await Context.Interaction.RespondAsync($"A category with the name **{category}** has been deleted");
      }

      [SlashCommand("edit", "Edits an existing category")]
      public async Task EditCategory([Summary("name", "The name of the category you wish to edit")] string category)
      {
         await Context.Interaction.RespondAsync($"A category with the name {category} has been edited");
      }
   }
}
