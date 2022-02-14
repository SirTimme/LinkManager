using Discord.Interactions;

namespace LinkManager.Utils
{
   public class LinkModal : IModal
   {
      public string Title { get; set; } =  "LinkManager";

      [InputLabel("Category")]
      [ModalTextInput("link_category", placeholder: "Monitore", maxLength: 25)]
      public string Category { get; set; }

      [InputLabel("Link")]
      [ModalTextInput("link_link", placeholder: "https://amazon.com", maxLength: 100)]
      public string Link { get; set; }
   }
}

