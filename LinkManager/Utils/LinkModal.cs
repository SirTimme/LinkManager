using Discord.Interactions;

namespace LinkManager.Utils
{
   public class LinkModal : IModal
   {
      public string Title { get; set; } =  "Add a new link to a category";

      [InputLabel("Category")]
      [ModalTextInput("link_category", placeholder: "Periphery", maxLength: 25)]
      public string Category { get; set; }

      [InputLabel("Link")]
      [ModalTextInput("link_link", placeholder: "https://discord.com", maxLength: 100)]
      public string Link { get; set; }
   }
}

