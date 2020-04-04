using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DBAccessLibary.Models
{
    public class Move
    {
        public int Id { get; set; }
        [MaxLength(2)]
        public string Chessman { get; set; }
        public int CoordinateX { get; set; }
        public int CoordinateY { get; set; }
        public int Order { get; set; }
    }
}
