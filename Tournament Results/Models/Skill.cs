    using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tournament_Results.Models
{
    public class Skill
    {
        [Key]
        public Guid ID { get; set; } 
        public string? Title { get; set; }
    }
}
