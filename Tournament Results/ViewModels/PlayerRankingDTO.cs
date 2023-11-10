using Tournament_Results.Models;

namespace Tournament_Results.ViewModels
{
    public class PlayerRankingDTO
    {
        public string Tag { get; set; }
        public List<Points> Placings { get; set; }
        public int AveragePlacing { get; set; }
        public int SumOfPoints { get; set; }
    }
}
