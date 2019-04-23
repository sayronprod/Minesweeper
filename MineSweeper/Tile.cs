using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Minesweeper.Properties.Resources;
namespace Minesweeper
{
    class Tile:UserControl
    {
        private int buttonSize = 25;

        public int status;
        public bool IsOpened { get; set; }
        public bool IsFlaged { get; set; }
        public bool IsAngle { get; set; }
        public int i, j;
        public Tile()
        {
            Size = new System.Drawing.Size(25, 25);
        }

        public Tile(int _x,int _y,int _i,int _j,bool angle)
        {
            Size = new System.Drawing.Size(buttonSize, buttonSize);
            Location = new System.Drawing.Point(_x, _y);
            BackgroundImage = Images.images[12];
            BackgroundImageLayout = ImageLayout.Stretch;
            i = _i;
            j = _j;
            IsAngle = angle;
        }

        public void Open()
        {
            BackgroundImage = Images.images[status];
            IsOpened = true;
        }

        public void Boom()
        {
            BackgroundImage = Images.images[9];
            IsOpened = true;
        }
        
        public void Flag()
        {
            BackgroundImage = Images.images[11];
            IsFlaged = true;
        }

        public void UnFlag()
        {
            BackgroundImage = Images.images[12];
            IsFlaged = false;
        }
    }
}
