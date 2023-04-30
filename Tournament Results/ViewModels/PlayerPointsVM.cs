namespace Tournament_Results.ViewModels
{
    public class PlayerPointsVM
    {
        public Guid PlayerID { get; set; }
        public string? Tag { get; set; }
        public int TotalPoints { get; set; }
    }
}
