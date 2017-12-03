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

namespace tp2_thread
{
    public partial class thread_manager : Form
    {
        /* --------------- CONSTANTS ---------------- */
        static string BALL_DLL_NAME = "Ballon";
        static string PRIME_DLL_NAME = "Premier";


        /* --------------- GLOBAL VARIABLES ---------------- */
        // list of child threads
        private List<Thread> threads = new List<Thread>();
        // state of threads table
        private bool show_threads = true;
        // state of program
        private bool paused = false;
        // delegate used by Asynchronous tasks that may return Thread objects
        delegate void ThreadArgReturningVoidDelegate(Thread p);


        public thread_manager()
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
            // finish child threads before exiting
            finish_childs();
        }

        // callback for menu => Quit
        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // finish child threads before exiting
            finish_childs();

            // finish this thread
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
        void child_thread_exited(object sender, EventArgs e)
        {
            // thread must do this call using Invoke because it's another thread
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
            finish_last_thread_with_name(BALL_DLL_NAME);
        }

        // callback for menu => Delete => Last Prime Thread
        private void lastPrimeProcessToolStripMenuItem_Click(object sender, EventArgs e)
        {
            finish_last_thread_with_name(PRIME_DLL_NAME);
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

        private void pauseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // invert program status
            this.paused = !this.paused;

            // pause or resume all threads
            foreach (Thread t in this.threads)
            {
                if (this.paused)
                    t.Suspend();
                else
                    t.Resume();
            }
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
            //p.Exited += new EventHandler(child_thread_exited);

            // append thread to list
            threads.Add(t);

            update_listView();
        }

        // finish the last launched thread with name thread_name, 
        // remove it from threads list and refresh table
        private void finish_last_thread_with_name(string thread_name)
        {
            // find last thread with given name
            bool hasName(Thread p) => thread_name.Equals(p.Name);
            Predicate<Thread> hasProcName = hasName;
            Thread last = this.threads.FindLast(hasProcName);

            finish_thread(last);

            // remove from global list
            this.threads.Remove(last);

            update_listView();
        }

        // finish the last launched thread, 
        // remove it from threads list and refresh table
        private void finish_last()
        {
            // finish last thread from list and remove from list
            if (this.threads.Count > 0)
            {
                Thread last = this.threads.Last();
                finish_thread(last);
                this.threads.Remove(last);
                update_listView();
            }
        }

        // finish a thread by releasing its resources and closing its window
        private void finish_thread(Thread t)
        {
            if (t != null)
            {
                t.Abort();
            }
        }

        // finish all childs of the manager thread
        private void finish_childs()
        {
            // finish child threads
            foreach (Thread p in this.threads)
            {
                finish_thread(p);
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
            this.listView1.Columns.Add("Thread ID", 100, System.Windows.Forms.HorizontalAlignment.Left);
            this.listView1.Columns.Add("Thread name", 100, System.Windows.Forms.HorizontalAlignment.Left);

            // fill listview using threads list
            int i = 1;
            foreach (Thread p in this.threads)
            {
                string[] arr = { i++.ToString(), p.ManagedThreadId.ToString(), p.Name };
                ListViewItem itm = new ListViewItem(arr);
                listView1.Items.Add(itm);
            }

            // redraw updated listview
            listView1.Refresh();
        }
    }
}
