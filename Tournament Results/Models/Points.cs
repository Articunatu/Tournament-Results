using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tournament_Results.Models
{
    public class Points
    {
        [Key]
        public int ID { get; set; }
        public int Value { get; set; }
        //public Points() { }

        public Points(int _ID, int _value)
        {
            ID = _ID;
            Value = _value;
        }
    }
}
