using System;
using System.Collections.Generic;
using System.Text;

namespace DBAccessLibary.Models
{
    public class StoredMatch
    {
        public int Id { get; set; }
        public StoredPlayer Player1 { get; set; }
        public StoredPlayer Player2 { get; set; }
        public List<StoredGame> Games { get; set; } = new List<StoredGame>();
        public bool IsPlayer1PlayFirst { get; set; }
        public bool HasBlockTwoEndsRule { get; set; }
        public int GameCount { get; set; }
        public int Score1 { get; set; }
        public int Score2 { get; set; }
        public DateTime DateTime { get; set; }
        
    }
}
