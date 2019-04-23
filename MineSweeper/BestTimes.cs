using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Minesweeper.Properties.Settings;
namespace Minesweeper
{
    public partial class BestTimes : Form
    {
        private string defaultName = "Anonymous";
        private string defaultTime = "999 seconds";
        public BestTimes()
        {
            InitializeComponent();
            WriteScores();
        }
        private void WriteScores()
        {
            BeginnerTime.Text = Default.beginnerTime;
            Beginner_Name.Text = Default.beginnerName;
            IntermediateTime.Text = Default.intermediateTime;
            IntermediateName.Text = Default.intermediateName;
            ExpertTime.Text = Default.expertTime;
            ExpertName.Text = Default.expertName;

        }
        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Default.beginnerTime = defaultTime;
            Default.beginnerName = defaultName;
            Default.intermediateTime = defaultTime;
            Default.intermediateName = defaultName;
            Default.expertTime = defaultTime;
            Default.expertName = defaultName;
            Default.Save();
            WriteScores();
        }
    }
}
