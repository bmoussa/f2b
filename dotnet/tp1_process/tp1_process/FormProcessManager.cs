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
        /* --------------- CONSTANTS ---------------- */
        static string BALL_PROC_NAME = "Ballon";
        static string PRIME_PROC_NAME = "PrimeNumber";
        static string PROC_EXTENSION = ".exe";


        /* --------------- GLOBAL VARIABLES ---------------- */
        List<Process> procs = new List<Process>();


        public process_manager()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormClosingHandler);
        }

        
        /* --------------- CALLBACK FUNCTIONS ---------------- */

        // callback for X button
        private void FormClosingHandler(object sender, EventArgs e)
        {
            finish_childs();
        }
        
        // callback for QUIT button
        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            finish_childs();

            // finish this process
            Close();
        }

        private void ballProcessToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            // create a process from type ball
            Process p = Process.Start(BALL_PROC_NAME + PROC_EXTENSION);

            // append process to list
            procs.Add(p);
        }

        private void primeProcessToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            // create a process from type prime
            Process p = Process.Start(PRIME_PROC_NAME + PROC_EXTENSION);
            Console.WriteLine(p.ProcessName);

            // append process to list
            procs.Add(p);
        }

        private void showProcessToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void lastBallProcessToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // find last ball process in list
            Predicate<Process> hasProcName = IsBallProcess;
            Process last = this.procs.FindLast(hasProcName);

            finish_process(last);

            // remove from global list
            this.procs.Remove(last);

            // TODO: refresh listview
        }

        private void lastPrimeProcessToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // find last prime process in list
            Predicate<Process> hasProcName = IsPrimeProcess;
            Process last = this.procs.FindLast(hasProcName);

            finish_process(last);

            // remove from global list
            this.procs.Remove(last);

            // TODO: refresh listview
        }

        private void lastProcessToolStripMenuItem_Click(object sender, EventArgs e)
        {
            finish_last();

            // TODO: refresh listview
        }

        private void allProcessesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            finish_childs();
        }

        /* --------------- AUXILIARY FUNCTIONS ---------------- */

        private void finish_process(Process p)
        {
            if (p != null)
            {
                // Close process by sending a close message to its main window.
                p.CloseMainWindow();
                // Free resources associated to process.
                p.Close();
            }
        }

        private void finish_childs()
        {
            // finish child processes
            foreach (Process p in this.procs)
            {
                finish_process(p);
            }

            // empty procs list
            this.procs.Clear();
        }

        // finish last process and remove from global list
        private void finish_last()
        {
            // finish last process from list and remove from list
            Process last = this.procs.Last();
            finish_process(last);
            this.procs.Remove(last);
        }

        /* --------------- PREDICATE FUNCTIONS ---------------- */

        static bool IsBallProcess(Process p) => BALL_PROC_NAME.Equals(p.ProcessName);

        static bool IsPrimeProcess(Process p) => PRIME_PROC_NAME.Equals(p.ProcessName);
    }
}
