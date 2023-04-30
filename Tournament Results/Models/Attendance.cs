using System.ComponentModel.DataAnnotations;

namespace Tournament_Results.Models
{
    public class Attendance
    {
        [Key]
        public Guid ID { get; set; } 
        public int? Placement { get; set; }
        public Guid PlayerID { get; set; }
        public Guid TournamentID { get; set; }
        public IEnumerable<Player>? Players { get; set; }
        public IEnumerable<Tournament>? Tournaments { get; set; }
    }
}