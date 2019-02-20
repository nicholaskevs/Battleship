using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.src
{
    public enum GameState
    {
        Start,
        End
    }

    public enum TurnState
    {
        Player1,
        Player2
    }

    public enum Alignment
    {
        Vertical,
        Horizontal
    }
}
