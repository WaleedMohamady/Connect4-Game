using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            label1.Text = "Blue's Turn";
            label1.ForeColor = Color.Blue;
        }
        Game game = new Game();

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            game.Draw(e);
        }

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            int ColumnIndex = game.GetColIndex(e.Location);
            //MessageBox.Show(ColumnIndex.ToString());
            if (ColumnIndex != -1)
            {
                int rowIndex = game.CheckRowsAvailability(ColumnIndex);
                //MessageBox.Show(rowIndex.ToString());
                if (rowIndex != -1)
                {
                    if (game.Player1)
                    {
                        game.BoardArray[rowIndex, ColumnIndex] = State.Blue;
                        Graphics g1 = panel1.CreateGraphics();
                        g1.FillEllipse(Brushes.DodgerBlue, ColumnIndex * 100, rowIndex * 100, 100, 100);
                    }
                    if (game.player2)
                    {
                        game.BoardArray[rowIndex, ColumnIndex] = State.Red;
                        Graphics g2 = panel1.CreateGraphics();
                        g2.FillEllipse(Brushes.DarkRed, ColumnIndex * 100, rowIndex * 100, 100, 100);
                    }
                    if (game.Winning(game.PColor))
                    {
                        MessageBox.Show($"{game.PColor} wins :)\n" +
                            "Click 'ok' to start a new game.");
                        game.StartGame();
                        panel1.Invalidate();
                        label1.Text = "Blue's Turn";
                        label1.ForeColor = Color.Blue;
                        return;
                    }
                    if (game.IsFull())
                    {
                        MessageBox.Show("The Game is draw :(");
                        return;
                    }
                    game.Turn();
                    if (game.Player1)
                    {
                        label1.Text = "Blue's Turn";
                        label1.ForeColor = Color.Blue;
                    }
                    if (game.player2)
                    {
                        label1.Text = "Red's Turn";
                        label1.ForeColor = Color.DarkRed;
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult confirmResult = MessageBox.Show("Are you want to start a new game ?",
                                    "Confirm Start a New Game !",
                                    MessageBoxButtons.YesNo);

            if (confirmResult == DialogResult.Yes)
            {
                game.StartGame();
                panel1.Invalidate();
                label1.Text = "Blue's Turn";
                label1.ForeColor = Color.Blue;
                return;
            }
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("How To Play The Game: \n" + "• The Blue starts playing.\n" + "• Each one play in his turn.\n" + "• The winner who make 4 consecutive color (Horizontally, Vertically or Diagonal).\n" + "\n" +
                "If you want to start a new game, click on 'Start New Game' button or from 'Game Menu' select 'New Game' or press 'Ctrl+N'.\n\n" +
                "You can change background color from 'View'.");
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void blackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BackColor = Color.Black;
        }

        private void whiteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BackColor = Color.White;
        }

        private void yellowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BackColor = Color.Yellow;
        }

        private void grayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BackColor = Color.Gray;
        }
    }
}

