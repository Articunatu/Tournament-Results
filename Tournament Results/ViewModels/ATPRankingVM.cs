using Tournament_Results.Models;

namespace Tournament_Results.ViewModels
{
    public class ATPRankingVM
    {
        public IEnumerable<Tournament> Tournaments { get; set; }
        public IEnumerable<PlayerRankingVM> Players { get; set; }
    }
}
