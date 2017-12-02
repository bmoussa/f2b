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
        private List<Process> procs = new List<Process>();
        private bool show_procs = true;
        
        delegate void ProcessArgReturningVoidDelegate(Process p);


        public process_manager()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // add X button callback
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormClosingHandler);

            // initialize listview
            update_listView();
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
            create_process(BALL_PROC_NAME + PROC_EXTENSION);
        }

        private void primeProcessToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            create_process(PRIME_PROC_NAME + PROC_EXTENSION);
        }
        
        void child_process_exited(object sender, EventArgs e)
        {
            // process must do this call using Invoke because it's another thread
            void remove_exited_child(Process p)
            {
                this.procs.Remove(p);
                this.update_listView();
            }

            if (this.listView1.InvokeRequired)
            {
                Process p = (Process)sender;
                ProcessArgReturningVoidDelegate d = new ProcessArgReturningVoidDelegate(remove_exited_child);
                this.Invoke(d, new object[] { p });
            }
        }
        
        private void showProcessToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // invert table status
            show_procs = !show_procs;

            // update table according to new state
            if (show_procs)
                this.listView1.Show();
            else
                this.listView1.Hide();
        }

        private void lastBallProcessToolStripMenuItem_Click(object sender, EventArgs e)
        {
            find_last_process_with_name(BALL_PROC_NAME);
            
            update_listView();
        }

        private void lastPrimeProcessToolStripMenuItem_Click(object sender, EventArgs e)
        {
            find_last_process_with_name(PRIME_PROC_NAME);
            
            update_listView();
        }

        private void lastProcessToolStripMenuItem_Click(object sender, EventArgs e)
        {
            finish_last();

            update_listView();
        }

        private void allProcessesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            finish_childs();

            update_listView();
        }

        /* --------------- AUXILIARY FUNCTIONS ---------------- */

        private void create_process(string process_name)
        {
            // create the process with a callback function
            Process p = Process.Start(process_name);
            p.EnableRaisingEvents = true;
            p.Exited += new EventHandler(child_process_exited);

            // append process to list
            procs.Add(p);

            update_listView();
        }

        private void find_last_process_with_name(string name)
        {
            // find last process with given name
            bool hasName(Process p) => name.Equals(p.ProcessName);
            Predicate<Process> hasProcName = hasName;
            Process last = this.procs.FindLast(hasProcName);

            finish_process(last);

            // remove from global list
            this.procs.Remove(last);
        }

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
        
        private void update_listView()
        {
            // empty listview
            this.listView1.Clear();

            // draw listview headers
            this.listView1.View = View.Details;
            this.listView1.Columns.Add("Index", 40, HorizontalAlignment.Left);
            this.listView1.Columns.Add("Process PID", 100, HorizontalAlignment.Left);
            this.listView1.Columns.Add("Process name", 100, HorizontalAlignment.Left);

            // fill listview using procs list
            int i = 1;
            foreach (Process p in this.procs)
            {
                string[] arr = {i++.ToString(), p.Id.ToString(), p.ProcessName};
                ListViewItem itm = new ListViewItem(arr);
                listView1.Items.Add(itm);
            }

            // redraw updated listview
            listView1.Refresh();
        }
        
    }
}
