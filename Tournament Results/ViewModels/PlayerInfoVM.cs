namespace Tournament_Results.ViewModels
{
    public class PlayerInfoVM
    {
        public IEnumerable<TournamentResultVM> TournamentResults { get; set; }
        public PlayerVM Player { get; set; }
        public PlayerInfoVM(IEnumerable<TournamentResultVM> tournamentResults, PlayerVM player)
        {
            TournamentResults = tournamentResults;
            Player = player;
        }
    }
}
