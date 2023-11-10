using Tournament_Results.Models;

namespace Tournament_Results.ViewModels
{
    public class PlayerDTO
    {
        public string? Tag { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public PlayerDTO(Player player)
        {
            Tag = player.Tag;
            FirstName = player.FirstName;
            LastName = player.LastName;
        }
    }
}
