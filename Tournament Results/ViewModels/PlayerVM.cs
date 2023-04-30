using Tournament_Results.Models;

namespace Tournament_Results.ViewModels
{
    public class PlayerVM
    {
        public string? Tag { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public PlayerVM(Player player)
        {
            Tag = player.Tag;
            FirstName = player.FirstName;
            LastName = player.LastName;
        }
    }
}
