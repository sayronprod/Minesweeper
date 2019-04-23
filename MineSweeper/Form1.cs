using System;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using static Minesweeper.Properties.Resources;
namespace Minesweeper
{
    public partial class Form1 : Form
    {
        private ToolStripMenuItem tempMenuItem;
        private Tile tempTile;
        private int openedCount { get; set; }
        private readonly int buttonSize = 25;
        private int Rows { get; set; }
        private int Columns { get; set; }
        private int Mines { get; set; }
        private int GameMines { get; set; }
        private int timer;
        private bool gameOver { get; set; }
        private bool botStarted = false;
        private bool globalPause = false;
        private int boardSizeX = 0;
        private int boardSizeY = 0;

        private EventWaitHandle wh = new AutoResetEvent(false);
        private Thread bot;
        private Tile[,] Tiles;

        public Form1()
        {
            InitializeComponent();
            DefaultInit();
            StartGame(true);
        }        
        private void DefaultInit()
        {
            newGame.BackgroundImage = norm;
            Rows = Properties.Settings.Default.beginnerWidth;
            Columns = Properties.Settings.Default.beginnerHeight;
            GameMines = Properties.Settings.Default.beginnerMines;
            tempMenuItem = beginnerItemMenu;
        }
        private void StartGame(bool isNew)
        {
            timer = 1;
            openedCount = 0;
            Mines = GameMines;
            gameOver = false;
            timer1.Stop();
            gameTime.Text = "000";
            mineCount.Text = Mines.ToString().PadLeft(3, '0');
            
            if (isNew)
            {
                boardSizeX = 0;
                boardSizeY = 0;
                CreateBoard();
            }else
            {
                UpdateBoard();
            }
            NewBombs();
            CreateGlobalMap();
            if (isNew)
            {
                ShowBoard();
            }
            this.SetClientSizeCore(boardSizeX, boardSizeY + board.Location.Y);            
        }
        private void UpdateBoard()
        {
            foreach(var tile in Tiles)
            {
                tile.status = 0;
                tile.BackgroundImage = Images.images[12];
                tile.IsOpened = false;
                tile.IsFlaged = false;
            }
        }
        private void CreateBoard()
        {
            Tiles = new Tile[Rows, Columns];
            
            for (int i = 0; i < Rows; i++)
            {
                boardSizeX = 0;
                for (int j = 0; j < Columns; j++)
                {
                    if (i == 0 && j == 0)
                    {
                        Tiles[i, j] = new Tile(boardSizeX, boardSizeY, i, j, true);
                    }
                    else if (i == Rows - 1 && j == 0)
                    {
                        Tiles[i, j] = new Tile(boardSizeX, boardSizeY, i, j, true);
                    }
                    else if (i == 0 && j == Columns - 1)
                    {
                        Tiles[i, j] = new Tile(boardSizeX, boardSizeY, i, j, true);
                    }
                    else if (i == Rows - 1 && j == Columns - 1)
                    {
                        Tiles[i, j] = new Tile(boardSizeX, boardSizeY, i, j, true);
                    }
                    else
                    {
                        Tiles[i, j] = new Tile(boardSizeX, boardSizeY, i, j,false);
                    }
                    Tiles[i, j].MouseDown += tile_MouseDown;
                    Tiles[i, j].MouseUp += tile_MouseUp;
                    Tiles[i, j].MouseEnter += tile_MouseEnter;
                    boardSizeX += buttonSize;
                }
                boardSizeY += buttonSize;
            }
        }
        private void NewBombs()
        {
            Random rand = new Random();
            for (int i = 0; i < Mines; i++)
            {
                int xcoord = rand.Next(0, Rows);
                int ycoord = rand.Next(0, Columns);
                while (Tiles[xcoord, ycoord].status == 10 || Tiles[xcoord, ycoord].IsAngle)
                {
                    xcoord = rand.Next(0, Rows);
                    ycoord = rand.Next(0, Columns);
                }
                Tiles[xcoord, ycoord].status = 10;
            }
        }
        private void CreateGlobalMap()
        {
            for (var i = 0; i < Rows; i++)
            {
                for (var j = 0; j < Columns; j++)
                {
                    if (Tiles[i, j].status == 10) continue;
                    var mines = 0;
                    for (var k = -1; k < 2; k++)
                    {
                        for (var l = -1; l < 2; l++)
                        {
                            var posI = i + k;
                            var posJ = j + l;
                            if (l != 2 && k != 2 && posI >= 0 && posI < Rows && posJ >= 0 && posJ < Columns && Tiles[posI, posJ].status == 10)
                                mines++;
                        }
                    }
                    Tiles[i, j].status = mines;
                }
            }
        }
        private void ShowBoard()
        {
            board.Controls.Clear();
            foreach (var tile in Tiles)
            {
                board.Controls.Add(tile);
            }
        }
        private void reSize()
        {
            newGame.Left = (panel1.Width - newGame.Width) / 2;
            newGame.Top = (panel1.Height - newGame.Height) / 2;
        }
        private void OpenAll()
        {
            int minestrue = 0;
            int minesfalse = 0;
            foreach (Tile tile in board.Controls)
            {
                if (!tile.IsOpened)
                {
                    tile.Open();
                }
                if (tile.IsFlaged && tile.status == 10)
                {
                    tile.BackgroundImage = flagged;
                    minestrue++;
                }
                if(tile.IsFlaged && tile.status != 10)
                {
                    tile.BackgroundImage = badFlagged;
                    minesfalse++;
                }
            }
            mineCount.BeginInvoke(new MethodInvoker(delegate
            {
                mineCount.Text = (GameMines - minestrue).ToString("000");
            }));            
        }
        public void Open(int i, int j)
        {
            if (Tiles[i,j].status == 10)
            {
                Tiles[i, j].Boom();
                OpenAll();
                timer1.Stop();
                newGame.BackgroundImage = rip;
                gameOver = true;
                return;
            }
            if (Tiles[i, j].status != 0)
            {
                Tiles[i, j].Open();
                openedCount++;
            }
            else
            {
                for (int k = -1; k < 2; k++)
                {
                    for (int l = -1; l < 2; l++)
                    {
                        var posI = i + k;
                        var posJ = j + l;
                        if (l != 2 && k != 2 && posI >= 0 && posI < Rows && posJ >= 0 && posJ < Columns && Tiles[posI, posJ].status != 10 && !Tiles[posI, posJ].IsOpened)
                        {
                            Tiles[posI, posJ].Open();
                            openedCount++;
                            if (Tiles[posI, posJ].status == 0)
                            {
                                Open(posI, posJ);
                            }
                        }
                    }
                }
            }
            CheckWin();
        }
        private void CheckWin()
        {
            if (openedCount == (Columns * Rows - GameMines))
            {
                timer1.Stop();
                gameOver = true;
                newGame.BackgroundImage = win;
                if (IsBestResult())
                {
                    AddScore add = new AddScore(tempMenuItem.Text, (timer.ToString() + " seconds"));
                    add.ShowDialog();
                }
            }
            else
            {
                newGame.BackgroundImage = norm;
            }
        }
        public void OpenAllIfFlags(int i,int j)
        {
            for (int k = -1; k < 2; k++)
            {
                for (int l = -1; l < 2; l++)
                {
                    var posI = i + k;
                    var posJ = j + l;
                    if (l != 2 && k != 2 && posI >= 0 && posI < Rows && posJ >= 0 && posJ < Columns && !Tiles[posI, posJ].IsFlaged && !Tiles[posI, posJ].IsOpened)
                    {
                        if (Tiles[posI, posJ].status == 10)
                        {
                            Tiles[i, j].Boom();
                            OpenAll();
                            timer1.Stop();
                            newGame.BackgroundImage = rip;
                            gameOver = true;
                            return;
                        }
                        Tiles[posI, posJ].Open();
                        openedCount++;
                        if (Tiles[posI, posJ].status == 0)
                        {
                            Open(posI, posJ);
                        }
                    }
                }
            }
            CheckWin();
        }
        private bool IsBestResult()
        {
            int time = timer;
            int besttime = 999;
            switch (tempMenuItem.Text)
            {
                case "Beginner":
                    besttime = Int32.Parse(Properties.Settings.Default.beginnerTime.Split(' ').First());
                    break;
                case "Intermediate":
                    besttime = Int32.Parse(Properties.Settings.Default.intermediateTime.Split(' ').First());
                    break;
                case "Expert":
                    besttime = Int32.Parse(Properties.Settings.Default.expertTime.Split(' ').First());
                    break;
                case "Custom...":
                    return false;
            }
            if(time<besttime)
            {
                return true;
            }else
            {
                return false;
            }
        }
        public bool PauseResume()
        {
            if (globalPause)
            {
                wh.WaitOne();
                return true;
            }
            else
            {
                wh.Set();
                return false;
            }
        }
        private void StartBot()
        {
            int[,] oldTableOfNumbers = new int[Rows, Columns];
            while (botStarted)
            {
                oldTableOfNumbers = GameIteration(oldTableOfNumbers);
            }
            bot = null;
        }
        private int[,] GameIteration(int[,] oldTableOfNumbers)
        {
            int[,] newTableOfNumbers = null;
            newTableOfNumbers = TilesGetStatus();
            if (PauseResume() == true)
            {
                newTableOfNumbers = TilesGetStatus();
                return newTableOfNumbers;
            }
            SaperFieldOfCells fieldOfCells = new SaperFieldOfCells(newTableOfNumbers, GameMines, Rows, Columns);
            BotAlgoritm alg = new BotAlgoritm(fieldOfCells, Tiles, Rows, Columns, this);
            if (alg.IfGameOver() || alg.IfVictory())
            {
                botStarted = false;
                return newTableOfNumbers;
            }
            alg.SimpleAlgorithm();
            alg.HardAlgorithm();
            alg.DoubleClickingAlgorithm();
            if (alg.EndOfCycle(oldTableOfNumbers, newTableOfNumbers))
            {
                alg.CleverRandomClick();
            }
            return newTableOfNumbers;
        }
        private int[,] TilesGetStatus()
        {
            int[,] vs = new int[Rows, Columns];
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    if (Tiles[i, j].IsOpened)
                    {
                        vs[i, j] = Tiles[i, j].status;
                    }
                    else if (Tiles[i, j].IsFlaged)
                    {
                        vs[i, j] = -1;
                    }
                    else
                    {
                        vs[i, j] = 9;
                    }
                }
            }
            return vs;
        }

        private void exitMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }      
        private void Form1_Resize(object sender, EventArgs e)
        {
            reSize();
        }
        private void tile_MouseEnter(object sender, EventArgs e)
        {
            if (tempTile != null)
            {
                if (!tempTile.IsOpened && !tempTile.IsFlaged)
                {
                    tempTile.BackgroundImage = closed;
                }
                tempTile = sender as Tile;
                if (!tempTile.IsFlaged && !tempTile.IsOpened)
                {
                    tempTile.BackgroundImage = _0;
                }
            }
        }
        private void tile_MouseUp(object sender, MouseEventArgs e)
        {
            Cursor.Clip = new Rectangle();
            if (e.Button == MouseButtons.Left && !gameOver&&tempTile!=null)
            {
                if (!tempTile.IsOpened && !tempTile.IsFlaged && !gameOver)
                {
                    if (!timer1.Enabled)
                    {
                        timer1.Start();
                    }
                    Open(tempTile.i, tempTile.j);
                }
                else
                {
                    newGame.BackgroundImage = norm;
                }                
                tempTile = null;
            }
        }
        private void tile_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && !gameOver)
            {
                Cursor.Clip = board.RectangleToScreen(board.DisplayRectangle);
                tempTile = sender as Tile;
                tempTile.Capture = false;
                newGame.BackgroundImage = omg;
                if (!tempTile.IsOpened && !tempTile.IsFlaged)
                {
                    tempTile.BackgroundImage = _0;
                }
            }
            else if (e.Button == MouseButtons.Right&&!gameOver)
            {
                var tile = sender as Tile;
                if (!tile.IsFlaged && !tile.IsOpened)
                {
                    Mines--;
                    tile.Flag();
                }
                else if (!tile.IsOpened)
                {
                    Mines++;
                    tile.UnFlag();
                }
                mineCount.Text = Mines.ToString("000");
            }
        }
        private void lvlItemMenu_Click(object sender, EventArgs e)
        {
            tempMenuItem.CheckState = CheckState.Unchecked;
            tempMenuItem = (ToolStripMenuItem)sender;
            tempMenuItem.CheckState = CheckState.Checked;
            switch (tempMenuItem.Text)
            {

                case "Beginner":
                    Columns = Properties.Settings.Default.beginnerWidth;
                    Rows = Properties.Settings.Default.beginnerHeight;
                    GameMines = Properties.Settings.Default.beginnerMines;
                    break;
                case "Intermediate":
                    Columns = Properties.Settings.Default.intermediteWidth;
                    Rows = Properties.Settings.Default.intermediteHeight;
                    GameMines = Properties.Settings.Default.intermediteMines;
                    break;
                case "Expert":
                    Columns = Properties.Settings.Default.expertWidth;
                    Rows = Properties.Settings.Default.expertHeight;
                    GameMines = Properties.Settings.Default.expertMines;
                    break;
            }
            StartGame(true);
            this.Location = new Point((Screen.PrimaryScreen.Bounds.Width - this.Width) / 2,
                (Screen.PrimaryScreen.Bounds.Height - this.Height) / 2);
        }
        private void newGameItemMenu_Click(object sender, EventArgs e)
        {
            StartGame(false);
        }
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            newGame.BackgroundImage = norm;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (timer < 1000)
            {
                gameTime.Text = timer++.ToString().PadLeft(3, '0');
            }
            else
            {
                timer1.Stop();
            }
        }
        private void newGame_MouseDown(object sender, MouseEventArgs e)
        {
            newGame.BackgroundImage = norm2;
            newGame.Capture = false;
        }
        private void newGame_MouseUp(object sender, MouseEventArgs e)
        {
            newGame.BackgroundImage = norm;
            StartGame(false);
        }
        private void newGame_MouseLeave(object sender, EventArgs e)
        {
            if (!gameOver)
            {
                newGame.BackgroundImage = norm;
            }
        }
        private void newGame_MouseEnter(object sender, EventArgs e)
        {
            if ((Control.MouseButtons & MouseButtons.Left) != 0)
            {
                newGame.BackgroundImage = norm2;
            }
        }
        private void customItemMenu_Click(object sender, EventArgs e)
        {
            tempMenuItem.CheckState = CheckState.Unchecked;
            tempMenuItem = (ToolStripMenuItem)sender;
            tempMenuItem.CheckState = CheckState.Checked;
            Custom custom = new Custom(Rows,Columns,GameMines);
            custom.FormClosing += Custom_FormClosing;
            custom.ShowDialog();            
        }
        private void Custom_FormClosing(object sender, FormClosingEventArgs e)
        {
            Custom custom = sender as Custom;
            Rows = custom.w;
            Columns = custom.h;
            GameMines = custom.m;
            StartGame(true);
        }
        private void besttimesItemMenu_Click(object sender, EventArgs e)
        {
            BestTimes bestTimes = new BestTimes();
            bestTimes.ShowDialog(this);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            botStarted = true;
            if(bot==null)
            {
                globalPause = false;
                bot = new Thread(StartBot);
                bot.IsBackground = true;
                bot.Start();
                wh.Set();
            }else
            {
                return;
            }
        }
    }
}