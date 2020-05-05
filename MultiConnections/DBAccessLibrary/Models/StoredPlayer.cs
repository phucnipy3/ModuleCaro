using System;
using System.Collections.Generic;
using System.Text;

namespace DBAccessLibary.Models
{
    public class StoredPlayer
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public virtual List<StoredMatch> Matches { get; set; } = new List<StoredMatch>();

    }
}
