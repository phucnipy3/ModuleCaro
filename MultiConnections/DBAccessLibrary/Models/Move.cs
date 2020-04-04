using System;
using System.Collections.Generic;
using System.Text;

namespace DBAccessLibary.Models
{
    public class Move
    {
        public int Id { get; set; }
        public char Chessman { get; set; }
        public int CoordinateX { get; set; }
        public int CoordinateY { get; set; }
        public int Order { get; set; }
    }
}
