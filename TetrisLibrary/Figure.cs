using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Tetris
{

    public class Figure
    {
        public List<int> MoveDown;//элементы для проверки движения вниз
        public List<int> MoveRight;//элементы для проверки движения вправо
        public List<int> MoveLeft;//элементы для проверки движения влево
        public List<int> MoveUp;//элементы для проверки движения вверх
        private int rotationElement;// элемент, неперемещаемый во время поворота фигуры
        public List<Square> ListElement;//элементы
        public Color color;//цвет фигуры
        

        public Figure(int fieldHeight, int fieldWidth)
        {
            switch (new Random().Next(1, 8))
            {
                case 1:
                    ListElement = new List<Square>
                    {
                        new Square(fieldHeight,fieldWidth / 2 ),
                        new Square( fieldHeight,fieldWidth / 2 + 1),
                        new Square( fieldHeight - 1,fieldWidth / 2),
                        new Square( fieldHeight - 1,fieldWidth / 2 + 1)
                    };
                    MoveDown = new List<int> { 2, 3 };
                    MoveRight = new List<int> { 1, 3 };
                    MoveLeft = new List<int> { 0, 2 };
                    MoveUp = new List<int> { 0, 1 };
                    rotationElement = 0;
                    color = Color.Red;
                    ; break;
                case 2:
                    ListElement = new List<Square>
                    {
                        new Square(fieldHeight,fieldWidth / 2 ),
                        new Square( fieldHeight- 1,fieldWidth / 2 ),
                        new Square( fieldHeight - 2,fieldWidth / 2),
                        new Square( fieldHeight - 3,fieldWidth / 2 )
                    };
                    MoveDown = new List<int> { 3 };
                    MoveRight = new List<int> { 0, 1, 2, 3 };
                    MoveLeft = MoveRight;
                    MoveUp = new List<int> { 0 };
                    rotationElement = 0;
                    color = Color.SlateBlue;
                    break;
                case 3:
                    ListElement = new List<Square>
                    {
                        new Square(fieldHeight,fieldWidth / 2 ),
                        new Square( fieldHeight- 1,fieldWidth / 2 - 1),
                        new Square( fieldHeight - 1,fieldWidth / 2),
                        new Square( fieldHeight - 1,fieldWidth / 2 + 1)
                    };
                    MoveDown = new List<int> { 1, 2, 3 };
                    MoveRight = new List<int> { 0, 3 };
                    MoveLeft = new List<int> { 0, 1 };
                    MoveUp = new List<int> { 0, 1, 3 };
                    rotationElement = 1;
                    color = Color.Aqua;
                    break;
                case 4:
                    ListElement = new List<Square>
                    {
                        new Square(fieldHeight,fieldWidth / 2 ),
                        new Square( fieldHeight- 1,fieldWidth / 2 ),
                        new Square( fieldHeight - 2,fieldWidth / 2),
                        new Square( fieldHeight - 2,fieldWidth / 2-1)
                    };
                    MoveDown = new List<int> { 2, 3 };
                    MoveRight = new List<int> { 0, 1, 2 };
                    MoveLeft = new List<int> { 0, 1, 3 };
                    MoveUp = new List<int> { 0, 3 };
                    rotationElement = 1;
                    color = Color.Yellow;
                    break;
                case 5:
                    ListElement = new List<Square>
                    {
                        new Square(fieldHeight,fieldWidth / 2 -1),
                        new Square( fieldHeight,fieldWidth / 2 ),
                        new Square( fieldHeight - 1,fieldWidth / 2),
                        new Square( fieldHeight - 1,fieldWidth / 2 + 1)
                    };
                    MoveDown = new List<int> { 0, 2, 3 };
                    MoveRight = new List<int> { 1, 3 };
                    MoveLeft = new List<int> { 0, 2 };
                    MoveUp = new List<int> { 0, 1, 3 };
                    rotationElement = 2;
                    color = Color.SpringGreen;
                    break;
                case 6:
                    ListElement = new List<Square>
                    {
                        new Square(fieldHeight,fieldWidth / 2 ),
                        new Square( fieldHeight-1,fieldWidth / 2 ),
                        new Square( fieldHeight - 2,fieldWidth / 2),
                        new Square( fieldHeight - 2,fieldWidth / 2 + 1)
                    };
                    MoveDown = new List<int> { 2, 3 };
                    MoveRight = new List<int> { 0, 1, 3 };
                    MoveLeft = new List<int> { 0,1,2 };
                    MoveUp = new List<int> { 0,3 };
                    rotationElement = 1;
                    color = Color.PeachPuff;
                    break;
                case 7:
                    ListElement = new List<Square>
                    {
                        new Square(fieldHeight,fieldWidth / 2 +1),
                        new Square( fieldHeight,fieldWidth / 2 ),
                        new Square( fieldHeight - 1,fieldWidth / 2),
                        new Square( fieldHeight - 1,fieldWidth / 2 - 1)
                    };
                    MoveDown = new List<int> { 0, 2, 3 };
                    MoveRight = new List<int> { 0, 2 };
                    MoveLeft = new List<int> { 1, 3 };
                    MoveUp = new List<int> { 0, 1, 3 };
                    rotationElement = 1;
                    color = Color.OrangeRed;
                    break;

            }
        }

        public bool MoveFigure(int[,] playingField, List<int> CheckSquare, int step, int direction)// перемещение фигуры
        {
            bool rezult = true;
            foreach (int element in CheckSquare)
            {
                if (!ListElement[element].MoveLowerElement(playingField, step, direction))
                {
                    rezult = false;
                    break;
                }
            }
            if (rezult)
            {
                foreach (Square element in ListElement)
                {
                    playingField[element.row, element.cow] = 0;
                }
                foreach (Square element in ListElement)
                {
                    switch (direction)
                    {
                        case 1:
                            element.MoveElement(step, ref element.row);
                            break;
                        case 2:
                            element.MoveElement(step, ref element.cow);
                            break;
                    }
                    playingField[element.row, element.cow] = 1;
                }
            }
            return rezult;
        }
        public void Rotation(int[,] playingField)//метод вращения 
        {
            if ((ListElement[rotationElement].cow>1)&&(ListElement[rotationElement].cow < playingField.GetLength(1)-1)&&(ListElement[rotationElement].row < playingField.GetLength(0)-1))
            {
                int temp;
                List<int> tempList;

                foreach (var items in ListElement)//новые координаты кубиков
                {
                    playingField[items.row, items.cow] = 0;
                    temp = items.cow;
                    items.cow = ListElement[rotationElement].cow - items.row + ListElement[rotationElement].row;
                    items.row = ListElement[rotationElement].row + temp - ListElement[rotationElement].cow;
                    playingField[items.row, items.cow] = 1;
                }

                tempList = MoveLeft;
                MoveLeft = MoveUp;
                MoveUp = MoveRight;
                MoveRight = MoveDown;
                MoveDown = tempList;
            }
        }
    }
}
