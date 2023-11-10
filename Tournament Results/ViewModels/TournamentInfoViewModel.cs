namespace Tournament_Results.ViewModels
{
    public class TournamentInfoViewModel
    {
        public string Title { get; set; }
        public IEnumerable<PlayerResultVM> PlayerResults  { get; set; }
        public TournamentInfoViewModel(string title, IEnumerable<PlayerResultVM> playerResults)
        {
            Title = title;
            PlayerResults = playerResults;
        }
    }
}
