namespace Tournament_Results.Models
{
    public class Player
    {
        public Guid ID { get; set; } 
        public string? Tag { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? CountryID { get; set; }
        public Country? Country { get; set; }
    }
}