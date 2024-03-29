﻿namespace Minesweeper
{
    class SaperFieldOfCells
    {
        private SaperCell[,] saperField;

        public int numberOfNotOpened = 0;

        public int numberOfMines = 0;

        private int Rows = 0;
        private int Columns = 0;
        //конструктор создает из числовой таблицы таблицу с соответствующими элементами класса SaperCell
        public SaperFieldOfCells(int[,] fieldOfNumbers,int _numberOfMines,int _Rows,int _Columns)
        {
            Rows = _Rows;
            Columns = _Columns;
            numberOfMines = _numberOfMines;
            saperField = new SaperCell[Rows, Columns];

            for (int i = 0; i < Rows; ++i)
                for (int j = 0; j < Columns; ++j)
                {
                    saperField[i, j] = new SaperCell(fieldOfNumbers[i, j], j, i);
                    if (fieldOfNumbers[i, j] == 9)//
                    {
                        numberOfNotOpened++;
                    }
                    if (fieldOfNumbers[i, j] == -1)//
                    {
                        numberOfMines--;
                    }
                }
            SetNeighboursForAllCells();
        }

        //находит соседей для всех ячеек
        public void SetNeighboursForAllCells()
        {
            for (int i = 0; i < Rows; ++i)
                for (int j = 0; j < Columns; ++j)
                {
                    FindNeighboursOfCell(saperField[i, j]);
                }
        }

        //находит соседей для данной ячейки
        public void FindNeighboursOfCell(SaperCell cell)
        {
            cell.numberOf9TypeNeighbours = 0;
            cell.numberOfFlags = 0;
            //если сверху есть сосед, то добавляем его
            if (cell.Y > 0)
            {
                cell.setNeighbour(saperField[cell.Y - 1, cell.X], 0);
            }

            //если сверху справа есть сосед, то добавляем его
            if (cell.Y > 0 && cell.X < Columns-1)
            {
                cell.setNeighbour(saperField[cell.Y - 1, cell.X + 1], 1);
            }

            //если справа есть сосед, то добавляем его
            if (cell.X < Columns-1)
            {
                cell.setNeighbour(saperField[cell.Y, cell.X + 1], 2);
            }

            //если справа снизу есть сосед, то добавляем его
            if (cell.Y < Rows-1 && cell.X < Columns-1)
            {
                cell.setNeighbour(saperField[cell.Y + 1, cell.X + 1], 3);
            }

            //если снизу есть сосед, то добавляем его
            if (cell.Y < Rows-1)
            {
                cell.setNeighbour(saperField[cell.Y + 1, cell.X], 4);
            }

            //если снизу слева есть сосед, то добавляем его
            if (cell.Y < Rows-1 && cell.X > 0)
            {
                cell.setNeighbour(saperField[cell.Y + 1, cell.X - 1], 5);
            }

            //если слева есть сосед, то добавляем его
            if (cell.X > 0)
            {
                cell.setNeighbour(saperField[cell.Y, cell.X - 1], 6);
            }

            //если сверху слева есть сосед, то добавляем его
            if (cell.Y > 0 && cell.X > 0)
            {
                cell.setNeighbour(saperField[cell.Y - 1, cell.X - 1], 7);
            }
        }

        public SaperCell[,] GetSaperField()
        {
            return saperField;
        }

        //меняет значение данной ячейки и меняет ее значение у всех соседей
        public void SetSaperCell(int value, int x, int y)
        {
            saperField[y, x].value = value;
        }

        //получить массив из неоткрытых ячеек
        public SaperCell[] GetNotOpened()
        {
            SaperCell[] notOpened = new SaperCell[numberOfNotOpened];
            int counter = 0;
            for (int i = 0; i < Rows; ++i)
                for (int j = 0; j < Columns; ++j)
                {
                    if (saperField[i, j].value == 9)
                    {
                        notOpened[counter++] = saperField[i, j];
                    }
                }
            return notOpened;
        }

        public float Probability()
        {
            if (numberOfNotOpened != 0)
            {
                return (float)numberOfMines / (float)numberOfNotOpened;
            }
            return 2;
        }
    }
}