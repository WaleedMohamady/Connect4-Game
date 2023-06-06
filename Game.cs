using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;

namespace WindowsFormsApp1
{
    public enum State { Blue, Red, Empty };  //Cell Value
    internal class Game
    {
        //................Game Main Attributes
        public int BWidth;
        public int BHeight;
        public int rows = 6;
        public int cols = 7;
        public bool Player1;
        public bool player2;
        public int CellWidth;
        public int CellHeight;
        public State PColor;
        public State[,] BoardArray = new State[6, 7];

        //.................Game Constructor
        public Game()
        {
            BWidth = 700;
            BHeight = 600;
            Player1 = true;
            player2 = false;
            CellWidth = BWidth / cols;
            CellHeight = BHeight / rows;
            PColor = State.Blue;
            StartGame();
        }

        //.................Game Functions
        public void StartGame()  //Start New Game Function
        {
            Player1 = true;
            player2 = false;
            PColor = State.Blue;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    BoardArray[i, j] = State.Empty;
                }
            }
        }
        public void Draw(PaintEventArgs e) //Drawing Function
        {
            Pen line = new Pen(Color.MidnightBlue);
            int x1 = 0;
            int x2 = 700;
            int y1 = 0;
            int y2 = 600;
            for (int i = 0; i <= BWidth; i += 100)
            {
                e.Graphics.DrawLine(line, i, y1, i, y2);
            }
            for (int i = 0; i <= BHeight; i += 100)
            {
                e.Graphics.DrawLine(line, x1, i, x2, i);
            }
            for (int y = 0; y < BHeight; y += 100)
            {
                for (int x = 0; x < BWidth; x += 100)
                {
                    e.Graphics.FillEllipse(Brushes.White, new Rectangle(x, y, 100, 100));
                }
            }
        }
        public int GetColIndex(Point pt) //Getting Column Number
        {
            for (int x = 0; x < BWidth; x += 100)
            {
                if ((pt.X >= x) && (pt.X < x + CellWidth))
                {
                    return x / 100;
                }
            }
            return -1;
        }

        public int CheckRowsAvailability(int col) //Check available Rows and get Row Number
        {
            if (BoardArray[0, col] == State.Empty)
            {
                for (int i = rows - 1; i >= 0; i--)
                {
                    if (BoardArray[i, col] == State.Empty)
                    {
                        return i;
                    }
                }
            }
            return -1;
        }

        //Check making 4 consecutive color vertically
        public bool CompleteVertical(int row, int col, State color)
        {
            if (row + 3 >= rows)
            {
                return false;
            }
            for (int distance = 0; distance < 4; distance++)
            {
                if (BoardArray[row + distance, col] != color) { return false; }
            }
            return true;
        }
        //Check making 4 consecutive color horizontally
        public bool CompleteHerozontally(int row, int col, State color)
        {
            if (col + 3 >= cols) { return false; }

            for (int distance = 0; distance < 4; distance++)
            {
                if (BoardArray[row, col + distance] != color) { return false; }
            }

            return true;
        }
        //Check making 4 consecutive color diagonally up
        public bool CompleteDiagonalUp(int row, int col, State color)
        {
            if (row - 3 < 0) { return false; }
            if (col + 3 >= cols) { return false; }

            for (int distance = 0; distance < 4; distance++)
            {
                if (BoardArray[row - distance, col + distance] != color) { return false; }
            }

            return true;
        }
        //Check making 4 consecutive color diagonally down
        public bool CompleteDiagonalDown(int row, int col, State color)
        {
            if (row + 3 >= rows) { return false; }
            if (col + 3 >= cols) { return false; }

            for (int distance = 0; distance < 4; distance++)
            {
                if (BoardArray[row + distance, col + distance] != color) { return false; }
            }
            return true;
        }
        //Check Winning Function
        public bool Winning(State color)
        {
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    if (CompleteVertical(row, col, color))
                    {
                        return true;
                    }
                    if (CompleteHerozontally(row, col, color))
                    {
                        return true;
                    }
                    if (CompleteDiagonalUp(row, col, color))
                    {
                        return true;
                    }
                    if (CompleteDiagonalDown(row, col, color))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
        //Check if we can add in a specific column
        public bool CanAdd(int column)
        {
            if (BoardArray[0, column] == State.Empty) { return true; }
            else { return false; }
        }
        //Check if the panel is full or not
        public bool IsFull()
        {
            for (int column = 0; column < cols; column++)
            {
                if (CanAdd(column)) { return false; }
            }
            return true;
        }
        //Swapping Player Turn Function 
        public void Turn()
        {
            Player1 = !Player1;
            player2 = !player2;
            if (Player1)
            {
                PColor = State.Blue;
            }
            if (player2)
            {
                PColor = State.Red;
            }
        }

    }
}
