using System;
using System.Linq;
using System.Windows.Forms;

namespace Minesweeper
{
    class BotAlgoritm
    {
        private SaperFieldOfCells field;
        private Tile[,] tiles;
        public int TIME_TO_BEGIN_NEW_GAME = 4;
        private int Rows = 0;
        private int Columns = 0;
        private Form1 form;
        public BotAlgoritm(SaperFieldOfCells saperField, Tile[,] _tiles,int _Rows,int _Columns,Form1 _form)
        {
            field = saperField;
            tiles = _tiles;
            Rows = _Rows;
            Columns = _Columns;
            form = _form;
        }
        public bool IfVictory()
        {
            if (field.numberOfNotOpened == 0)
            {
                return true;
            }
            return false;
        }

        //этот метод проверяет, есть ли клетки, у которых количество неоткрытых соседей равно количеству бомб-соседей
        //и заполняет этих соседей флагами о бомбе
        public void SimpleAlgorithm()
        {
            //получаем таблицу ячеек для удобства
            SaperCell[,] saperField = field.GetSaperField();
            for (int i = 0; i < Rows; ++i)
                for (int j = 0; j < Columns; ++j)
                {
                    //если клетка имеет в соседях неоткрытые бомбы(или имела)(т.е. клетка - число)
                    if (saperField[i, j].value > 0 && saperField[i, j].value < 9)
                    {
                        //если во всех неоткрытых соседях 100% бомбы
                        if (saperField[i, j].numberOf9TypeNeighbours > 0 &&
                            saperField[i, j].numberOf9TypeNeighbours + saperField[i, j].numberOfFlags == saperField[i, j].value)
                        {
                            //получаем всех неоткрытых соседей
                            SaperCell[] notOpenedNeighbours = saperField[i, j].get9TypeNeighbours();
                            int numberOfnotOpenedCells = saperField[i, j].numberOf9TypeNeighbours;
                            //ставим флаг о бомбе в каждой неоткрытой соседней клетке
                            for (int k = 0; k < numberOfnotOpenedCells; ++k)
                            {

                                tiles[notOpenedNeighbours[k].Y,
                                   notOpenedNeighbours[k].X].Flag();

                                //вносим наш флаг о бомбе в таблицу
                                field.SetSaperCell(-1,
                                    notOpenedNeighbours[k].X,
                                    notOpenedNeighbours[k].Y);
                            }

                            //необходимо внести изменения о соседях во все соседние клетки измененных ячеек
                            for (int k = 0; k < numberOfnotOpenedCells; ++k)
                            {
                                CalculateNeighboursForAllNeighboursOfThisCell(notOpenedNeighbours[k]);
                            }
                            saperField[i, j].numberOf9TypeNeighbours = 0;

                        }

                    }
                }
        }

        public void HardAlgorithm()
        {
            Horizontal121Template();
            Vertical121Template();

            Horizontal1221Template();
            Vertical1221Template();

            Horizontal111ClosedTemplate();
            Vertical111ClosedTemplate();

            Horizontal1111ClosedTemplate();
            Vertical1111ClosedTemplate();
        }

        //данный метод ищет три подряд горизонтально идущих ячейки типа 1, 1, 1, 
        //причем перед ними ровно три неоткрытых клетки(слева от левой единицы и справа от правой ячейки уже открыты или их нет)
        //(замкнутая тройка), тогда напротив них одна мина, напротив центральной ячейки
        private void Horizontal111ClosedTemplate()
        {
            //получаем таблицу ячеек для удобства
            SaperCell[,] saperField = field.GetSaperField();
            for (int i = 0; i < Rows; ++i)
                for (int j = 0; j < Columns-2; ++j)
                {
                    //если находим три подряд горизонтальных ячейки со значениями 1,1,1
                    if (IfThreeHorizontalOnes(i, j, 1, 1, 1) == true)
                    {
                        //если сверху над этой троицей три неоткрытых клетки
                        if (i > 0)
                        {
                            if (IfThreeHorizontalNotOpened(i - 1, j) == true)
                            {
                                //если эта тройка замкнута
                                if (IfThreeHorizontalClosed(i, j, true) == true)
                                {
                                    //кликаем по экрану правой кнопкой на местах мин
                                    tiles[i - 1, j + 1].Flag();

                                    //вносим наши флаги о бомбах в таблицу
                                    field.SetSaperCell(-1, j + 1, i - 1);

                                    //пересчитываем соседей для внесенных флагов
                                    CalculateNeighboursForAllNeighboursOfThisCell(saperField[i - 1, j + 1]);
                                }
                            }
                        }
                        //если снизу под этой троицей три неоткрытых клетки
                        if (i < Rows-1)
                        {
                            if (IfThreeHorizontalNotOpened(i + 1, j) == true)
                            {
                                //если эта тройка замкнута
                                if (IfThreeHorizontalClosed(i, j, false) == true)
                                {
                                    //кликаем по экрану правой кнопкой на местах мин
                                    tiles[i + 1, j + 1].Flag();

                                    //вносим наши флаги о бомбах в таблицу
                                    field.SetSaperCell(-1, j + 1, i + 1);

                                    //пересчитываем соседей для внесенных флагов
                                    CalculateNeighboursForAllNeighboursOfThisCell(saperField[i + 1, j + 1]);
                                }
                            }
                        }
                    }
                }
        }

        //данный метод ищет три подряд вертикально идущих ячейки типа 1, 1, 1, 
        //причем перед ними ровно три неоткрытых клетки(слева от левой единицы и справа от правой ячейки уже открыты или их нет)
        //(замкнутая тройка), тогда напротив них одна мина, напротив центральной ячейки
        private void Vertical111ClosedTemplate()
        {
            //получаем таблицу ячеек для удобства
            SaperCell[,] saperField = field.GetSaperField();
            for (int i = 0; i < Rows-2; ++i)
                for (int j = 0; j < Columns; ++j)
                {
                    //если находим три подряд вертикальных ячейки со значениями 1,1,1
                    if (IfThreeVerticalOnes(i, j, 1, 1, 1) == true)
                    {
                        //если слева от этой троицы три неоткрытых клетки
                        if (j > 0)
                        {
                            if (IfThreeVerticalNotOpened(i, j - 1) == true)
                            {
                                //если эта тройка замкнута
                                if (IfThreeVerticalClosed(i, j, true) == true)
                                {
                                    //кликаем по экрану правой кнопкой на местах мин
                                    tiles[i + 1, j - 1].Flag();

                                    //вносим наши флаги о бомбах в таблицу
                                    field.SetSaperCell(-1, j - 1, i + 1);

                                    //пересчитываем соседей для внесенных флагов
                                    CalculateNeighboursForAllNeighboursOfThisCell(saperField[i + 1, j - 1]);
                                }
                            }
                        }
                        //если справа от этой троицы три неоткрытых клетки
                        if (j < Columns-1)
                        {
                            if (IfThreeVerticalNotOpened(i, j + 1) == true)
                            {
                                //если эта тройка замкнута
                                if (IfThreeVerticalClosed(i, j, false) == true)
                                {
                                    //кликаем по экрану правой кнопкой на местах мин
                                    tiles[i + 1, j + 1].Flag();

                                    //вносим наши флаги о бомбах в таблицу
                                    field.SetSaperCell(-1, j + 1, i + 1);

                                    //пересчитываем соседей для внесенных флагов
                                    CalculateNeighboursForAllNeighboursOfThisCell(saperField[i + 1, j + 1]);
                                }
                            }
                        }
                    }
                }
        }

        //данный метод ищет четыре подряд горизонтально идущих ячейки типа 1, 1, 1, 1 
        //причем перед ними ровно четыре неоткрытых клетки(слева от левой единицы и справа от правой ячейки уже открыты или их нет)
        //(замкнутая четверка), тогда напротив них две мины, напротив левой и правой ячеек
        private void Horizontal1111ClosedTemplate()
        {
            //получаем таблицу ячеек для удобства
            SaperCell[,] saperField = field.GetSaperField();
            for (int i = 0; i < Rows; ++i)
                for (int j = 0; j < Columns-3; ++j)
                {
                    //если находим четыре подряд горизонтальных ячейки со значениями 1,1,1,1
                    if (IfFourHorizontalOnes(i, j, 1, 1, 1, 1) == true)
                    {
                        //если сверху над этой четверкой четыре неоткрытых клетки
                        if (i > 0)
                        {
                            if (IfFourHorizontalNotOpened(i - 1, j) == true)
                            {
                                //если эта четверка замкнута
                                if (IfFourHorizontalClosed(i, j, true) == true)
                                {
                                    //кликаем по экрану правой кнопкой на местах мин
                                    tiles[i - 1, j].Flag();

                                    tiles[i - 1, j + 3].Flag();
                                    //вносим наши флаги о бомбах в таблицу
                                    field.SetSaperCell(-1, j, i - 1);
                                    field.SetSaperCell(-1, j + 3, i - 1);

                                    //пересчитываем соседей для внесенных флагов
                                    CalculateNeighboursForAllNeighboursOfThisCell(saperField[i - 1, j]);
                                    CalculateNeighboursForAllNeighboursOfThisCell(saperField[i - 1, j + 3]);
                                }
                            }
                        }
                        //если снизу под этой четверкойчетыре неоткрытых клетки
                        if (i < Rows-1)
                        {
                            if (IfFourHorizontalNotOpened(i + 1, j) == true)
                            {
                                //если эта четверка замкнута
                                if (IfFourHorizontalClosed(i, j, false) == true)
                                {
                                    //кликаем по экрану правой кнопкой на местах мин
                                    tiles[i + 1, j].Flag();

                                    tiles[i + 1, j + 3].Flag();
                                    //вносим наши флаги о бомбах в таблицу
                                    field.SetSaperCell(-1, j, i + 1);
                                    field.SetSaperCell(-1, j + 3, i + 1);

                                    //пересчитываем соседей для внесенных флагов
                                    CalculateNeighboursForAllNeighboursOfThisCell(saperField[i + 1, j]);
                                    CalculateNeighboursForAllNeighboursOfThisCell(saperField[i + 1, j + 3]);
                                }
                            }
                        }
                    }
                }
        }

        //данный метод ищет четыре подряд вертикально идущих ячейки типа 1, 1, 1, 1 
        //причем перед ними ровно четыре неоткрытых клетки(слева от левой единицы и справа от правой ячейки уже открыты или их нет)
        //(замкнутая четверка), тогда напротив них две мины, напротив левой и правой ячеек
        private void Vertical1111ClosedTemplate()
        {
            //получаем таблицу ячеек для удобства
            SaperCell[,] saperField = field.GetSaperField();
            for (int i = 0; i < Rows-3; ++i)
                for (int j = 0; j < Columns; ++j)
                {
                    //если находим четыре подряд вертикальных ячейки со значениями 1,1,1,1
                    if (IfFourVerticalOnes(i, j, 1, 1, 1, 1) == true)
                    {
                        //если слева от этой четверки четыре неоткрытых клетки
                        if (j > 0)
                        {
                            if (IfFourVerticalNotOpened(i, j - 1) == true)
                            {
                                //если эта четверка замкнута
                                if (IfFourVerticalClosed(i, j, true) == true)
                                {
                                    //кликаем по экрану правой кнопкой на местах мин
                                    tiles[i, j - 1].Flag();

                                    tiles[i + 3, j - 1].Flag();

                                    //вносим наши флаги о бомбах в таблицу
                                    field.SetSaperCell(-1, j - 1, i);
                                    field.SetSaperCell(-1, j - 1, i + 3);

                                    //пересчитываем соседей для внесенных флагов
                                    CalculateNeighboursForAllNeighboursOfThisCell(saperField[i, j - 1]);
                                    CalculateNeighboursForAllNeighboursOfThisCell(saperField[i + 3, j - 1]);
                                }
                            }
                        }
                        //если справа от этой четверки четыре неоткрытых клетки
                        if (j < Columns-1)
                        {
                            if (IfFourVerticalNotOpened(i, j + 1) == true)
                            {
                                //если эта четверка замкнута
                                if (IfFourVerticalClosed(i, j, false) == true)
                                {
                                    //кликаем по экрану правой кнопкой на местах мин
                                    tiles[i, j + 1].Flag();

                                    tiles[i + 3, j + 1].Flag();
                                    //вносим наши флаги о бомбах в таблицу
                                    field.SetSaperCell(-1, j + 1, i);
                                    field.SetSaperCell(-1, j + 1, i + 3);

                                    //пересчитываем соседей для внесенных флагов
                                    CalculateNeighboursForAllNeighboursOfThisCell(saperField[i, j + 1]);
                                    CalculateNeighboursForAllNeighboursOfThisCell(saperField[i + 3, j + 1]);
                                }
                            }
                        }
                    }
                }
        }

        //данный метод ищет три подряд горизонтально идущих ячейки типа 1, 2, 1,
        //в этом случае ставим мины напротив единиц
        private void Horizontal121Template()
        {
            //получаем таблицу ячеек для удобства
            SaperCell[,] saperField = field.GetSaperField();
            for (int i = 0; i < Rows; ++i)
                for (int j = 0; j < Columns-2; ++j)
                {
                    //если находим три подряд горизонтальных ячейки со значениями 1,2,1 , то бомбы стоят по краям
                    if (IfThreeHorizontalOnes(i, j, 1, 2, 1) == true)
                    {
                        //если сверху над этой троицей три неоткрытых клетки
                        if (i > 0)
                        {
                            if (IfThreeHorizontalNotOpened(i - 1, j) == true && (i == (Rows - 1) || IfOneOfThreeHorizontalNotOpened(i + 1, j) == false))
                            {
                                //кликаем по экрану правой кнопкой на местах мин
                                tiles[i - 1, j].Flag();

                                tiles[i - 1, j + 2].Flag();
                                //вносим наши флаги о бомбах в таблицу
                                field.SetSaperCell(-1, j, i - 1);
                                field.SetSaperCell(-1, j + 2, i - 1);

                                //пересчитываем соседей для внесенных флагов
                                CalculateNeighboursForAllNeighboursOfThisCell(saperField[i - 1, j + 2]);
                                CalculateNeighboursForAllNeighboursOfThisCell(saperField[i - 1, j]);
                            }
                        }
                        //если снизу под этой троицей три неоткрытых клетки
                        if (i < Rows-1)
                        {
                            if (IfThreeHorizontalNotOpened(i + 1, j) == true && (i == 0 || IfOneOfThreeHorizontalNotOpened(i - 1, j) == false))
                            {
                                //кликаем по экрану правой кнопкой на местах мин
                                tiles[i + 1, j].Flag();

                                tiles[i + 1, j + 2].Flag();

                                //вносим наши флаги о бомбах в таблицу
                                field.SetSaperCell(-1, j, i + 1);
                                field.SetSaperCell(-1, j + 2, i + 1);

                                //пересчитываем соседей для внесенных флагов
                                CalculateNeighboursForAllNeighboursOfThisCell(saperField[i + 1, j + 2]);
                                CalculateNeighboursForAllNeighboursOfThisCell(saperField[i + 1, j]);
                            }
                        }
                    }
                }
        }

        //данный метод ищет три подряд вертикально идущих ячейки типа 1, 2, 1,
        //в этом случае ставим мины напротив единиц
        private void Vertical121Template()
        {
            //получаем таблицу ячеек для удобства
            SaperCell[,] saperField = field.GetSaperField();
            for (int i = 0; i < Rows-2; ++i)
                for (int j = 0; j < Columns; ++j)
                {
                    //если находим три подряд вертикальных ячейки со значениями 1,2,1 , то бомбы стоят по краям
                    if (IfThreeVerticalOnes(i, j, 1, 2, 1) == true)
                    {
                        //если слева от этой троицы три неоткрытых клетки
                        if (j > 0)
                        {
                            if (IfThreeVerticalNotOpened(i, j - 1) == true && (j == (Columns - 1) || IfOneOfThreeVerticalNotOpened(i, j + 1) == false))
                            {
                                tiles[i, j - 1].Flag();

                                tiles[i + 2, j - 1].Flag();

                                //вносим наши флаги о бомбах в таблицу
                                field.SetSaperCell(-1, j - 1, i);
                                field.SetSaperCell(-1, j - 1, i + 2);

                                //пересчитываем соседей для внесенных флагов
                                CalculateNeighboursForAllNeighboursOfThisCell(saperField[i, j - 1]);
                                CalculateNeighboursForAllNeighboursOfThisCell(saperField[i + 2, j - 1]);
                            }
                        }
                        //если справа от этой троицы три неоткрытых клетки
                        if (j < Columns-1)
                        {
                            if (IfThreeVerticalNotOpened(i, j + 1) == true && (j == 0 || IfOneOfThreeVerticalNotOpened(i, j - 1) == false))
                            {
                                tiles[i, j + 1].Flag();

                                tiles[i + 2, j + 1].Flag();

                                //вносим наши флаги о бомбах в таблицу
                                field.SetSaperCell(-1, j + 1, i);
                                field.SetSaperCell(-1, j + 1, i + 2);

                                //пересчитываем соседей для внесенных флагов
                                CalculateNeighboursForAllNeighboursOfThisCell(saperField[i, j + 1]);
                                CalculateNeighboursForAllNeighboursOfThisCell(saperField[i + 2, j + 1]);
                            }
                        }
                    }
                }
        }

        //данный метод ищет четыре подряд горизонтально идущих ячейки типа 1, 2, 2, 1,
        //в этом случае ставим мины напротив двоек
        private void Horizontal1221Template()
        {
            //получаем таблицу ячеек для удобства
            SaperCell[,] saperField = field.GetSaperField();
            for (int i = 0; i < Rows; ++i)
                for (int j = 0; j < Columns-3; ++j)
                {
                    //если находим четыре подряд горизонтальных ячейки со значениями 1,2,2,1 , то бомбы стоят напротив двоек
                    if (IfFourHorizontalOnes(i, j, 1, 2, 2, 1) == true)
                    {
                        //если сверху над этой четверкой четыре неоткрытых клетки
                        if (i > 1)
                        {
                            if (IfFourHorizontalNotOpened(i - 1, j) == true && (i == (Rows - 1) || IfOneOfFourHorizontalNotOpened(i + 1, j) == false))
                            {
                                //кликаем по экрану правой кнопкой на местах мин
                                tiles[i - 1, j + 1].Flag();

                                tiles[i - 1, j + 2].Flag();

                                //вносим наши флаги о бомбах в таблицу
                                field.SetSaperCell(-1, j + 1, i - 1);
                                field.SetSaperCell(-1, j + 2, i - 1);

                                //пересчитываем соседей для внесенных флагов
                                CalculateNeighboursForAllNeighboursOfThisCell(saperField[i - 1, j + 2]);
                                CalculateNeighboursForAllNeighboursOfThisCell(saperField[i - 1, j + 1]);
                            }
                        }
                        //если снизу под этой четверкой четыре неоткрытых клетки
                        if (i < Rows-1)
                        {
                            if (IfFourHorizontalNotOpened(i + 1, j) == true && (i == 0 || IfOneOfFourHorizontalNotOpened(i - 1, j) == false))
                            {
                                //кликаем по экрану правой кнопкой на местах мин
                                tiles[i + 1, j + 1].Flag();

                                tiles[i + 1, j + 2].Flag();

                                //вносим наши флаги о бомбах в таблицу
                                field.SetSaperCell(-1, j + 1, i + 1);
                                field.SetSaperCell(-1, j + 2, i + 1);

                                //пересчитываем соседей для внесенных флагов
                                CalculateNeighboursForAllNeighboursOfThisCell(saperField[i + 1, j + 2]);
                                CalculateNeighboursForAllNeighboursOfThisCell(saperField[i + 1, j + 1]);
                            }
                        }
                    }
                }
        }

        //данный метод ищет четыре подряд вертикально идущих ячейки типа 1, 2, 2, 1,
        //в этом случае ставим мины напротив двоек
        private void Vertical1221Template()
        {
            //получаем таблицу ячеек для удобства
            SaperCell[,] saperField = field.GetSaperField();
            for (int i = 0; i < Rows-3; ++i)
                for (int j = 0; j < Columns; ++j)
                {
                    //если находим четыре подряд вертикальных ячейки со значениями 1,2,2,1 , то бомбы стоят напротив двоек
                    if (IfFourVerticalOnes(i, j, 1, 2, 2, 1) == true)
                    {
                        //если слева от этой четверки неоткрытые клетки
                        if (j > 1)
                        {
                            if (IfFourVerticalNotOpened(i, j - 1) == true && (j == (Columns - 1) || IfOneOfFourVerticalNotOpened(i, j + 1) == false))
                            {
                                tiles[i + 1, j - 1].Flag();

                                tiles[i + 2, j - 1].Flag();
                                //вносим наши флаги о бомбах в таблицу
                                field.SetSaperCell(-1, j - 1, i + 1);
                                field.SetSaperCell(-1, j - 1, i + 2);

                                //пересчитываем соседей для внесенных флагов
                                CalculateNeighboursForAllNeighboursOfThisCell(saperField[i + 1, j - 1]);
                                CalculateNeighboursForAllNeighboursOfThisCell(saperField[i + 2, j - 1]);
                            }
                        }
                        //если справа от этой четверки неоткрытые клетки
                        if (j < Columns-1)
                        {
                            if (IfFourVerticalNotOpened(i, j + 1) == true && (j == 0 || IfOneOfFourVerticalNotOpened(i, j - 1) == false))
                            {
                                tiles[i + 1, j + 1].Flag();

                                tiles[i + 2, j + 1].Flag();
                                //вносим наши флаги о бомбах в таблицу
                                field.SetSaperCell(-1, j + 1, i + 1);
                                field.SetSaperCell(-1, j + 1, i + 2);

                                //пересчитываем соседей для внесенных флагов
                                CalculateNeighboursForAllNeighboursOfThisCell(saperField[i + 1, j + 1]);
                                CalculateNeighboursForAllNeighboursOfThisCell(saperField[i + 2, j + 1]);
                            }
                        }
                    }
                }
        }

        //этот вспомогательный метод проверяет, когда первые два алгоритма больше не изменяют позицию на поле
        public bool EndOfCycle(int[,] table1, int[,] table2)
        {
            for (int i = 0; i < Rows; ++i)
                for (int j = 0; j < Columns; ++j)
                {
                    //если клетка имеет в соседях неоткрытые бомбы(или имела)(т.е. клетка - число)
                    if (table1[i, j] != table2[i, j])
                    {
                        return false;
                    }
                }
            return true;
        }

        private void FirstRandomClick()
        {
            form.Open(0, 0);
            form.Open(0, Columns - 1);
            form.Open((Rows - 1), 0);
            form.Open((Rows - 1), Columns-1);
        }

        //кликает на случайную клетку из ненажатых ячеек
        private void RandomClick()
        {
            bool badDecision = true;
            Random rand = new Random();
            SaperCell[] notOpened = field.GetNotOpened();
            SaperCell randCell = notOpened[rand.Next(field.numberOfNotOpened)];
            for (int i = 0; badDecision == true && i < 10; ++i)
            {
                badDecision = false;
                randCell = notOpened[rand.Next(field.numberOfNotOpened)];
                foreach (var neighbour in randCell.getNeighbours())
                {
                    if (neighbour != null)
                        if (neighbour.value > 0 && neighbour.value < 9)
                        {
                            if (neighbour.Probability() > field.Probability())
                            {
                                {
                                    badDecision = true;
                                    break;
                                }
                            }
                        }
                }
            }
            form.Open(randCell.Y, randCell.X);
        }

        //метод пересчитывает соседей для всех соседних клеток данной ячейки, он необходим 
        //для внесения изменений в соседние ячейки, после установки флага о мине
        private void CalculateNeighboursForAllNeighboursOfThisCell(SaperCell cell)
        {
            foreach (var neighbour in cell.getNeighbours())
            {
                if (neighbour != null)
                {
                    field.FindNeighboursOfCell(neighbour);
                }
            }
        }
        public void DoubleClickingAlgorithm()
        {
            //получаем таблицу ячеек для удобства
            SaperCell[,] saperField = field.GetSaperField();

            for (int i = 0; i < Rows; ++i)
                for (int j = 0; j < Columns; ++j)
                {
                    //если клетка имеет в соседях неоткрытые бомбы
                    if (saperField[i, j].value > 0 && saperField[i, j].value < 9 && saperField[i, j].numberOf9TypeNeighbours > 0)
                    {
                        //если вокруг клетки уже расставлены флаги и их количество совпадает с количеством необходимых бомб
                        if (saperField[i, j].numberOfFlags == saperField[i, j].value)
                        {
                            form.OpenAllIfFlags(i, j);
                        }
                    }
                }
        }

        public void CleverRandomClick()
        {
            if (SafeNeighbourDetection() == true)
            {
                return;
            }
            bool firstClick = true;
            int x = -1;
            int y = -1;
            float minProbability = field.Probability();
            SaperCell[,] saperField = field.GetSaperField();
            for (int i = 0; i < Rows; ++i)
                for (int j = 0; j < Columns; ++j)
                {
                    if (saperField[i, j].value > 0 && saperField[i, j].value < 9 && saperField[i, j].numberOf9TypeNeighbours > 0)
                    {
                        firstClick = false;
                        if (saperField[i, j].Probability() < minProbability)
                        {
                            minProbability = saperField[i, j].Probability();
                            x = j;
                            y = i;
                        }
                    }
                }
            if (firstClick == true)
            {
                FirstRandomClick();
                firstClick = false;
                return;
            }
            if (x == -1 && y == -1)
            {
                RandomClick();
            }
            else
            {
                Random rand = new Random();
                int randomNumber = rand.Next(saperField[y, x].numberOf9TypeNeighbours);
                SaperCell[] notOpened = saperField[y, x].get9TypeNeighbours();

                form.Open(notOpened[randomNumber].Y, notOpened[randomNumber].X);
            }
        }

        //метод принимает координаты ячейки и смотрит, являются ли три подряд горизонтальных ячейки, начиная с данной, единицами
        private bool IfThreeHorizontalOnes(int y, int x, int firstValue, int secondValue, int thirdValue)
        {
            SaperCell[,] saperField = field.GetSaperField();
            if (saperField[y, x].value - saperField[y, x].numberOfFlags == firstValue &&
                        saperField[y, x + 1].value - saperField[y, x + 1].numberOfFlags == secondValue &&
                        saperField[y, x + 2].value - saperField[y, x + 2].numberOfFlags == thirdValue)
            {
                return true;
            }
            return false;
        }

        //метод принимает координаты ячейки и смотрит, являются ли три подряд вертикальных ячейки, начиная с данной, единицами
        private bool IfThreeVerticalOnes(int y, int x, int firstValue, int secondValue, int thirdValue)
        {
            SaperCell[,] saperField = field.GetSaperField();
            if (saperField[y, x].value - saperField[y, x].numberOfFlags == firstValue &&
                        saperField[y + 1, x].value - saperField[y + 1, x].numberOfFlags == secondValue &&
                        saperField[y + 2, x].value - saperField[y + 2, x].numberOfFlags == thirdValue)
            {
                return true;
            }
            return false;
        }

        //метод принимает координаты ячейки и смотрит, являются ли три подряд вертикальных ячейки, начиная с данной, единицами
        private bool IfFourVerticalOnes(int y, int x, int firstValue, int secondValue, int thirdValue, int fourthValue)
        {
            SaperCell[,] saperField = field.GetSaperField();
            if (saperField[y, x].value - saperField[y, x].numberOfFlags == firstValue &&
                        saperField[y + 1, x].value - saperField[y + 1, x].numberOfFlags == secondValue &&
                        saperField[y + 2, x].value - saperField[y + 2, x].numberOfFlags == thirdValue &&
                        saperField[y + 3, x].value - saperField[y + 3, x].numberOfFlags == fourthValue)
            {
                return true;
            }
            return false;
        }

        //метод принимает координаты ячейки и смотрит, являются ли три подряд горизонтальных ячейки, начиная с данной, единицами
        private bool IfFourHorizontalOnes(int y, int x, int firstValue, int secondValue, int thirdValue, int fourthValue)
        {
            SaperCell[,] saperField = field.GetSaperField();
            if (saperField[y, x].value - saperField[y, x].numberOfFlags == firstValue &&
                        saperField[y, x + 1].value - saperField[y, x + 1].numberOfFlags == secondValue &&
                        saperField[y, x + 2].value - saperField[y, x + 2].numberOfFlags == thirdValue &&
                        saperField[y, x + 3].value - saperField[y, x + 3].numberOfFlags == fourthValue)
            {
                return true;
            }
            return false;
        }

        //метод проверяет, является ли горизонтальная тройка, начиная с ячейки с координатами y, x замкнутой,
        //notOpenedTop == true, если проверка на неоткрытые ячейки сверху тройки
        private bool IfThreeHorizontalClosed(int y, int x, bool notOpenedTop)
        {
            SaperCell[,] saperField = field.GetSaperField();
            if (notOpenedTop == true)
            {
                if ((x > 0 && saperField[y - 1, x - 1].value != 9 &&
                    x + 2 < (Columns-1) && saperField[y - 1, x + 3].value != 9 &&
                    saperField[y, x - 1].value != 9 && saperField[y, x + 3].value != 9)
                    ||
                    (x == 0 && saperField[y - 1, x + 3].value != 9 && saperField[y, x + 3].value != 9)
                    ||
                    (x == (Columns-3) && saperField[y - 1, x - 1].value != 9 && saperField[y, x - 1].value != 9))
                {
                    return true;
                }
            }
            else
            {
                if ((x > 0 && saperField[y + 1, x - 1].value != 9 &&
                    x + 2 < (Columns-1) && saperField[y + 1, x + 3].value != 9 &&
                    saperField[y, x - 1].value != 9 && saperField[y, x + 3].value != 9)
                    ||
                    (x == 0 && saperField[y + 1, x + 3].value != 9 && saperField[y, x + 3].value != 9)
                    ||
                    (x == (Columns - 3) && saperField[y + 1, x - 1].value != 9 && saperField[y, x - 1].value != 9))
                {
                    return true;
                }
            }
            return false;
        }

        //метод проверяет, является ли горизонтальная четверка, начиная с ячейки с координатами y, x замкнутой,
        //notOpenedTop == true, если проверка на неоткрытые ячейки сверху четверки
        private bool IfFourHorizontalClosed(int y, int x, bool notOpenedTop)
        {
            SaperCell[,] saperField = field.GetSaperField();
            if (notOpenedTop == true)
            {
                if ((x > 0 && saperField[y - 1, x - 1].value != 9 &&
                    x + 3 < (Columns - 1) && saperField[y - 1, x + 4].value != 9 &&
                    saperField[y, x - 1].value != 9 && saperField[y, x + 4].value != 9)
                    ||
                    (x == 0 && saperField[y - 1, x + 4].value != 9 && saperField[y, x + 4].value != 9)
                    ||
                    (x == (Columns - 4) && saperField[y - 1, x - 1].value != 9 && saperField[y, x - 1].value != 9))
                {
                    return true;
                }
            }
            else
            {
                if ((x > 0 && saperField[y + 1, x - 1].value != 9 &&
                    x + 3 < (Columns - 1) && saperField[y + 1, x + 4].value != 9 &&
                    saperField[y, x - 1].value != 9 && saperField[y, x + 4].value != 9)
                    ||
                    (x == 0 && saperField[y + 1, x + 4].value != 9 && saperField[y, x + 4].value != 9)
                    ||
                    (x == (Columns - 4) && saperField[y + 1, x - 1].value != 9 && saperField[y, x - 1].value != 9))
                {
                    return true;
                }
            }
            return false;
        }

        //метод проверяет, является ли вертикальная тройка, начиная с ячейки с координатами y, x замкнутой,
        //notOpenedTop == true, если проверка на неоткрытые ячейки слева тройки
        private bool IfThreeVerticalClosed(int y, int x, bool notOpenedTop)
        {
            SaperCell[,] saperField = field.GetSaperField();
            if (notOpenedTop == true)
            {
                if ((y > 0 && saperField[y - 1, x - 1].value != 9 &&
                    y + 2 < (Rows-1) && saperField[y + 3, x - 1].value != 9 &&
                    saperField[y - 1, x].value != 9 && saperField[y + 3, x].value != 9 &&
                    (x == (Columns - 1) || saperField[y - 1, x + 1].value != 9 && saperField[y + 3, x + 1].value != 9))
                    ||
                    (y == 0 && saperField[y + 3, x - 1].value != 9 && saperField[y + 3, x].value != 9)
                    ||
                    (y == (Rows-3) && saperField[y - 1, x - 1].value != 9 && saperField[y - 1, x].value != 9))
                {
                    return true;
                }
            }
            else
            {
                if ((y > 0 && saperField[y - 1, x + 1].value != 9 &&
                    y + 2 < (Rows-1) && saperField[y + 3, x + 1].value != 9 &&
                    saperField[y - 1, x].value != 9 && saperField[y + 3, x].value != 9 &&
                    (x == 0 || saperField[y - 1, x - 1].value != 9 && saperField[y + 3, x - 1].value != 9))
                    ||
                    (y == 0 && saperField[y + 3, x + 1].value != 9 && saperField[y + 3, x].value != 9)
                    ||
                    (y == (Rows-3) && saperField[y - 1, x + 1].value != 9 && saperField[y - 1, x].value != 9))
                {
                    return true;
                }
            }
            return false;
        }

        //метод проверяет, является ли вертикальная четверка, начиная с ячейки с координатами y, x замкнутой,
        //notOpenedTop == true, если проверка на неоткрытые ячейки слева четверки
        private bool IfFourVerticalClosed(int y, int x, bool notOpenedTop)
        {
            SaperCell[,] saperField = field.GetSaperField();
            if (notOpenedTop == true)
            {
                if ((y > 0 && saperField[y - 1, x - 1].value != 9 &&
                    y + 3 < (Rows - 1) && saperField[y + 4, x - 1].value != 9 &&
                    saperField[y - 1, x].value != 9 && saperField[y + 4, x].value != 9 &&
                    (x == (Columns-1) || saperField[y - 1, x + 1].value != 9 && saperField[y + 4, x + 1].value != 9))
                    ||
                    (y == 0 && saperField[y + 4, x - 1].value != 9 && saperField[y + 4, x].value != 9)
                    ||
                    (y == (Rows-4) && saperField[y - 1, x - 1].value != 9 && saperField[y - 1, x].value != 9))
                {
                    return true;
                }
            }
            else
            {
                if ((y > 0 && saperField[y - 1, x + 1].value != 9 &&
                    y + 3 < (Rows - 1) && saperField[y + 4, x + 1].value != 9 &&
                    saperField[y - 1, x].value != 9 && saperField[y + 4, x].value != 9 &&
                    (x == 0 || saperField[y - 1, x - 1].value != 9 && saperField[y + 4, x - 1].value != 9))
                    ||
                    (y == 0 && saperField[y + 4, x + 1].value != 9 && saperField[y + 4, x].value != 9)
                    ||
                    (y == (Rows-4) && saperField[y - 1, x + 1].value != 9 && saperField[y - 1, x].value != 9))
                {
                    return true;
                }
            }
            return false;
        }

        //метод проверяет, является ли вертикальная тройка, начиная с данной клетки, неоткрытой
        private bool IfThreeVerticalNotOpened(int y, int x)
        {
            SaperCell[,] saperField = field.GetSaperField();
            if (saperField[y, x].value == 9 &&
                        saperField[y + 1, x].value == 9 &&
                        saperField[y + 2, x].value == 9)
            {
                return true;
            }
            return false;
        }

        //метод проверяет, является ли горизонтальная тройка, начиная с данной клетки, неоткрытой
        private bool IfThreeHorizontalNotOpened(int y, int x)
        {
            SaperCell[,] saperField = field.GetSaperField();
            if (saperField[y, x].value == 9 &&
                        saperField[y, x + 1].value == 9 &&
                        saperField[y, x + 2].value == 9)
            {
                return true;
            }
            return false;
        }

        //метод проверяет, является ли вертикальная четверка, начиная с данной клетки, неоткрытой
        private bool IfFourVerticalNotOpened(int y, int x)
        {
            SaperCell[,] saperField = field.GetSaperField();
            if (saperField[y, x].value == 9 &&
                        saperField[y + 1, x].value == 9 &&
                        saperField[y + 2, x].value == 9 &&
                        saperField[y + 3, x].value == 9)
            {
                return true;
            }
            return false;
        }

        //метод проверяет, является ли горизонтальная четверка, начиная с данной клетки, неоткрытой
        private bool IfFourHorizontalNotOpened(int y, int x)
        {
            SaperCell[,] saperField = field.GetSaperField();
            if (saperField[y, x].value == 9 &&
                        saperField[y, x + 1].value == 9 &&
                        saperField[y, x + 2].value == 9 &&
                        saperField[y, x + 3].value == 9)
            {
                return true;
            }
            return false;
        }

        //метод проверяет, является ли вертикальная тройка, начиная с данной клетки, неоткрытой
        private bool IfOneOfThreeVerticalNotOpened(int y, int x)
        {
            SaperCell[,] saperField = field.GetSaperField();
            if (saperField[y, x].value == 9 ||
                        saperField[y + 1, x].value == 9 ||
                        saperField[y + 2, x].value == 9)
            {
                return true;
            }
            return false;
        }

        //метод проверяет, является ли горизонтальная тройка, начиная с данной клетки, неоткрытой
        private bool IfOneOfThreeHorizontalNotOpened(int y, int x)
        {
            SaperCell[,] saperField = field.GetSaperField();
            if (saperField[y, x].value == 9 ||
                        saperField[y, x + 1].value == 9 ||
                        saperField[y, x + 2].value == 9)
            {
                return true;
            }
            return false;
        }

        //метод проверяет, является ли вертикальная четверка, начиная с данной клетки, неоткрытой
        private bool IfOneOfFourVerticalNotOpened(int y, int x)
        {
            SaperCell[,] saperField = field.GetSaperField();
            if (saperField[y, x].value == 9 ||
                        saperField[y + 1, x].value == 9 ||
                        saperField[y + 2, x].value == 9 ||
                        saperField[y + 3, x].value == 9)
            {
                return true;
            }
            return false;
        }

        //метод проверяет, является ли горизонтальная четверка, начиная с данной клетки, неоткрытой
        private bool IfOneOfFourHorizontalNotOpened(int y, int x)
        {
            SaperCell[,] saperField = field.GetSaperField();
            if (saperField[y, x].value == 9 ||
                        saperField[y, x + 1].value == 9 ||
                        saperField[y, x + 2].value == 9 ||
                        saperField[y, x + 3].value == 9)
            {
                return true;
            }
            return false;
        }

        //метод проверяет, входят ли все неоткрытые соседи данной клетки в массив
        private bool IfCommonNotOpenedNeighbours(int y, int x, SaperCell[] otherNeighbours)
        {
            SaperCell[,] saperField = field.GetSaperField();
            foreach (var cell in saperField[y, x].get9TypeNeighbours())
            {
                if (otherNeighbours.Contains(cell) == false)
                    return false;
            }
            return true;
        }
        public bool IfGameOver()
        {
            SaperCell[,] saperField = field.GetSaperField();
            for (int i = 0; i < Rows; ++i)
                for (int j = 0; j < Columns; ++j)
                {
                    if (saperField[i, j].value == 10)
                    {
                        return true;
                    }
                }
            return false;
        }


        //метод проверяет, может все неоткрытые соседи какой то клетки уже касаются кого то, в этом случае
        //он тыкает левой кнопкой в безопасное место
        private bool SafeNeighbourDetection()
        {
            SaperCell[,] saperField = field.GetSaperField();
            for (int i = 0; i < Rows; ++i)
                for (int j = 0; j < Columns; ++j)
                {
                    if (saperField[i, j].value > 0 && saperField[i, j].value < 9 && saperField[i, j].numberOf9TypeNeighbours > 0)
                    {
                        foreach (var cell in saperField[i, j].getNeighbours())
                        {
                            if (cell != null)
                            {
                                if (cell.value > 0 && cell.value < 9
                                    && cell != saperField[i, j] && cell.numberOf9TypeNeighbours > saperField[i, j].numberOf9TypeNeighbours)
                                {
                                    if (IfCommonNotOpenedNeighbours(i, j, cell.get9TypeNeighbours()) == true &&
                                        (cell.value - cell.numberOfFlags) - (saperField[i, j].value - saperField[i, j].numberOfFlags) == 0)
                                    {
                                        foreach (var cellForClick in cell.get9TypeNeighbours())
                                        {
                                            if (saperField[i, j].get9TypeNeighbours().Contains(cellForClick) == false)
                                            {
                                                form.Open(cellForClick.Y, cellForClick.X);
                                            }
                                        }
                                        return true;
                                    }
                                    if (IfCommonNotOpenedNeighbours(i, j, cell.get9TypeNeighbours()) == true &&
                                        (cell.value - cell.numberOfFlags) - (saperField[i, j].value - saperField[i, j].numberOfFlags) == cell.numberOf9TypeNeighbours - saperField[i, j].numberOf9TypeNeighbours)
                                    {
                                        foreach (var cellForClick in cell.get9TypeNeighbours())
                                        {
                                            if (saperField[i, j].get9TypeNeighbours().Contains(cellForClick) == false)
                                            {
                                                form.Open(cellForClick.Y, cellForClick.X);
                                            }
                                        }
                                        return true;
                                    }
                                }
                            }
                        }
                    }
                }
            return false;
        }
    }
}
