using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tetris
{
   public class Square
    {
        public int row;// положение в строке
        public int cow;//положение в столбце

        public Square(int row, int cow)
        {
            this.row = row;
            this.cow = cow;
        }


        public bool MoveLowerElement(int[,] playingField, int movementStep, int directoin)// проверка элементов, в зависимости от выбора напраления движения
        {
            bool rezult = false;
            switch (directoin)
            {
                case 1:
                    if ((row > 0) && (playingField[row + movementStep, cow] != 1))//проверка на возможность движения элемента вниз
                    {
                        rezult = true;
                    }
                    break;
                case 2:
                    if ((cow < playingField.GetLength(1) - 1) && (cow > 0) && (playingField[row, cow + movementStep] != 1))//проверка на возможность движения элемента вправо или влево в зависимости от шага 
                    {
                        rezult = true;
                    }
                    break;
            }
            return rezult;
        }

        public void MoveElement(int movementStep, ref int rowOrCow)//перемещение после проверки
        {
            rowOrCow += movementStep;
        }
    }

}
