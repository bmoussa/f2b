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
        static string BALL_DLL_NAME = "Ballon";
        static string PRIME_DLL_NAME = "Premier";
        static string DLL_EXTENSION = ".dll";


        /* --------------- GLOBAL VARIABLES ---------------- */
        // list of child processes
        private List<Process> procs = new List<Process>();
        // state of processes table
        private bool show_procs = true;
        // delegate used by Asynchronous tasks that may return Process objects
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

        // callback for X button (close)
        private void FormClosingHandler(object sender, EventArgs e)
        {
            // finish child processes before exiting
            finish_childs();
        }
        
        // callback for menu => Quit
        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // finish child processes before exiting
            finish_childs();

            // finish this process
            Close();
        }

        // callback for menu => Create => Ball Process
        private void ballProcessToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            create_process(BALL_DLL_NAME + DLL_EXTENSION);
        }
        
        // callback for menu => Create => Prime Process
        private void primeProcessToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            create_process(PRIME_DLL_NAME + DLL_EXTENSION);
        }
        
        // callback for the X button of a child
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
        
        // callback for menu => Show/Hide table
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

        // callback for menu => Delete => Last Ball Process
        private void lastBallProcessToolStripMenuItem_Click(object sender, EventArgs e)
        {
            finish_last_process_with_name(BALL_DLL_NAME);
        }

        // callback for menu => Delete => Last Prime Process
        private void lastPrimeProcessToolStripMenuItem_Click(object sender, EventArgs e)
        {
            finish_last_process_with_name(PRIME_DLL_NAME);
        }

        // callback for menu => Delete => Last Process
        private void lastProcessToolStripMenuItem_Click(object sender, EventArgs e)
        {
            finish_last();
        }

        // callback for menu => Delete => All Processes
        private void allProcessesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            finish_childs();

            update_listView();
        }

        /* --------------- AUXILIARY FUNCTIONS ---------------- */

        // create a process with name process_name, 
        // append it to processes list and refresh table
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

        /*
        // launch a thread with name thread_name, 
        // append it to processes list and refresh table
        private void launch_thread(string thread_name)
        {
            ProcessThread
            Process p = Process.Start(process_name);
            p.EnableRaisingEvents = true;
            p.Exited += new EventHandler(child_process_exited);

            // append process to list
            procs.Add(p);

            update_listView();
        }
        */

        // finish the last launched process with name process_name, 
        // remove it from processes list and refresh table
        private void finish_last_process_with_name(string process_name)
        {
            // find last process with given name
            bool hasName(Process p) => process_name.Equals(p.ProcessName);
            Predicate<Process> hasProcName = hasName;
            Process last = this.procs.FindLast(hasProcName);

            finish_process(last);

            // remove from global list
            this.procs.Remove(last);
            
            update_listView();
        }

        // finish the last launched process, 
        // remove it from processes list and refresh table
        private void finish_last()
        {
            // finish last process from list and remove from list
            Process last = this.procs.Last();
            finish_process(last);
            this.procs.Remove(last);

            update_listView();
        }

        // finish a process by releasing its resources and closing its window
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

        // finish all childs of the manager process
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
        
        // clean table and redraw it using the actual data of procs list
        private void update_listView()
        {
            // empty listview
            this.listView1.Clear();

            // draw listview headers
            this.listView1.View = View.Details;
            this.listView1.Columns.Add("Index", 40, HorizontalAlignment.Left);
            this.listView1.Columns.Add("Thread PID", 100, HorizontalAlignment.Left);
            this.listView1.Columns.Add("Thread name", 100, HorizontalAlignment.Left);

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
