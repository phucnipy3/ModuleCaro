using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryClient
{
    public class MoveTracker
    {
        private List<string> opponentMoves;
        private List<string> allyMoves;
        public MoveTracker()
        {
            Reset();    
        }
        public void AddOpponentMove(string move)
        {
            opponentMoves.Add(move);
        }
        public bool TryAddAllyMove(string move)
        {
            if (opponentMoves.Contains(move))
                return false;
            if (allyMoves.Contains(move))
                return false;
            allyMoves.Add(move);
            return true;
        }

        internal void Reset()
        {
            opponentMoves = new List<string>();
            allyMoves = new List<string>();
        }
    }

}
