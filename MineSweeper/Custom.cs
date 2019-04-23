using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minesweeper
{
    public partial class Custom : Form
    {
        public int w,h,m;
        public Custom(int width,int height,int mines)
        {
            InitializeComponent();
            w = width;
            h = height;
            m = mines;
            Init();
        }
        private void Init()
        {
            HeightText.Text = h.ToString();
            WidthText.Text = w.ToString();
            MinesText.Text = m.ToString();
        }
        private void HeightText_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
        (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            w = Int32.Parse(WidthText.Text);
            h = Int32.Parse(HeightText.Text);
            m = Int32.Parse(MinesText.Text);
            if (Check())
            {
                Close();
            }else
            {
                MessageBox.Show("Incorrect data");
            }
        }
        private bool Check()
        {
            int ver = (m * 100) / (w * h);
            if(w>9&&h>9&&m>0&&ver<82)
            {
                return true;
            }else
            {
                return false;
            }
        }
    }
}
