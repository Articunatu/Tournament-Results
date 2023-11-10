using Tournament_Results.Models;

namespace Tournament_Results.ViewModels
{
    public class RankingGridViewModel
    {
        public IEnumerable<Tournament> Tournaments { get; set; }
        public IEnumerable<PlayerRankingDTO> Players { get; set; }
    }
}
