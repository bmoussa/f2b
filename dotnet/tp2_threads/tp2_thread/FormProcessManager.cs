using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Premier;
using WpfAppliTh;
using System.Windows;

namespace tp1_process
{
    public partial class process_manager : Form
    {
        /* --------------- CONSTANTS ---------------- */
        static string BALL_DLL_NAME = "Ballon";
        static string PRIME_DLL_NAME = "Premier";


        /* --------------- GLOBAL VARIABLES ---------------- */
        // list of child threads
        private List<Thread> threads = new List<Thread>();
        // state of threads table
        private bool show_threads = true;
        // delegate used by Asynchronous tasks that may return Thread objects
        delegate void ThreadArgReturningVoidDelegate(Thread p);


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

        // callback for menu => Create => Ball Thread
        private void ballProcessToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            launch_thread(BALL_DLL_NAME);
        }

        // callback for menu => Create => Prime Thread
        private void primeProcessToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            launch_thread(PRIME_DLL_NAME);
        }

        // callback for the X button of a child
        void child_process_exited(object sender, EventArgs e)
        {
            // process must do this call using Invoke because it's another thread
            void remove_exited_child(Thread p)
            {
                this.threads.Remove(p);
                this.update_listView();
            }

            if (this.listView1.InvokeRequired)
            {
                Thread p = (Thread)sender;
                ThreadArgReturningVoidDelegate d = new ThreadArgReturningVoidDelegate(remove_exited_child);
                this.Invoke(d, new object[] { p });
            }
        }

        // callback for menu => Show/Hide table
        private void showProcessToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // invert table status
            show_threads = !show_threads;

            // update table according to new state
            if (show_threads)
                this.listView1.Show();
            else
                this.listView1.Hide();
        }

        // callback for menu => Delete => Last Ball Thread
        private void lastBallProcessToolStripMenuItem_Click(object sender, EventArgs e)
        {
            finish_last_process_with_name(BALL_DLL_NAME);
        }

        // callback for menu => Delete => Last Prime Thread
        private void lastPrimeProcessToolStripMenuItem_Click(object sender, EventArgs e)
        {
            finish_last_process_with_name(PRIME_DLL_NAME);
        }

        // callback for menu => Delete => Last Thread
        private void lastProcessToolStripMenuItem_Click(object sender, EventArgs e)
        {
            finish_last();
        }

        // callback for menu => Delete => All Threades
        private void allProcessesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            finish_childs();

            update_listView();
        }

        /* --------------- AUXILIARY FUNCTIONS ---------------- */
        
        // launch a thread with name thread_name, 
        // append it to threads list and refresh table
        private void launch_thread(string thread_name)
        {
            void launch_thread_ball()
            {
                WindowBallon b = new WindowBallon();
                b.Show();
                System.Windows.Threading.Dispatcher.Run();
            }
            void launch_thread_prime()
            {
                NombrePremier p = new NombrePremier();
                p.CalculatePrimes();
                System.Windows.Threading.Dispatcher.Run();
            }

            Thread t = (thread_name == BALL_DLL_NAME) ?
                    new Thread(launch_thread_ball) :
                    new Thread(launch_thread_prime);

            t.Name = thread_name;

            // thread must be STA to change UI components
            t.SetApartmentState(ApartmentState.STA);
            t.Start();

            // TODO: add exit callback
            //p.EnableRaisingEvents = true;
            //p.Exited += new EventHandler(child_process_exited);

            // append process to list
            threads.Add(t);

            update_listView();
        }

        // finish the last launched process with name process_name, 
        // remove it from processes list and refresh table
        private void finish_last_process_with_name(string process_name)
        {
            // find last process with given name
            bool hasName(Thread p) => process_name.Equals(p.Name);
            Predicate<Thread> hasProcName = hasName;
            Thread last = this.threads.FindLast(hasProcName);

            finish_process(last);

            // remove from global list
            this.threads.Remove(last);

            update_listView();
        }

        // finish the last launched process, 
        // remove it from processes list and refresh table
        private void finish_last()
        {
            // finish last process from list and remove from list
            Thread last = this.threads.Last();
            finish_process(last);
            this.threads.Remove(last);

            update_listView();
        }

        // finish a process by releasing its resources and closing its window
        private void finish_process(Thread p)
        {
            if (p != null)
            {
                /*
                // Close process by sending a close message to its main window.
                p.CloseMainWindow();
                // Free resources associated to process.
                p.Close();
                */
            }
        }

        // finish all childs of the manager process
        private void finish_childs()
        {
            // finish child processes
            foreach (Thread p in this.threads)
            {
                finish_process(p);
            }

            // empty threads list
            this.threads.Clear();
        }

        // clean table and redraw it using the actual data of threads list
        private void update_listView()
        {
            // empty listview
            this.listView1.Clear();

            // draw listview headers
            this.listView1.View = View.Details;
            this.listView1.Columns.Add("Index", 40, System.Windows.Forms.HorizontalAlignment.Left);
            this.listView1.Columns.Add("Thread PID", 100, System.Windows.Forms.HorizontalAlignment.Left);
            this.listView1.Columns.Add("Thread name", 100, System.Windows.Forms.HorizontalAlignment.Left);

            // fill listview using threads list
            int i = 1;
            foreach (Thread p in this.threads)
            {
                string[] arr = { i++.ToString(), p.Priority.ToString(), p.Name };
                ListViewItem itm = new ListViewItem(arr);
                listView1.Items.Add(itm);
            }

            // redraw updated listview
            listView1.Refresh();
        }

    }
}
