using System.ComponentModel.DataAnnotations;

namespace Tournament_Results.Models
{
    public class Main
    {
        [Key]
        public Guid ID { get; set; } 
        public byte? IsMain { get; set; }
        public string? SkillID { get; set; }
        public string? PlayerID { get; set; }
        public IEnumerable<Player>? Players { get; set; }
        public IEnumerable<Skill>? Skills { get; set; }
    }
}