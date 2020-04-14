using DBAccessLibary.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;

namespace DBAccessLibary.DBAccess
{
    public class ModuleContext: DbContext
    {
        public ModuleContext() : base("name=CaroModule")
        {
            Database.SetInitializer<ModuleContext>(null);
            Database.CreateIfNotExists();
        }

        public DbSet<StoredPlayer> Players { get; set; }
        public DbSet<StoredGame> Games { get; set; }
        public DbSet<StoredMatch> Matches { get; set; }
        public DbSet<Move> Moves { get; set; }

    }
}
