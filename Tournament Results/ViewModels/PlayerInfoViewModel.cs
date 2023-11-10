namespace Tournament_Results.ViewModels
{
    public class PlayerInfoViewModel
    {
        public IEnumerable<TournamentResultVM> TournamentResults { get; set; }
        public PlayerDTO Player { get; set; }
        public PlayerInfoViewModel(IEnumerable<TournamentResultVM> tournamentResults, PlayerDTO player)
        {
            TournamentResults = tournamentResults;
            Player = player;
        }
    }
}
