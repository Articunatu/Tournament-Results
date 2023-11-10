using System.ComponentModel.DataAnnotations;

namespace Tournament_Results.Models
{
    public class Attendance
    {
        [Key]
        public Guid ID { get; set; } 
        public int Placing { get; set; }
        public Guid PlayerID { get; set; }
        public Guid TournamentID { get; set; }
        public string? Details { get; set; }
        //public IEnumerable<Player>? Players { get; set; }
        //public IEnumerable<Tournament>? Tournaments { get; set; }
    }
}