using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tournament_Results.Models
{
    public class Tournament
    {
        [Key]
        public Guid ID { get; set; } 
        public string? Title { get; set; }
        public int? AttendeesAmount { get; set; }
        public decimal? PrizeMoney { get; set; }
        public bool IsPremier { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
