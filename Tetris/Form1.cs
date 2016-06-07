using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Tetris
{
    public partial class Form1 : Form
    {
        GameBoard new_game;
        int[,] p;
        Timer myTimer = new Timer();
        bool rezult;

        public Form1()
        {
            InitializeComponent();
            KeyPreview = true;
            p = new int[25, 10];

            new_game = new GameBoard(25, 10);

            for (int i = 0; i < new_game.playingField.GetLength(0); i++)
            {
                for (int j = 0; j < new_game.playingField.GetLength(1); j++)
                {
                    tableLayoutPanel1.Controls.Add(new PictureBox(), j, i);

                }
            }
            myTimer.Tick += new EventHandler(MoveDownTimer);

        }

        public void MoveDownTimer(Object stateInfo, EventArgs e)
        {
            rezult = new_game.element.MoveFigure(new_game.playingField, new_game.element.MoveDown, -1, 1); //вызывать по счетчику
            ColorBoard();
            if (!rezult)
            {
                myTimer.Dispose();
                Moveitems();
            }

        }

        public void ColorBoard()
        {

            for (int i = 0; i < new_game.playingField.GetLength(0); i++)
            {
                for (int j = 0; j < new_game.playingField.GetLength(1); j++)
                {
                    if (new_game.playingField[i, j] != p[i, j])
                    {
                        if (new_game.playingField[i, j] == 1)
                        {
                            tableLayoutPanel1.GetControlFromPosition(j, 24 - i).BackColor = new_game.element.color;

                        }
                        else
                        {
                            tableLayoutPanel1.GetControlFromPosition(j, 24 - i).BackColor = Color.White;
                        }
                        p[i, j] = new_game.playingField[i, j];
                    }
                }
            }
        }
        public void CreateNewGame()

        {
            myTimer.Dispose();
            p = new int[25, 10];
            new_game = new GameBoard(25, 10);

            for (int i = 0; i < new_game.playingField.GetLength(0); i++)
            {
                for (int j = 0; j < new_game.playingField.GetLength(1); j++)
                {
                    tableLayoutPanel1.GetControlFromPosition(j, i).BackColor = Color.White;

                }
            }
        }

        public void Moveitems()
        {
            if (new_game.CreateElement())
            {
                ColorBoard();
                myTimer.Interval = 1000;
                myTimer.Start();
                label2.Text = "" + new_game.points;
            }
            else
            {
                DialogResult result = MessageBox.Show("вы набрали "+ new_game.points+". Хотите начать новую игру?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

                if (result == DialogResult.Yes)
                {
                    CreateNewGame();
                }
                else
                {
                    Close();
                }
            }
        }


        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateNewGame();
            Moveitems();

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Left)
            {
                new_game.element.MoveFigure(new_game.playingField, new_game.element.MoveLeft, -1, 2);
                ColorBoard();
            }
            if (e.KeyCode == Keys.Right)
            {
                new_game.element.MoveFigure(new_game.playingField, new_game.element.MoveRight, 1, 2);
                ColorBoard();
            }
            if (e.KeyCode == Keys.Down)
            {
                myTimer.Interval = 10;
            }
            if (e.KeyCode == Keys.Up)
            {   
                new_game.element.Rotation(new_game.playingField);
                ColorBoard();
            }
        }

        private void справкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Для перемещения фигуры используйте клавиши клавиатура вправо и влево. Для быстрого перемешения фигуры вниз используйте клавишу вниз. Для поворота фигуры нажмите клавищу вверх. Для начала игры нажмите Новая игра.", "");

        }
    }
}    
  
        

