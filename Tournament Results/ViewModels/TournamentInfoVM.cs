namespace Tournament_Results.ViewModels
{
    public class TournamentInfoVM
    {
        public string Title { get; set; }
        public IEnumerable<PlayerResultVM> PlayerResults  { get; set; }
        public TournamentInfoVM(string title, IEnumerable<PlayerResultVM> playerResults)
        {
            Title = title;
            PlayerResults = playerResults;
        }
    }
}
