using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tp1_process
{
    public partial class process_manager : Form
    {
        List<Process> ball_procs = new List<Process>();
        List<Process> prime_procs = new List<Process>();

        public process_manager()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void createToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void ballProcessToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Process p = Process.Start("Ballon.exe");
            ball_procs.Add(p);
        }

        private void primeProcessToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Process p = Process.Start("PrimeNumber.exe");
            prime_procs.Add(p);
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void showProcessToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
