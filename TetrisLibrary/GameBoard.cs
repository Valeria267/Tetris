using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Tetris
{
    public class GameBoard
    {
        public int[,] playingField;//поле
        public int points;//очки
        public Figure element;//фигура


        public GameBoard(int m, int n)
        {
            playingField = new int[m, n];
            points = 0;
        }

        public void Сombustion()//удаление строки при полном ее заполнении
        {
            int check = -1;
            bool rezult = false;

            for (int i = playingField.GetLength(0) - 1; i >= 0; i--)
            {
                for (int j = 0; j < playingField.GetLength(1); j++)
                {
                    if (playingField[i, j] != 1)
                    {
                        check = -1;
                        rezult = false;
                        break;
                    }
                    else
                    { check = i; rezult = true; }
                }
                if (rezult)
                {
                    for (; check < playingField.GetLength(0) - 1; check++)
                    {
                        for (int j = 0; j < playingField.GetLength(1); j++)
                        {
                            playingField[check, j] = playingField[check + 1, j]; // удаление cтроки
                        }

                    }
                    for (int j = 0; j < playingField.GetLength(1); j++)
                    {
                        playingField[playingField.GetLength(0) - 1, j] = 0; //обнуление верхней строки
                    }
                    points += 100;
                    i++;
                }

            }
        }

        public bool CreateElement()// создание элемента на доске
        {
            bool rezult = true;
            points += 10;

          Сombustion();

            element = new Figure(playingField.GetLength(0) - 1, playingField.GetLength(1) - 1);

            foreach (Square items in element.ListElement)
            {
                if (playingField[items.row, items.cow] == 1)
                {
                    rezult = false;
                }
            }
            if (rezult)
            {
                foreach (Square items in element.ListElement)
                {
                    playingField[items.row, items.cow] = 1;
                }
            }
            return rezult;
        }


    }

}