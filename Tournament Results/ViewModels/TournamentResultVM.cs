namespace Tournament_Results.ViewModels
{
    public class TournamentResultVM
    {
        public string? Title { get; set; }
        public int Placing { get; set; }
        public DateTime Date { get; set; }
        public bool IsPremier { get; set; }
    }
}
