using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.src
{
    class GameTracker
    {
        public GameState GameState { get; set; }
        public TurnState TurnState { get; set; }

        private Player player1;
        private Player player2;

        public GameTracker()
        {
            //constructor
            GameState = GameState.End;
            TurnState = TurnState.Player1;

            player1 = new Player("Player 1");
            player2 = new Player("Player 2");
        }

        public void StartGame()
        {
            GameState = GameState.Start;
            player1.Restart();
            player2.Restart();
            Status();
            Console.WriteLine("New game!");
        }

        public void EndGame()
        {
            GameState = GameState.End;
            Console.WriteLine("Game end!");
        }

        public void CheckWinner()
        {
            if (player1.ShipLeft == 0 || player2.ShipLeft == 0)
            {
                TurnState winner, loser;
                if (TurnState == TurnState.Player1)
                {
                    winner = TurnState.Player2;
                    loser = TurnState.Player1;
                }
                else
                {
                    winner = TurnState.Player1;
                    loser = TurnState.Player2;
                }
                Console.WriteLine("All {0} ships has sunk, {1} won the game!", loser, winner);
                EndGame();
            }
        }

        public void PlayerAttack(string tile)
        {
            if (GameState == GameState.Start)
            {
                int x = char.ToUpper(tile[0]) - 65;
                int y = int.Parse(tile[1].ToString());
                string detail = "";

                if (TurnState == TurnState.Player1)
                {
                    if (player2.Attacked(x, y, out detail))
                    {
                        TurnState = TurnState.Player2;
                    }
                }
                else
                {
                    if (player1.Attacked(x, y, out detail))
                    {
                        TurnState = TurnState.Player1;
                    }
                }

                Status();
                Console.WriteLine(detail);
                //check winner and end the game if there is one
                CheckWinner();
            }
            else
            {
                Console.WriteLine("Currently no game, please use 's' to start new game");
            }
        }

        public void Status()
        {
            player1.PrintStatus();
            player2.PrintStatus();
            Console.WriteLine("Now player turn: " + TurnState);
        }
    }
}
