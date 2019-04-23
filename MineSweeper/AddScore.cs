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
    public partial class AddScore : Form
    {
        string lvl,time;
        public AddScore(string _lvl,string _time)
        {
            InitializeComponent();
            lvl = _lvl;
            time = _time;
        }
        private void add_Click(object sender, EventArgs e)
        {
            string text = name.Text;

            switch (lvl)
            {
                case "Beginner":
                    Properties.Settings.Default.beginnerName = text;
                    Properties.Settings.Default.beginnerTime = time;
                    break;
                case "Intermediate":
                    Properties.Settings.Default.intermediateName = text;
                    Properties.Settings.Default.intermediateTime = time;
                    break;
                case "Expert":
                    Properties.Settings.Default.expertName = text;
                    Properties.Settings.Default.expertTime = time;
                    break;
            }
            Properties.Settings.Default.Save();
            Close();
        }
    }
}
