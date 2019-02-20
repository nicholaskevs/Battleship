using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.src
{
    class Player
    {
        public string Name { get; set; }
        public char[,] Board { get; set; }
        public List<Ship> Ships { get; set; }
        public int ShipLeft { get; set; }

        //board setting
        private readonly int boardWidth = 10;
        private readonly int boardHeight = 10;

        public Player(string name)
        {
            Name = name;
            Board = new char[boardWidth, boardHeight];
            PopulateBoard();
            Ships = new List<Ship>();
            PlaceShip();
            ShipLeft = Ships.Count;
        }

        public void Restart()
        {
            PopulateBoard();
            PlaceShip();
            ShipLeft = Ships.Count;
        }

        public void PopulateBoard()
        {
            for (int i = 0; i < boardWidth; i++)
            {
                for (int j = 0; j < boardHeight; j++)
                {
                    Board[j, i] = 'O';
                }
            }
        }

        public void PrintBoard()
        {
            Console.WriteLine("  A B C D E F G H I J");

            for (int i = 0; i < boardWidth; i++)
            {
                Console.Write(i);
                for (int j = 0; j < boardHeight; j++)
                {
                    Console.Write(" " + Board[j, i]);
                }
                Console.WriteLine();
            }
        }

        public void PlaceShip()
        {
            //make 3 big ships (size 4)
            //and put it by design (manually by coder) on the board
            Ships.Clear();
            //Ships.Add(new Ship("Big", 5));
            Ships.Add(new Ship("Small", 3, Alignment.Horizontal));
            Ships.Add(new Ship("Small", 3, Alignment.Vertical));
            Ships.Add(new Ship("Small", 3, Alignment.Horizontal));

            //place ships
            Ships[0].Position[0] = new int[] { 3, 3 }; //D3
            Ships[0].Position[1] = new int[] { 4, 3 }; //E3
            Ships[0].Position[2] = new int[] { 5, 3 }; //F3

            Ships[1].Position[0] = new int[] { 7, 4 }; //H4
            Ships[1].Position[1] = new int[] { 7, 5 }; //H5
            Ships[1].Position[2] = new int[] { 7, 6 }; //H6

            Ships[2].Position[0] = new int[] { 1, 1 }; //B1
            Ships[2].Position[1] = new int[] { 2, 1 }; //C1
            Ships[2].Position[2] = new int[] { 3, 1 }; //D1

        }

        public bool Attacked(int x, int y, out string detail)
        {
            //attacked by other players with detail will be outputted to console
            if (Board[x, y] == 'O')
            {
                if (CheckShipHit(x, y, out int index))
                {
                    DestroyShip(index);
                    detail = "Attack success one ship has been destroyed!!";
                }
                else
                {
                    Board[x, y] = 'X';
                    detail = "Attack success but no ship hit";
                }
                return true;
            }

            detail = "Attack failed, tile has been attacked before, choose another tile";
            return false;
        }

        public bool CheckShipHit(int x, int y, out int index)
        {
            index = 0;
            foreach (Ship ship in Ships)
            {
                foreach (int[] pos in ship.Position)
                {
                    if (pos[0] == x && pos[1] == y)
                    {
                        return true;
                    }
                }
                index++;
            }
            return false;
        }

        public void DestroyShip(int index)
        {
            //mark ship's tile with X and remove it from list
            Ship ship = Ships[index];
            foreach (int[] pos in ship.Position)
            {
                Board[pos[0], pos[1]] = 'X';
            }
            Ships.RemoveAt(index);
            ShipLeft = Ships.Count;
        }

        public void PrintStatus()
        {
            Console.WriteLine(Name);
            Console.WriteLine("Ship left: " + ShipLeft);
            Console.WriteLine("Board:");
            PrintBoard();
            Console.WriteLine();
        }
    }
}
